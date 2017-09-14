using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace DM.UBP.CodeBuilder
{
    public partial class MainFrm : Form
    {
        private DbType _DbType;

        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            tbCodeRootPath.Text = System.Configuration.ConfigurationManager.AppSettings["CodeRootPath"];
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            using (var context = new MyDbContext("default"))
            {
                List<string> tableNameList = new List<string>();

                switch (context.Database.Connection.GetType().Name)
                {
                    case "OracleConnection":
                        _DbType = DbType.Oracle;
                        tableNameList = context.Database.SqlQuery<string>("SELECT table_name from user_tables").ToList();
                        break;
                    case "SqlConnection":
                        _DbType = DbType.SqlServer;
                        tableNameList = context.Database.SqlQuery<string>("select name from sysobjects where xtype = 'U'").ToList();
                        break;

                }

                if (tableNameList.Count > 0)
                {
                    cbTableName.Items.Clear();
                    cbTableName.Items.AddRange(tableNameList.ToArray());
                }
            }

        }

        private void tbModuleName_TextChanged(object sender, EventArgs e)
        {
            //tbEntityNS.Text = _EntityNS + (String.IsNullOrEmpty(tbModuleName.Text) ? "" : "." + tbModuleName.Text)
            //                            + (String.IsNullOrEmpty(tbSubModuleName.Text) ? "" : "." + tbSubModuleName.Text);
        }

        private void cbTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var textInfo = new CultureInfo("en-US").TextInfo;
            tbFileName.Text = textInfo.ToTitleCase(cbTableName.Text.ToLower());
            //tbFileName.Text = cbTableName.Text.ToPascalCase();
            LoadDBFields();
        }

        private void LoadDBFields()
        {
            string sql = "";

            using (var context = new MyDbContext("default"))
            {
                List<Field> fieldList = new List<Field>();

                switch (context.Database.Connection.GetType().Name)
                {
                    case "OracleConnection":
                        _DbType = DbType.OracleDevart;
                        sql = "select a.COLUMN_NAME name, a.DATA_TYPE type, "
                            + @" case when a.DATA_TYPE = 'VARCHAR2' then a.DATA_LENGTH / 2
                                        when a.DATA_TYPE = 'NUMBER' then a.DATA_PRECISION
                                        else a.DATA_LENGTH end length, " 
                            + " a.DATA_SCALE scale, "
                            + " case when a.nullable = 'Y' then 1 else 0 end nullable, 0 IsIdentity, 0 ispk, b.comments, "
                            + " 1 HasInputDto, 1 HasOutputDto, "
                            + " '' TableColWidth, 0 IsEdit "
                            + " from user_TAB_COLUMNS a, user_col_comments b "
                            + " where a.table_name = b.table_name and a.column_name = b.column_name"
                            + " and a.TABLE_NAME ='" + cbTableName.Text + "'"
                            + " order by a.COLUMN_ID ";
                        break;
                    case "SqlConnection":
                        _DbType = DbType.SqlServer;
                        sql = @"SELECT col.name AS name, typ.name as type, 
                                case when col.precision > 0 then col.precision else col.max_length end AS length, 
                                col.scale AS scale,
                                cast(col.is_nullable as bit) AS nullable, cast(col.is_identity as bit) AS IsIdentity,
                                cast(case when exists (SELECT 1 FROM sys.indexes idx
                                                        join sys.index_columns idxCol
                                                                on(idx.object_id = idxCol.object_id)
                                                        WHERE idx.object_id = col.object_id
                                                                AND idxCol.index_column_id = col.column_id
                                                                AND idx.is_primary_key = 1
                                                    ) THEN 1 ELSE 0 END as bit) AS IsPk,
	                                g.[value] comments,
                                1 As listdto, 1 As editdto, 
                                '' As TableColWidth, 1 As IsEdit
                                FROM sys.columns col left join sys.types typ on(col.system_type_id = typ.system_type_id)
                                     left join sys.extended_properties g on(col.object_id = g.major_id AND g.minor_id = col.column_id)
                                    WHERE col.object_id = (SELECT object_id FROM sys.tables WHERE name = '" + cbTableName.Text + "')"
                                + " order by col.column_id ";
                        break;

                }

                fieldList = context.Database.SqlQuery<Field>(sql).ToList();
                if (fieldList.Count > 0)
                {
                    gvFields.DataSource = fieldList;
                    //gvFields.Columns[0].DataPropertyName = "NAME";
                }
            }
        }

        private void btnCodeCreate_Click(object sender, EventArgs e)
        {
            List<Field> fields_Property = new List<Field>();

            foreach (DataGridViewRow row in gvFields.Rows)
            {
                Field f = new Field();
                f.Name = row.Cells["NAME"].Value.ToString();
                f.Type = row.Cells["TYPE"].Value.ToString();
                f.Length = Convert.ToInt16(row.Cells["LENGTH"].Value is null ? 0 : row.Cells["LENGTH"].Value);
                f.Scale = Convert.ToByte(row.Cells["SCALE"].Value is null ? 0 : row.Cells["SCALE"].Value);
                f.IsIdentity = Convert.ToBoolean(row.Cells["IsIdentity"].Value is null ? false : row.Cells["IsIdentity"].Value);
                f.Nullable = Convert.ToBoolean(row.Cells["Nullable"].Value is null ? true : row.Cells["Nullable"].Value);
                f.Comments = row.Cells["comments"].Value is null ? "" : row.Cells["comments"].Value.ToString();
                f.IsPk = Convert.ToBoolean(row.Cells["IsPk"].Value is null ? false : row.Cells["IsPk"].Value);
                f.Property = row.Cells["Property"].Value is null ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(f.Name.ToLower()) : row.Cells["Property"].Value.ToString();
                f.HasInputDto = Convert.ToBoolean(row.Cells["HasInputDto"].Value is null ? false : row.Cells["HasInputDto"].Value);
                f.HasOutputDto = Convert.ToBoolean(row.Cells["HasOutputDto"].Value is null ? false : row.Cells["HasOutputDto"].Value);
                f.TableColWidth = Convert.ToInt32(row.Cells["TableColWidth"].Value is null ? 0 : row.Cells["TableColWidth"].Value);
                f.IsEdit = Convert.ToBoolean(row.Cells["IsEdit"].Value is null ? false : row.Cells["IsEdit"].Value);

                fields_Property.Add(f);
            }

            EntityCodeBuilder entityCodeBuilder = new EntityCodeBuilder();
            entityCodeBuilder.RootCodePath = tbCodeRootPath.Text;
            entityCodeBuilder.TableName = cbTableName.Text;
            entityCodeBuilder.DbType = _DbType;
            entityCodeBuilder.Fields = fields_Property;
            entityCodeBuilder.ModuleName = tbModuleName.Text;
            entityCodeBuilder.SubModuleName = tbSubModuleName.Text;
            entityCodeBuilder.FileName = tbFileName.Text;
            entityCodeBuilder.ClassName = tbClass.Text;
            entityCodeBuilder.ClassPluralName = System.Data.Entity.Design.PluralizationServices.PluralizationService.CreateService(new CultureInfo("en")).Pluralize(tbClass.Text);
            entityCodeBuilder.ClassComments = tbClassDesc.Text;
            entityCodeBuilder.BaseClass = cbBaseClass.Text;
            entityCodeBuilder.PkType = cbPkType.Text;

            if (rbNone.Checked)
                entityCodeBuilder.TenantInterface = "";
            if (rbIMayHaveTenant.Checked)
                entityCodeBuilder.TenantInterface = "IMayHaveTenant";
            if (rbIMustHaveTenant.Checked)
                entityCodeBuilder.TenantInterface = "IMustHaveTenant";

            EntityConfigurationCodeBuilder entityConfigurationCodeBuilder = new EntityConfigurationCodeBuilder(entityCodeBuilder);
            entityConfigurationCodeBuilder.RootCodePath = tbCodeRootPath.Text;
            entityConfigurationCodeBuilder.ClassComments = tbComment.Text +  "基于数据库—" + _DbType.ToString() + "的映射";

            entityCodeBuilder.CreateCode();
            entityConfigurationCodeBuilder.CreateCode();

            //UbpDbContext
            UbpDbContextCodeBuilder ubpDbContextCodeBuilder = new UbpDbContextCodeBuilder(entityCodeBuilder, tbModuleName.Text);
            ubpDbContextCodeBuilder.RootCodePath = tbCodeRootPath.Text;
            ubpDbContextCodeBuilder.ClassComments = tbModuleName.Text + "的DbContext";
            ubpDbContextCodeBuilder.CreateCode();

            //Domain.Service.Interface
            DomainServiceInterfaceCodeBuilder domainServiceInterfaceCodeBuilder = new DomainServiceInterfaceCodeBuilder(entityCodeBuilder, tbFunName.Text);
            domainServiceInterfaceCodeBuilder.RootCodePath = tbCodeRootPath.Text;
            domainServiceInterfaceCodeBuilder.ClassName = domainServiceInterfaceCodeBuilder.FileName = "I" + tbDomainServiceClassName.Text;
            domainServiceInterfaceCodeBuilder.ClassComments = tbComment.Text + "的Domain.Service.Interface";
            domainServiceInterfaceCodeBuilder.Has_Method_GetAllAsync = cbGetAllAsync.Checked;
            domainServiceInterfaceCodeBuilder.Has_Method_GetByIdAsync = cbGetByIdAsync.Checked;
            domainServiceInterfaceCodeBuilder.Has_Method_CreateAsync = cbCreateAsync.Checked;
            domainServiceInterfaceCodeBuilder.Has_Method_UpdateAsync = cbUpdateAsync.Checked;
            domainServiceInterfaceCodeBuilder.Has_Method_DeleteAsync = cbDeleteAsync.Checked;
            domainServiceInterfaceCodeBuilder.CreateCode();

            //Domain.Service
            DomainServiceCodeBuilder domainServiceCodeBuilder = new DomainServiceCodeBuilder(entityCodeBuilder, 
                domainServiceInterfaceCodeBuilder, tbFunName.Text);
            domainServiceCodeBuilder.RootCodePath = tbCodeRootPath.Text;
            domainServiceCodeBuilder.ClassName = domainServiceCodeBuilder.FileName = tbDomainServiceClassName.Text;
            domainServiceCodeBuilder.ClassComments = tbComment.Text + "的Domain.Service";
            domainServiceCodeBuilder.CreateCode();

            //InputDto
            InputDtoCodeBuilder inputDtoCodeBuilder = new InputDtoCodeBuilder(entityCodeBuilder, tbInputDtoClassName.Text, tbFunName.Text);
            inputDtoCodeBuilder.RootCodePath = tbCodeRootPath.Text;
            inputDtoCodeBuilder.ClassComments = tbComment.Text + "的InputDto";
            inputDtoCodeBuilder.CreateCode();

            //OutputDto
            OutputDtoCodeBuilder outputDtoCodeBuilder = new OutputDtoCodeBuilder(entityCodeBuilder, tbOutputDtoClassName.Text, tbFunName.Text);
            outputDtoCodeBuilder.RootCodePath = tbCodeRootPath.Text;
            outputDtoCodeBuilder.ClassComments = tbComment.Text + "的OutputDto";
            outputDtoCodeBuilder.CreateCode();

            //Permission
            PermissionCodeBuilder permissionCodeBuilder = new PermissionCodeBuilder(tbModuleName.Text);
            permissionCodeBuilder.RootCodePath = tbCodeRootPath.Text;
            permissionCodeBuilder.ClassComments = tbModuleName.Text + "的Permission定义";
            permissionCodeBuilder.MenuPermValue = tbMenuPerm.Text;
            permissionCodeBuilder.PermCreateValue = tbPermCreate.Text;
            permissionCodeBuilder.PermEditValue = tbPermEdit.Text;
            permissionCodeBuilder.PermDeleteValue = tbPermDelete.Text;
            permissionCodeBuilder.CreateCode();


            //Application.Service.Interface
            AppServiceInterfaceCodeBuilder appServiceInterfaceCodeBuilder = new AppServiceInterfaceCodeBuilder(entityCodeBuilder,
                inputDtoCodeBuilder, outputDtoCodeBuilder, 
                domainServiceInterfaceCodeBuilder, "I" + tbAppServiceClassName.Text,
                tbFunName.Text);
            appServiceInterfaceCodeBuilder.RootCodePath = tbCodeRootPath.Text;
            appServiceInterfaceCodeBuilder.ClassComments = tbComment.Text + "的Application.Service.Interface";
            appServiceInterfaceCodeBuilder.CreateCode();

            //Application.Service
            AppServiceCodeBuilder appServiceCodeBuilder = new AppServiceCodeBuilder(entityCodeBuilder,
                inputDtoCodeBuilder, outputDtoCodeBuilder, domainServiceInterfaceCodeBuilder, appServiceInterfaceCodeBuilder,
                permissionCodeBuilder, tbAppServiceClassName.Text, tbFunName.Text);
            appServiceCodeBuilder.RootCodePath = tbCodeRootPath.Text;
            appServiceCodeBuilder.ClassComments = tbComment.Text + "的Application.Service";
            appServiceCodeBuilder.CreateCode();

            //Controller
            ControllerCodeBuilder controllerCodeBuilder = new ControllerCodeBuilder(inputDtoCodeBuilder, outputDtoCodeBuilder,
                appServiceInterfaceCodeBuilder, permissionCodeBuilder, tbControllerName.Text, tbModuleName.Text);
            controllerCodeBuilder.RootCodePath = tbCodeRootPath.Text;
            controllerCodeBuilder.ClassComments = tbComment.Text + "的Controller";
            controllerCodeBuilder.CreateCode();

            //Index.cshtml
            IndexCshtmlCodeBuilder indexCshtmlCodeBuilder = new IndexCshtmlCodeBuilder(permissionCodeBuilder,
                controllerCodeBuilder, tbModuleName.Text);
            indexCshtmlCodeBuilder.RootCodePath = tbCodeRootPath.Text;
            indexCshtmlCodeBuilder.CreateCode();

            //Index.js
            IndexJsCodeBuilder indexJsCodeBuilder = new IndexJsCodeBuilder(permissionCodeBuilder,
                controllerCodeBuilder, tbModuleName.Text);
            indexJsCodeBuilder.RootCodePath = tbCodeRootPath.Text;
            indexJsCodeBuilder.CreateCode();

            //_CreateOrEditModal.cshtml
            EditModalCshtmlCodeBuilder editModalCshtmlCodeBuilder = new EditModalCshtmlCodeBuilder(permissionCodeBuilder,
                controllerCodeBuilder, tbModuleName.Text);
            editModalCshtmlCodeBuilder.RootCodePath = tbCodeRootPath.Text;
            editModalCshtmlCodeBuilder.CreateCode();

            //_CreateOrEditModal.js
            EditModalJsCodeBuilder editModalJsCodeBuilder = new EditModalJsCodeBuilder(permissionCodeBuilder,
                controllerCodeBuilder, tbModuleName.Text);
            editModalJsCodeBuilder.RootCodePath = tbCodeRootPath.Text;
            editModalJsCodeBuilder.CreateCode();

            MessageBox.Show("OK");
        }

        private void tbClass_TextChanged(object sender, EventArgs e)
        {
            string classPlural = System.Data.Entity.Design.PluralizationServices.PluralizationService.CreateService(new CultureInfo("en")).Pluralize(tbClass.Text);

            tbFunName.Text = classPlural;

            tbDomainServiceClassName.Text = tbClass.Text + "Manager";
            tbAppServiceClassName.Text = tbClass.Text + "AppService";
            tbInputDtoClassName.Text = tbClass.Text + "InputDto";
            tbOutputDtoClassName.Text = tbClass.Text + "OutputDto";
            tbMenuPerm.Text = "Pages."
                + (string.IsNullOrEmpty(tbModuleName.Text) ? "" : tbModuleName.Text + ".")
                + (string.IsNullOrEmpty(tbSubModuleName.Text) ? "" : tbSubModuleName.Text + ".") 
                + classPlural;
            tbPermCreate.Text = tbMenuPerm.Text + ".Create";
            tbPermEdit.Text = tbMenuPerm.Text + ".Edit";
            tbPermDelete.Text = tbMenuPerm.Text + ".Delete";

            tbControllerName.Text = tbClass.Text;
        }

        private void tbComment_TextChanged(object sender, EventArgs e)
        {
            tbClassDesc.Text = tbComment.Text + "的实体类";
        }
    }
}
