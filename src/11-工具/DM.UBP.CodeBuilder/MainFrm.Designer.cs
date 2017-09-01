namespace DM.UBP.CodeBuilder
{
    partial class MainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbClassDesc = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.gbLP = new System.Windows.Forms.GroupBox();
            this.tbLPNameSpace = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.tbGridView = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbLPMasterPageFile = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.clbLPActionList = new System.Windows.Forms.CheckedListBox();
            this.tbLPClassName = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.clbDPActionList = new System.Windows.Forms.CheckedListBox();
            this.tbDPClassName = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.tbDPNameSpace = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.tbDPMasterPageFile = new System.Windows.Forms.TextBox();
            this.gbDP = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.tpPage = new System.Windows.Forms.TabPage();
            this.gbClass = new System.Windows.Forms.GroupBox();
            this.rbIMustHaveTenant = new System.Windows.Forms.RadioButton();
            this.rbIMayHaveTenant = new System.Windows.Forms.RadioButton();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.cbPkType = new System.Windows.Forms.ComboBox();
            this.cbBaseClass = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tbEntityNS = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbSubModuleName = new System.Windows.Forms.TextBox();
            this.tbClass = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tbModuleName = new System.Windows.Forms.TextBox();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tpEntity = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btnRefreshCBB = new System.Windows.Forms.Button();
            this.btnCodeCreate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbCodeRootPath = new System.Windows.Forms.TextBox();
            this.cbTableName = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.btnConn = new System.Windows.Forms.Button();
            this.gbFields = new System.Windows.Forms.GroupBox();
            this.gvFields = new System.Windows.Forms.DataGridView();
            this.NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LENGTH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SCALE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nullable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsIdentity = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsPk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Property = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbLP.SuspendLayout();
            this.gbDP.SuspendLayout();
            this.tpPage.SuspendLayout();
            this.gbClass.SuspendLayout();
            this.tpEntity.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvFields)).BeginInit();
            this.SuspendLayout();
            // 
            // tbClassDesc
            // 
            this.tbClassDesc.Location = new System.Drawing.Point(97, 207);
            this.tbClassDesc.Margin = new System.Windows.Forms.Padding(6);
            this.tbClassDesc.Name = "tbClassDesc";
            this.tbClassDesc.Size = new System.Drawing.Size(631, 35);
            this.tbClassDesc.TabIndex = 16;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(1471, 113);
            this.splitter1.Margin = new System.Windows.Forms.Padding(6);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(6, 765);
            this.splitter1.TabIndex = 12;
            this.splitter1.TabStop = false;
            // 
            // gbLP
            // 
            this.gbLP.Controls.Add(this.tbLPNameSpace);
            this.gbLP.Controls.Add(this.label24);
            this.gbLP.Controls.Add(this.tbGridView);
            this.gbLP.Controls.Add(this.label19);
            this.gbLP.Controls.Add(this.tbLPMasterPageFile);
            this.gbLP.Controls.Add(this.label18);
            this.gbLP.Controls.Add(this.clbLPActionList);
            this.gbLP.Controls.Add(this.tbLPClassName);
            this.gbLP.Controls.Add(this.label21);
            this.gbLP.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbLP.Location = new System.Drawing.Point(4, 5);
            this.gbLP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLP.Name = "gbLP";
            this.gbLP.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLP.Size = new System.Drawing.Size(746, 413);
            this.gbLP.TabIndex = 6;
            this.gbLP.TabStop = false;
            this.gbLP.Text = "LP：";
            // 
            // tbLPNameSpace
            // 
            this.tbLPNameSpace.Location = new System.Drawing.Point(138, 24);
            this.tbLPNameSpace.Margin = new System.Windows.Forms.Padding(6);
            this.tbLPNameSpace.Name = "tbLPNameSpace";
            this.tbLPNameSpace.Size = new System.Drawing.Size(592, 35);
            this.tbLPNameSpace.TabIndex = 17;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 32);
            this.label24.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(142, 24);
            this.label24.TabIndex = 18;
            this.label24.Text = "NameSpace：";
            // 
            // tbGridView
            // 
            this.tbGridView.Location = new System.Drawing.Point(114, 360);
            this.tbGridView.Margin = new System.Windows.Forms.Padding(6);
            this.tbGridView.Name = "tbGridView";
            this.tbGridView.Size = new System.Drawing.Size(613, 35);
            this.tbGridView.TabIndex = 15;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 365);
            this.label19.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(118, 24);
            this.label19.TabIndex = 16;
            this.label19.Text = "GriView：";
            // 
            // tbLPMasterPageFile
            // 
            this.tbLPMasterPageFile.Location = new System.Drawing.Point(196, 126);
            this.tbLPMasterPageFile.Margin = new System.Windows.Forms.Padding(6);
            this.tbLPMasterPageFile.Name = "tbLPMasterPageFile";
            this.tbLPMasterPageFile.Size = new System.Drawing.Size(534, 35);
            this.tbLPMasterPageFile.TabIndex = 13;
            this.tbLPMasterPageFile.Text = "~/MasterPage/ListPage.Master";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 131);
            this.label18.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(202, 24);
            this.label18.TabIndex = 14;
            this.label18.Text = "MasterPageFile：";
            // 
            // clbLPActionList
            // 
            this.clbLPActionList.FormattingEnabled = true;
            this.clbLPActionList.Items.AddRange(new object[] {
            "Query",
            "Detail",
            "Add",
            "Update",
            "Delete",
            "Submit",
            "Approve1",
            "Approve2",
            "ExpToExcel",
            "Attachment",
            "Disable",
            "Suspend"});
            this.clbLPActionList.Location = new System.Drawing.Point(10, 184);
            this.clbLPActionList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clbLPActionList.MultiColumn = true;
            this.clbLPActionList.Name = "clbLPActionList";
            this.clbLPActionList.Size = new System.Drawing.Size(720, 154);
            this.clbLPActionList.TabIndex = 12;
            // 
            // tbLPClassName
            // 
            this.tbLPClassName.Location = new System.Drawing.Point(76, 74);
            this.tbLPClassName.Margin = new System.Windows.Forms.Padding(6);
            this.tbLPClassName.Name = "tbLPClassName";
            this.tbLPClassName.Size = new System.Drawing.Size(654, 35);
            this.tbLPClassName.TabIndex = 10;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 78);
            this.label21.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(82, 24);
            this.label21.TabIndex = 11;
            this.label21.Text = "类名：";
            // 
            // clbDPActionList
            // 
            this.clbDPActionList.Enabled = false;
            this.clbDPActionList.FormattingEnabled = true;
            this.clbDPActionList.Items.AddRange(new object[] {
            "Query",
            "Detail",
            "Add",
            "Update",
            "Delete",
            "Submit",
            "Approve1",
            "Approve2",
            "ExpToExcel",
            "Attachment",
            "Disable",
            "Suspend"});
            this.clbDPActionList.Location = new System.Drawing.Point(18, 189);
            this.clbDPActionList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clbDPActionList.MultiColumn = true;
            this.clbDPActionList.Name = "clbDPActionList";
            this.clbDPActionList.Size = new System.Drawing.Size(720, 154);
            this.clbDPActionList.TabIndex = 14;
            // 
            // tbDPClassName
            // 
            this.tbDPClassName.Location = new System.Drawing.Point(81, 85);
            this.tbDPClassName.Margin = new System.Windows.Forms.Padding(6);
            this.tbDPClassName.Name = "tbDPClassName";
            this.tbDPClassName.Size = new System.Drawing.Size(650, 35);
            this.tbDPClassName.TabIndex = 12;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(10, 90);
            this.label22.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(82, 24);
            this.label22.TabIndex = 13;
            this.label22.Text = "类名：";
            // 
            // tbDPNameSpace
            // 
            this.tbDPNameSpace.Location = new System.Drawing.Point(138, 32);
            this.tbDPNameSpace.Margin = new System.Windows.Forms.Padding(6);
            this.tbDPNameSpace.Name = "tbDPNameSpace";
            this.tbDPNameSpace.Size = new System.Drawing.Size(595, 35);
            this.tbDPNameSpace.TabIndex = 19;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(9, 40);
            this.label25.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(142, 24);
            this.label25.TabIndex = 20;
            this.label25.Text = "NameSpace：";
            // 
            // tbDPMasterPageFile
            // 
            this.tbDPMasterPageFile.Location = new System.Drawing.Point(201, 138);
            this.tbDPMasterPageFile.Margin = new System.Windows.Forms.Padding(6);
            this.tbDPMasterPageFile.Name = "tbDPMasterPageFile";
            this.tbDPMasterPageFile.Size = new System.Drawing.Size(534, 35);
            this.tbDPMasterPageFile.TabIndex = 15;
            this.tbDPMasterPageFile.Text = "~/MasterPage/DetailsPage.Master";
            // 
            // gbDP
            // 
            this.gbDP.Controls.Add(this.tbDPNameSpace);
            this.gbDP.Controls.Add(this.label25);
            this.gbDP.Controls.Add(this.tbDPMasterPageFile);
            this.gbDP.Controls.Add(this.label23);
            this.gbDP.Controls.Add(this.clbDPActionList);
            this.gbDP.Controls.Add(this.tbDPClassName);
            this.gbDP.Controls.Add(this.label22);
            this.gbDP.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDP.Location = new System.Drawing.Point(4, 418);
            this.gbDP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbDP.Name = "gbDP";
            this.gbDP.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbDP.Size = new System.Drawing.Size(746, 381);
            this.gbDP.TabIndex = 8;
            this.gbDP.TabStop = false;
            this.gbDP.Text = "DP：";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(10, 142);
            this.label23.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(202, 24);
            this.label23.TabIndex = 16;
            this.label23.Text = "MasterPageFile：";
            // 
            // tpPage
            // 
            this.tpPage.Controls.Add(this.gbDP);
            this.tpPage.Controls.Add(this.gbLP);
            this.tpPage.Location = new System.Drawing.Point(8, 39);
            this.tpPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpPage.Name = "tpPage";
            this.tpPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpPage.Size = new System.Drawing.Size(754, 718);
            this.tpPage.TabIndex = 1;
            this.tpPage.Text = "Web";
            this.tpPage.UseVisualStyleBackColor = true;
            // 
            // gbClass
            // 
            this.gbClass.Controls.Add(this.rbIMustHaveTenant);
            this.gbClass.Controls.Add(this.rbIMayHaveTenant);
            this.gbClass.Controls.Add(this.rbNone);
            this.gbClass.Controls.Add(this.cbPkType);
            this.gbClass.Controls.Add(this.cbBaseClass);
            this.gbClass.Controls.Add(this.tbClassDesc);
            this.gbClass.Controls.Add(this.label8);
            this.gbClass.Controls.Add(this.label5);
            this.gbClass.Controls.Add(this.label1);
            this.gbClass.Controls.Add(this.label16);
            this.gbClass.Controls.Add(this.tbEntityNS);
            this.gbClass.Controls.Add(this.label14);
            this.gbClass.Controls.Add(this.tbSubModuleName);
            this.gbClass.Controls.Add(this.tbClass);
            this.gbClass.Controls.Add(this.label6);
            this.gbClass.Controls.Add(this.label15);
            this.gbClass.Controls.Add(this.tbModuleName);
            this.gbClass.Controls.Add(this.tbFileName);
            this.gbClass.Controls.Add(this.label2);
            this.gbClass.Controls.Add(this.label3);
            this.gbClass.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbClass.Location = new System.Drawing.Point(4, 5);
            this.gbClass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbClass.Name = "gbClass";
            this.gbClass.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbClass.Size = new System.Drawing.Size(746, 691);
            this.gbClass.TabIndex = 5;
            this.gbClass.TabStop = false;
            this.gbClass.Text = "类名：";
            // 
            // rbIMustHaveTenant
            // 
            this.rbIMustHaveTenant.AutoSize = true;
            this.rbIMustHaveTenant.Location = new System.Drawing.Point(507, 394);
            this.rbIMustHaveTenant.Name = "rbIMustHaveTenant";
            this.rbIMustHaveTenant.Size = new System.Drawing.Size(221, 28);
            this.rbIMustHaveTenant.TabIndex = 19;
            this.rbIMustHaveTenant.TabStop = true;
            this.rbIMustHaveTenant.Text = "IMustHaveTenant";
            this.rbIMustHaveTenant.UseVisualStyleBackColor = true;
            // 
            // rbIMayHaveTenant
            // 
            this.rbIMayHaveTenant.AutoSize = true;
            this.rbIMayHaveTenant.Location = new System.Drawing.Point(269, 394);
            this.rbIMayHaveTenant.Name = "rbIMayHaveTenant";
            this.rbIMayHaveTenant.Size = new System.Drawing.Size(209, 28);
            this.rbIMayHaveTenant.TabIndex = 19;
            this.rbIMayHaveTenant.TabStop = true;
            this.rbIMayHaveTenant.Text = "IMayHaveTenant";
            this.rbIMayHaveTenant.UseVisualStyleBackColor = true;
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Location = new System.Drawing.Point(158, 394);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(65, 28);
            this.rbNone.TabIndex = 19;
            this.rbNone.TabStop = true;
            this.rbNone.Text = "空";
            this.rbNone.UseVisualStyleBackColor = true;
            // 
            // cbPkType
            // 
            this.cbPkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPkType.FormattingEnabled = true;
            this.cbPkType.Items.AddRange(new object[] {
            "int",
            "long",
            "Guid"});
            this.cbPkType.Location = new System.Drawing.Point(125, 332);
            this.cbPkType.Margin = new System.Windows.Forms.Padding(6);
            this.cbPkType.Name = "cbPkType";
            this.cbPkType.Size = new System.Drawing.Size(603, 32);
            this.cbPkType.TabIndex = 18;
            // 
            // cbBaseClass
            // 
            this.cbBaseClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaseClass.FormattingEnabled = true;
            this.cbBaseClass.Items.AddRange(new object[] {
            "FullAuditedEntity",
            "AuditedEntity",
            "CreationAuditedEntity",
            "Entity"});
            this.cbBaseClass.Location = new System.Drawing.Point(97, 275);
            this.cbBaseClass.Margin = new System.Windows.Forms.Padding(6);
            this.cbBaseClass.Name = "cbBaseClass";
            this.cbBaseClass.Size = new System.Drawing.Size(631, 32);
            this.cbBaseClass.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 396);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(154, 24);
            this.label8.TabIndex = 17;
            this.label8.Text = "多租户接口：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 340);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 24);
            this.label5.TabIndex = 17;
            this.label5.Text = "主键类型：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 283);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 17;
            this.label1.Text = "基类：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(4, 207);
            this.label16.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(106, 24);
            this.label16.TabIndex = 17;
            this.label16.Text = "类说明：";
            // 
            // tbEntityNS
            // 
            this.tbEntityNS.Location = new System.Drawing.Point(133, 89);
            this.tbEntityNS.Margin = new System.Windows.Forms.Padding(6);
            this.tbEntityNS.Name = "tbEntityNS";
            this.tbEntityNS.Size = new System.Drawing.Size(595, 35);
            this.tbEntityNS.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(4, 89);
            this.label14.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(142, 24);
            this.label14.TabIndex = 15;
            this.label14.Text = "NameSpace：";
            // 
            // tbSubModuleName
            // 
            this.tbSubModuleName.Location = new System.Drawing.Point(461, 33);
            this.tbSubModuleName.Margin = new System.Windows.Forms.Padding(6);
            this.tbSubModuleName.Name = "tbSubModuleName";
            this.tbSubModuleName.Size = new System.Drawing.Size(267, 35);
            this.tbSubModuleName.TabIndex = 12;
            this.tbSubModuleName.TextChanged += new System.EventHandler(this.tbModuleName_TextChanged);
            // 
            // tbClass
            // 
            this.tbClass.Location = new System.Drawing.Point(422, 148);
            this.tbClass.Margin = new System.Windows.Forms.Padding(6);
            this.tbClass.Name = "tbClass";
            this.tbClass.Size = new System.Drawing.Size(306, 35);
            this.tbClass.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(348, 40);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 24);
            this.label6.TabIndex = 13;
            this.label6.Text = "子模块名：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(350, 153);
            this.label15.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 24);
            this.label15.TabIndex = 13;
            this.label15.Text = "类名：";
            // 
            // tbModuleName
            // 
            this.tbModuleName.Location = new System.Drawing.Point(97, 33);
            this.tbModuleName.Margin = new System.Windows.Forms.Padding(6);
            this.tbModuleName.Name = "tbModuleName";
            this.tbModuleName.Size = new System.Drawing.Size(236, 35);
            this.tbModuleName.TabIndex = 10;
            this.tbModuleName.TextChanged += new System.EventHandler(this.tbModuleName_TextChanged);
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(99, 148);
            this.tbFileName.Margin = new System.Windows.Forms.Padding(6);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(236, 35);
            this.tbFileName.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 39);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "模块名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 153);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 24);
            this.label3.TabIndex = 11;
            this.label3.Text = "文件名：";
            // 
            // tpEntity
            // 
            this.tpEntity.AutoScroll = true;
            this.tpEntity.Controls.Add(this.gbClass);
            this.tpEntity.Location = new System.Drawing.Point(8, 39);
            this.tpEntity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpEntity.Name = "tpEntity";
            this.tpEntity.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpEntity.Size = new System.Drawing.Size(754, 718);
            this.tpEntity.TabIndex = 0;
            this.tpEntity.Text = "Entity";
            this.tpEntity.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpEntity);
            this.tabControl1.Controls.Add(this.tpPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabControl1.Location = new System.Drawing.Point(1477, 113);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(770, 765);
            this.tabControl1.TabIndex = 11;
            // 
            // btnRefreshCBB
            // 
            this.btnRefreshCBB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshCBB.Location = new System.Drawing.Point(1821, 13);
            this.btnRefreshCBB.Margin = new System.Windows.Forms.Padding(6);
            this.btnRefreshCBB.Name = "btnRefreshCBB";
            this.btnRefreshCBB.Size = new System.Drawing.Size(196, 46);
            this.btnRefreshCBB.TabIndex = 1;
            this.btnRefreshCBB.Text = "刷新ComboBox";
            this.btnRefreshCBB.UseVisualStyleBackColor = true;
            // 
            // btnCodeCreate
            // 
            this.btnCodeCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCodeCreate.Location = new System.Drawing.Point(2073, 13);
            this.btnCodeCreate.Margin = new System.Windows.Forms.Padding(6);
            this.btnCodeCreate.Name = "btnCodeCreate";
            this.btnCodeCreate.Size = new System.Drawing.Size(150, 46);
            this.btnCodeCreate.TabIndex = 0;
            this.btnCodeCreate.Text = "生成代码";
            this.btnCodeCreate.UseVisualStyleBackColor = true;
            this.btnCodeCreate.Click += new System.EventHandler(this.btnCodeCreate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 24);
            this.label4.TabIndex = 0;
            this.label4.Text = "label4";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnRefreshCBB);
            this.panel2.Controls.Add(this.btnCodeCreate);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 878);
            this.panel2.Margin = new System.Windows.Forms.Padding(6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(2247, 72);
            this.panel2.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbCodeRootPath);
            this.panel1.Controls.Add(this.cbTableName);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label29);
            this.panel1.Controls.Add(this.btnConn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2247, 113);
            this.panel1.TabIndex = 9;
            // 
            // tbCodeRootPath
            // 
            this.tbCodeRootPath.Location = new System.Drawing.Point(189, 15);
            this.tbCodeRootPath.Margin = new System.Windows.Forms.Padding(6);
            this.tbCodeRootPath.Name = "tbCodeRootPath";
            this.tbCodeRootPath.Size = new System.Drawing.Size(2028, 35);
            this.tbCodeRootPath.TabIndex = 16;
            // 
            // cbTableName
            // 
            this.cbTableName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTableName.FormattingEnabled = true;
            this.cbTableName.Location = new System.Drawing.Point(263, 69);
            this.cbTableName.Margin = new System.Windows.Forms.Padding(6);
            this.cbTableName.Name = "cbTableName";
            this.cbTableName.Size = new System.Drawing.Size(502, 32);
            this.cbTableName.TabIndex = 15;
            this.cbTableName.SelectedIndexChanged += new System.EventHandler(this.cbTableName_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 21);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(178, 24);
            this.label7.TabIndex = 14;
            this.label7.Text = "代码的根目录：";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(185, 74);
            this.label29.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(82, 24);
            this.label29.TabIndex = 14;
            this.label29.Text = "表名：";
            // 
            // btnConn
            // 
            this.btnConn.Location = new System.Drawing.Point(18, 64);
            this.btnConn.Margin = new System.Windows.Forms.Padding(6);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(128, 39);
            this.btnConn.TabIndex = 13;
            this.btnConn.Text = "连接";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // gbFields
            // 
            this.gbFields.Controls.Add(this.gvFields);
            this.gbFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFields.Location = new System.Drawing.Point(0, 113);
            this.gbFields.Margin = new System.Windows.Forms.Padding(6);
            this.gbFields.Name = "gbFields";
            this.gbFields.Padding = new System.Windows.Forms.Padding(6);
            this.gbFields.Size = new System.Drawing.Size(1471, 765);
            this.gbFields.TabIndex = 13;
            this.gbFields.TabStop = false;
            this.gbFields.Text = "字段：";
            // 
            // gvFields
            // 
            this.gvFields.AllowUserToAddRows = false;
            this.gvFields.AllowUserToDeleteRows = false;
            this.gvFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NAME,
            this.TYPE,
            this.LENGTH,
            this.SCALE,
            this.Nullable,
            this.comments,
            this.IsIdentity,
            this.IsPk,
            this.Property});
            this.gvFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvFields.Location = new System.Drawing.Point(6, 34);
            this.gvFields.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.gvFields.Name = "gvFields";
            this.gvFields.RowTemplate.Height = 23;
            this.gvFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvFields.Size = new System.Drawing.Size(1459, 725);
            this.gvFields.TabIndex = 0;
            // 
            // NAME
            // 
            this.NAME.DataPropertyName = "NAME";
            this.NAME.HeaderText = "字段名";
            this.NAME.Name = "NAME";
            this.NAME.Width = 140;
            // 
            // TYPE
            // 
            this.TYPE.DataPropertyName = "TYPE";
            this.TYPE.HeaderText = "字段类型";
            this.TYPE.Name = "TYPE";
            this.TYPE.Width = 160;
            // 
            // LENGTH
            // 
            this.LENGTH.DataPropertyName = "LENGTH";
            this.LENGTH.HeaderText = "长度";
            this.LENGTH.Name = "LENGTH";
            this.LENGTH.Width = 70;
            // 
            // SCALE
            // 
            this.SCALE.DataPropertyName = "SCALE";
            this.SCALE.HeaderText = "小数位";
            this.SCALE.Name = "SCALE";
            this.SCALE.Width = 140;
            // 
            // Nullable
            // 
            this.Nullable.DataPropertyName = "Nullable";
            this.Nullable.HeaderText = "IsNull";
            this.Nullable.Name = "Nullable";
            // 
            // comments
            // 
            this.comments.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.comments.DataPropertyName = "comments";
            this.comments.HeaderText = "字段说明";
            this.comments.Name = "comments";
            this.comments.Width = 118;
            // 
            // IsIdentity
            // 
            this.IsIdentity.DataPropertyName = "IsIdentity";
            this.IsIdentity.HeaderText = "自增";
            this.IsIdentity.Name = "IsIdentity";
            // 
            // IsPk
            // 
            this.IsPk.DataPropertyName = "IsPk";
            this.IsPk.HeaderText = "PK?";
            this.IsPk.Name = "IsPk";
            this.IsPk.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsPk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Property
            // 
            this.Property.HeaderText = "类中的属性名";
            this.Property.Name = "Property";
            this.Property.Width = 160;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2247, 950);
            this.Controls.Add(this.gbFields);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainFrm";
            this.Text = "DM.UBP代码生成器";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.gbLP.ResumeLayout(false);
            this.gbLP.PerformLayout();
            this.gbDP.ResumeLayout(false);
            this.gbDP.PerformLayout();
            this.tpPage.ResumeLayout(false);
            this.gbClass.ResumeLayout(false);
            this.gbClass.PerformLayout();
            this.tpEntity.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbFields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvFields)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbClassDesc;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.GroupBox gbLP;
        private System.Windows.Forms.TextBox tbLPNameSpace;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox tbGridView;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbLPMasterPageFile;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckedListBox clbLPActionList;
        private System.Windows.Forms.TextBox tbLPClassName;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckedListBox clbDPActionList;
        private System.Windows.Forms.TextBox tbDPClassName;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox tbDPNameSpace;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tbDPMasterPageFile;
        private System.Windows.Forms.GroupBox gbDP;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TabPage tpPage;
        private System.Windows.Forms.GroupBox gbClass;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbEntityNS;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbClass;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tpEntity;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnRefreshCBB;
        private System.Windows.Forms.Button btnCodeCreate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.ComboBox cbTableName;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.GroupBox gbFields;
        private System.Windows.Forms.RadioButton rbIMustHaveTenant;
        private System.Windows.Forms.RadioButton rbIMayHaveTenant;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.ComboBox cbBaseClass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPkType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbSubModuleName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbModuleName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView gvFields;
        private System.Windows.Forms.TextBox tbCodeRootPath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn LENGTH;
        private System.Windows.Forms.DataGridViewTextBoxColumn SCALE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nullable;
        private System.Windows.Forms.DataGridViewTextBoxColumn comments;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsIdentity;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsPk;
        private System.Windows.Forms.DataGridViewTextBoxColumn Property;
    }
}

