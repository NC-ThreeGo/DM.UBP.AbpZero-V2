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
                        _DbType = DbType.DevartOracle;
                        sql = "select a.COLUMN_NAME name, a.DATA_TYPE type, "
                            + @" case when a.DATA_TYPE = 'VARCHAR2' then a.DATA_LENGTH / 2
                                        when a.DATA_TYPE = 'NUMBER' then a.DATA_PRECISION
                                        else a.DATA_LENGTH end length, " 
                            + " a.DATA_SCALE scale, "
                            + " case when a.nullable = 'Y' then 1 else 0 end nullable, 0 IsIdentity, 0 ispk, b.comments "
                            + " from user_TAB_COLUMNS a, user_col_comments b "
                            + " where a.table_name = b.table_name and a.column_name = b.column_name"
                            + " and a.TABLE_NAME ='" + cbTableName.Text + "'"
                            + " order by a.COLUMN_ID ";
                        break;
                    case "SqlConnection":
                        _DbType = DbType.SqlServer;
                        sql = @"SELECT col.name AS name, typ.name as type, col.max_length AS length, col.scale AS scale,
                                cast(col.is_nullable as bit) AS nullable, cast(col.is_identity as bit) AS IsIdentity,
                                cast(case when exists (SELECT 1 FROM sys.indexes idx
                                                        join sys.index_columns idxCol
                                                                on(idx.object_id = idxCol.object_id)
                                                        WHERE idx.object_id = col.object_id
                                                                AND idxCol.index_column_id = col.column_id
                                                                AND idx.is_primary_key = 1
                                                    ) THEN 1 ELSE 0 END as bit) AS IsPk,
	                                g.[value] comments
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
                    //gvFields.Columns[1].DataPropertyName = "TYPE";
                    //gvFields.Columns[2].DataPropertyName = "LENGTH";
                    //gvFields.Columns[3].DataPropertyName = "SCALE";
                    //gvFields.Columns[4].DataPropertyName = "Nullable";
                    //gvFields.Columns[5].DataPropertyName = "Comments";
                    //gvFields.Columns[6].DataPropertyName = "IsIdentity";
                    //gvFields.Columns[7].DataPropertyName = "IsPk";
                }
            }
        }

        private void btnCodeCreate_Click(object sender, EventArgs e)
        {
            List<Field> fields_Property = new List<Field>();

            foreach (DataGridViewRow row in gvFields.Rows)
            {
                Field f = new Field();
                f.Name = row.Cells[1].Value.ToString();
                f.Type = row.Cells[2].Value.ToString();
                f.Length = Convert.ToInt16(row.Cells[3].Value is null ? 0 : row.Cells[3].Value);
                f.Scale = Convert.ToByte(row.Cells[4].Value is null ? 0 : row.Cells[4].Value);
                f.IsIdentity = Convert.ToBoolean(row.Cells[5].Value is null ? false : row.Cells[5].Value);
                f.Nullable = Convert.ToBoolean(row.Cells[6].Value is null ? true : row.Cells[6].Value);
                f.Comments = row.Cells[7].Value is null ? "" : row.Cells[7].Value.ToString();
                f.IsPk = Convert.ToBoolean(row.Cells[8].Value is null ? false : row.Cells[8].Value);
                f.Property = row.Cells[9].Value is null ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(f.Name.ToLower()) : row.Cells[9].Value.ToString();

                fields_Property.Add(f);
            }

            EntityCodeBuilder entityCodeBuilder = new EntityCodeBuilder();
            entityCodeBuilder.DbType = _DbType;
            entityCodeBuilder.Fields = fields_Property;
            entityCodeBuilder.ModuleName = tbModuleName.Text;
            entityCodeBuilder.SubModuleName = tbSubModuleName.Text;
            entityCodeBuilder.FileName = tbFileName.Text;
            entityCodeBuilder.ClassName = tbClass.Text;
            entityCodeBuilder.ClassComments = tbClassDesc.Text;
            entityCodeBuilder.BaseClass = cbBaseClass.Text;
            entityCodeBuilder.PkType = cbPkType.Text;

            if (rbNone.Checked)
                entityCodeBuilder.TenantInterface = "";
            if (rbIMayHaveTenant.Checked)
                entityCodeBuilder.TenantInterface = "IMayHaveTenant";
            if (rbIMustHaveTenant.Checked)
                entityCodeBuilder.TenantInterface = "IMustHaveTenant";

            entityCodeBuilder.CreateCode();
        }
    }
}
