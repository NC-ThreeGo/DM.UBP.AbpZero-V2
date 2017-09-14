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
            this.tpWebView = new System.Windows.Forms.TabPage();
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
            this.tpService = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbAppServiceNS = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.tbAppServiceClassName = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.tbPermDelete = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.tbPermEdit = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbPermCreate = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbMenuPerm = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbOutputDtoClassName = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.tbInputDtoClassName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbDeleteAsync = new System.Windows.Forms.CheckBox();
            this.cbUpdateAsync = new System.Windows.Forms.CheckBox();
            this.cbCreateAsync = new System.Windows.Forms.CheckBox();
            this.cbGetByIdAsync = new System.Windows.Forms.CheckBox();
            this.cbGetAllAsync = new System.Windows.Forms.CheckBox();
            this.tbDomainServiceNS = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.tbDomainServiceClassName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbFunName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tpController = new System.Windows.Forms.TabPage();
            this.tbControllerName = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.btnRefreshCBB = new System.Windows.Forms.Button();
            this.btnCodeCreate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.tbCodeRootPath = new System.Windows.Forms.TextBox();
            this.cbTableName = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
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
            this.HasInputDto = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.HasOutputDto = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TableColWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsEdit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.gbLP.SuspendLayout();
            this.gbDP.SuspendLayout();
            this.tpWebView.SuspendLayout();
            this.gbClass.SuspendLayout();
            this.tpEntity.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpService.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tpController.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvFields)).BeginInit();
            this.SuspendLayout();
            // 
            // tbClassDesc
            // 
            this.tbClassDesc.Location = new System.Drawing.Point(48, 104);
            this.tbClassDesc.Name = "tbClassDesc";
            this.tbClassDesc.Size = new System.Drawing.Size(318, 21);
            this.tbClassDesc.TabIndex = 16;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(546, 56);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 417);
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
            this.gbLP.Location = new System.Drawing.Point(2, 2);
            this.gbLP.Margin = new System.Windows.Forms.Padding(2);
            this.gbLP.Name = "gbLP";
            this.gbLP.Padding = new System.Windows.Forms.Padding(2);
            this.gbLP.Size = new System.Drawing.Size(373, 206);
            this.gbLP.TabIndex = 6;
            this.gbLP.TabStop = false;
            this.gbLP.Text = "LP：";
            // 
            // tbLPNameSpace
            // 
            this.tbLPNameSpace.Location = new System.Drawing.Point(69, 12);
            this.tbLPNameSpace.Name = "tbLPNameSpace";
            this.tbLPNameSpace.Size = new System.Drawing.Size(298, 21);
            this.tbLPNameSpace.TabIndex = 17;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(3, 16);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(71, 12);
            this.label24.TabIndex = 18;
            this.label24.Text = "NameSpace：";
            // 
            // tbGridView
            // 
            this.tbGridView.Location = new System.Drawing.Point(57, 180);
            this.tbGridView.Name = "tbGridView";
            this.tbGridView.Size = new System.Drawing.Size(308, 21);
            this.tbGridView.TabIndex = 15;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 182);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(59, 12);
            this.label19.TabIndex = 16;
            this.label19.Text = "GriView：";
            // 
            // tbLPMasterPageFile
            // 
            this.tbLPMasterPageFile.Location = new System.Drawing.Point(98, 63);
            this.tbLPMasterPageFile.Name = "tbLPMasterPageFile";
            this.tbLPMasterPageFile.Size = new System.Drawing.Size(269, 21);
            this.tbLPMasterPageFile.TabIndex = 13;
            this.tbLPMasterPageFile.Text = "~/MasterPage/ListPage.Master";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(3, 66);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(101, 12);
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
            this.clbLPActionList.Location = new System.Drawing.Point(5, 92);
            this.clbLPActionList.Margin = new System.Windows.Forms.Padding(2);
            this.clbLPActionList.MultiColumn = true;
            this.clbLPActionList.Name = "clbLPActionList";
            this.clbLPActionList.Size = new System.Drawing.Size(362, 68);
            this.clbLPActionList.TabIndex = 12;
            // 
            // tbLPClassName
            // 
            this.tbLPClassName.Location = new System.Drawing.Point(38, 37);
            this.tbLPClassName.Name = "tbLPClassName";
            this.tbLPClassName.Size = new System.Drawing.Size(329, 21);
            this.tbLPClassName.TabIndex = 10;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(3, 39);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(41, 12);
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
            this.clbDPActionList.Location = new System.Drawing.Point(9, 94);
            this.clbDPActionList.Margin = new System.Windows.Forms.Padding(2);
            this.clbDPActionList.MultiColumn = true;
            this.clbDPActionList.Name = "clbDPActionList";
            this.clbDPActionList.Size = new System.Drawing.Size(362, 68);
            this.clbDPActionList.TabIndex = 14;
            // 
            // tbDPClassName
            // 
            this.tbDPClassName.Location = new System.Drawing.Point(40, 42);
            this.tbDPClassName.Name = "tbDPClassName";
            this.tbDPClassName.Size = new System.Drawing.Size(327, 21);
            this.tbDPClassName.TabIndex = 12;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(5, 45);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 12);
            this.label22.TabIndex = 13;
            this.label22.Text = "类名：";
            // 
            // tbDPNameSpace
            // 
            this.tbDPNameSpace.Location = new System.Drawing.Point(69, 16);
            this.tbDPNameSpace.Name = "tbDPNameSpace";
            this.tbDPNameSpace.Size = new System.Drawing.Size(300, 21);
            this.tbDPNameSpace.TabIndex = 19;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(4, 20);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(71, 12);
            this.label25.TabIndex = 20;
            this.label25.Text = "NameSpace：";
            // 
            // tbDPMasterPageFile
            // 
            this.tbDPMasterPageFile.Location = new System.Drawing.Point(100, 69);
            this.tbDPMasterPageFile.Name = "tbDPMasterPageFile";
            this.tbDPMasterPageFile.Size = new System.Drawing.Size(269, 21);
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
            this.gbDP.Location = new System.Drawing.Point(2, 208);
            this.gbDP.Margin = new System.Windows.Forms.Padding(2);
            this.gbDP.Name = "gbDP";
            this.gbDP.Padding = new System.Windows.Forms.Padding(2);
            this.gbDP.Size = new System.Drawing.Size(373, 190);
            this.gbDP.TabIndex = 8;
            this.gbDP.TabStop = false;
            this.gbDP.Text = "DP：";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(5, 71);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(101, 12);
            this.label23.TabIndex = 16;
            this.label23.Text = "MasterPageFile：";
            // 
            // tpWebView
            // 
            this.tpWebView.Controls.Add(this.gbDP);
            this.tpWebView.Controls.Add(this.gbLP);
            this.tpWebView.Location = new System.Drawing.Point(4, 22);
            this.tpWebView.Margin = new System.Windows.Forms.Padding(2);
            this.tpWebView.Name = "tpWebView";
            this.tpWebView.Padding = new System.Windows.Forms.Padding(2);
            this.tpWebView.Size = new System.Drawing.Size(377, 391);
            this.tpWebView.TabIndex = 1;
            this.tpWebView.Text = "WebView";
            this.tpWebView.UseVisualStyleBackColor = true;
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
            this.gbClass.Location = new System.Drawing.Point(2, 2);
            this.gbClass.Margin = new System.Windows.Forms.Padding(2);
            this.gbClass.Name = "gbClass";
            this.gbClass.Padding = new System.Windows.Forms.Padding(2);
            this.gbClass.Size = new System.Drawing.Size(373, 346);
            this.gbClass.TabIndex = 5;
            this.gbClass.TabStop = false;
            this.gbClass.Text = "类名：";
            // 
            // rbIMustHaveTenant
            // 
            this.rbIMustHaveTenant.AutoSize = true;
            this.rbIMustHaveTenant.Location = new System.Drawing.Point(254, 197);
            this.rbIMustHaveTenant.Margin = new System.Windows.Forms.Padding(2);
            this.rbIMustHaveTenant.Name = "rbIMustHaveTenant";
            this.rbIMustHaveTenant.Size = new System.Drawing.Size(113, 16);
            this.rbIMustHaveTenant.TabIndex = 19;
            this.rbIMustHaveTenant.TabStop = true;
            this.rbIMustHaveTenant.Text = "IMustHaveTenant";
            this.rbIMustHaveTenant.UseVisualStyleBackColor = true;
            // 
            // rbIMayHaveTenant
            // 
            this.rbIMayHaveTenant.AutoSize = true;
            this.rbIMayHaveTenant.Location = new System.Drawing.Point(134, 197);
            this.rbIMayHaveTenant.Margin = new System.Windows.Forms.Padding(2);
            this.rbIMayHaveTenant.Name = "rbIMayHaveTenant";
            this.rbIMayHaveTenant.Size = new System.Drawing.Size(107, 16);
            this.rbIMayHaveTenant.TabIndex = 19;
            this.rbIMayHaveTenant.TabStop = true;
            this.rbIMayHaveTenant.Text = "IMayHaveTenant";
            this.rbIMayHaveTenant.UseVisualStyleBackColor = true;
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Location = new System.Drawing.Point(79, 197);
            this.rbNone.Margin = new System.Windows.Forms.Padding(2);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(35, 16);
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
            this.cbPkType.Location = new System.Drawing.Point(62, 166);
            this.cbPkType.Name = "cbPkType";
            this.cbPkType.Size = new System.Drawing.Size(304, 20);
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
            this.cbBaseClass.Location = new System.Drawing.Point(48, 138);
            this.cbBaseClass.Name = "cbBaseClass";
            this.cbBaseClass.Size = new System.Drawing.Size(318, 20);
            this.cbBaseClass.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 198);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "多租户接口：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "主键类型：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "基类：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(2, 104);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 17;
            this.label16.Text = "类说明：";
            // 
            // tbEntityNS
            // 
            this.tbEntityNS.Location = new System.Drawing.Point(66, 44);
            this.tbEntityNS.Name = "tbEntityNS";
            this.tbEntityNS.Size = new System.Drawing.Size(300, 21);
            this.tbEntityNS.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(2, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 12);
            this.label14.TabIndex = 15;
            this.label14.Text = "NameSpace：";
            // 
            // tbSubModuleName
            // 
            this.tbSubModuleName.Location = new System.Drawing.Point(230, 16);
            this.tbSubModuleName.Name = "tbSubModuleName";
            this.tbSubModuleName.Size = new System.Drawing.Size(136, 21);
            this.tbSubModuleName.TabIndex = 12;
            this.tbSubModuleName.TextChanged += new System.EventHandler(this.tbModuleName_TextChanged);
            // 
            // tbClass
            // 
            this.tbClass.Location = new System.Drawing.Point(211, 74);
            this.tbClass.Name = "tbClass";
            this.tbClass.Size = new System.Drawing.Size(155, 21);
            this.tbClass.TabIndex = 12;
            this.tbClass.TextChanged += new System.EventHandler(this.tbClass_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(174, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "子模块名：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(175, 76);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 13;
            this.label15.Text = "类名：";
            // 
            // tbModuleName
            // 
            this.tbModuleName.Location = new System.Drawing.Point(48, 16);
            this.tbModuleName.Name = "tbModuleName";
            this.tbModuleName.Size = new System.Drawing.Size(120, 21);
            this.tbModuleName.TabIndex = 10;
            this.tbModuleName.TextChanged += new System.EventHandler(this.tbModuleName_TextChanged);
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(50, 74);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(120, 21);
            this.tbFileName.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "模块名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "文件名：";
            // 
            // tpEntity
            // 
            this.tpEntity.AutoScroll = true;
            this.tpEntity.Controls.Add(this.gbClass);
            this.tpEntity.Location = new System.Drawing.Point(4, 22);
            this.tpEntity.Margin = new System.Windows.Forms.Padding(2);
            this.tpEntity.Name = "tpEntity";
            this.tpEntity.Padding = new System.Windows.Forms.Padding(2);
            this.tpEntity.Size = new System.Drawing.Size(377, 391);
            this.tpEntity.TabIndex = 0;
            this.tpEntity.Text = "Entity";
            this.tpEntity.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpEntity);
            this.tabControl1.Controls.Add(this.tpService);
            this.tabControl1.Controls.Add(this.tpController);
            this.tabControl1.Controls.Add(this.tpWebView);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabControl1.Location = new System.Drawing.Point(549, 56);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(385, 417);
            this.tabControl1.TabIndex = 11;
            // 
            // tpService
            // 
            this.tpService.Controls.Add(this.groupBox2);
            this.tpService.Controls.Add(this.groupBox1);
            this.tpService.Controls.Add(this.tbFunName);
            this.tpService.Controls.Add(this.label9);
            this.tpService.Location = new System.Drawing.Point(4, 22);
            this.tpService.Margin = new System.Windows.Forms.Padding(2);
            this.tpService.Name = "tpService";
            this.tpService.Padding = new System.Windows.Forms.Padding(2);
            this.tpService.Size = new System.Drawing.Size(377, 391);
            this.tpService.TabIndex = 2;
            this.tpService.Text = "Service";
            this.tpService.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tbAppServiceNS);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Controls.Add(this.tbAppServiceClassName);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.tbPermDelete);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.tbPermEdit);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.tbPermCreate);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.tbMenuPerm);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.tbOutputDtoClassName);
            this.groupBox2.Controls.Add(this.label31);
            this.groupBox2.Controls.Add(this.tbInputDtoClassName);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Location = new System.Drawing.Point(7, 170);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(359, 214);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "AppService";
            // 
            // tbAppServiceNS
            // 
            this.tbAppServiceNS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAppServiceNS.Location = new System.Drawing.Point(82, 18);
            this.tbAppServiceNS.Name = "tbAppServiceNS";
            this.tbAppServiceNS.Size = new System.Drawing.Size(262, 21);
            this.tbAppServiceNS.TabIndex = 29;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(8, 20);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(71, 12);
            this.label30.TabIndex = 28;
            this.label30.Text = "NameSpace：";
            // 
            // tbAppServiceClassName
            // 
            this.tbAppServiceClassName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAppServiceClassName.Location = new System.Drawing.Point(82, 42);
            this.tbAppServiceClassName.Name = "tbAppServiceClassName";
            this.tbAppServiceClassName.Size = new System.Drawing.Size(262, 21);
            this.tbAppServiceClassName.TabIndex = 29;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(8, 44);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(77, 12);
            this.label27.TabIndex = 28;
            this.label27.Text = "Class Name：";
            // 
            // tbPermDelete
            // 
            this.tbPermDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPermDelete.Location = new System.Drawing.Point(98, 185);
            this.tbPermDelete.Name = "tbPermDelete";
            this.tbPermDelete.Size = new System.Drawing.Size(246, 21);
            this.tbPermDelete.TabIndex = 16;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(8, 188);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(89, 12);
            this.label26.TabIndex = 23;
            this.label26.Text = "Delete权限名：";
            // 
            // tbPermEdit
            // 
            this.tbPermEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPermEdit.Location = new System.Drawing.Point(98, 162);
            this.tbPermEdit.Name = "tbPermEdit";
            this.tbPermEdit.Size = new System.Drawing.Size(246, 21);
            this.tbPermEdit.TabIndex = 17;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 164);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(77, 12);
            this.label20.TabIndex = 24;
            this.label20.Text = "Edit权限名：";
            // 
            // tbPermCreate
            // 
            this.tbPermCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPermCreate.Location = new System.Drawing.Point(98, 138);
            this.tbPermCreate.Name = "tbPermCreate";
            this.tbPermCreate.Size = new System.Drawing.Size(246, 21);
            this.tbPermCreate.TabIndex = 18;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 141);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(89, 12);
            this.label17.TabIndex = 25;
            this.label17.Text = "Create权限名：";
            // 
            // tbMenuPerm
            // 
            this.tbMenuPerm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMenuPerm.Location = new System.Drawing.Point(79, 114);
            this.tbMenuPerm.Name = "tbMenuPerm";
            this.tbMenuPerm.Size = new System.Drawing.Size(264, 21);
            this.tbMenuPerm.TabIndex = 19;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 118);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 12);
            this.label13.TabIndex = 26;
            this.label13.Text = "菜单权限名：";
            // 
            // tbOutputDtoClassName
            // 
            this.tbOutputDtoClassName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutputDtoClassName.Location = new System.Drawing.Point(109, 91);
            this.tbOutputDtoClassName.Name = "tbOutputDtoClassName";
            this.tbOutputDtoClassName.Size = new System.Drawing.Size(234, 21);
            this.tbOutputDtoClassName.TabIndex = 20;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(8, 94);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(107, 12);
            this.label31.TabIndex = 27;
            this.label31.Text = "OutputDto Class：";
            // 
            // tbInputDtoClassName
            // 
            this.tbInputDtoClassName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInputDtoClassName.Location = new System.Drawing.Point(109, 68);
            this.tbInputDtoClassName.Name = "tbInputDtoClassName";
            this.tbInputDtoClassName.Size = new System.Drawing.Size(234, 21);
            this.tbInputDtoClassName.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 12);
            this.label11.TabIndex = 27;
            this.label11.Text = "InputDto Class：";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbDeleteAsync);
            this.groupBox1.Controls.Add(this.cbUpdateAsync);
            this.groupBox1.Controls.Add(this.cbCreateAsync);
            this.groupBox1.Controls.Add(this.cbGetByIdAsync);
            this.groupBox1.Controls.Add(this.cbGetAllAsync);
            this.groupBox1.Controls.Add(this.tbDomainServiceNS);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.tbDomainServiceClassName);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(7, 40);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(359, 126);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DomainService";
            // 
            // cbDeleteAsync
            // 
            this.cbDeleteAsync.AutoSize = true;
            this.cbDeleteAsync.Checked = true;
            this.cbDeleteAsync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDeleteAsync.Location = new System.Drawing.Point(250, 99);
            this.cbDeleteAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbDeleteAsync.Name = "cbDeleteAsync";
            this.cbDeleteAsync.Size = new System.Drawing.Size(90, 16);
            this.cbDeleteAsync.TabIndex = 24;
            this.cbDeleteAsync.Text = "DeleteAsync";
            this.cbDeleteAsync.UseVisualStyleBackColor = true;
            // 
            // cbUpdateAsync
            // 
            this.cbUpdateAsync.AutoSize = true;
            this.cbUpdateAsync.Checked = true;
            this.cbUpdateAsync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUpdateAsync.Location = new System.Drawing.Point(128, 99);
            this.cbUpdateAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbUpdateAsync.Name = "cbUpdateAsync";
            this.cbUpdateAsync.Size = new System.Drawing.Size(90, 16);
            this.cbUpdateAsync.TabIndex = 22;
            this.cbUpdateAsync.Text = "UpdateAsync";
            this.cbUpdateAsync.UseVisualStyleBackColor = true;
            // 
            // cbCreateAsync
            // 
            this.cbCreateAsync.AutoSize = true;
            this.cbCreateAsync.Checked = true;
            this.cbCreateAsync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCreateAsync.Location = new System.Drawing.Point(10, 99);
            this.cbCreateAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbCreateAsync.Name = "cbCreateAsync";
            this.cbCreateAsync.Size = new System.Drawing.Size(90, 16);
            this.cbCreateAsync.TabIndex = 23;
            this.cbCreateAsync.Text = "CreateAsync";
            this.cbCreateAsync.UseVisualStyleBackColor = true;
            // 
            // cbGetByIdAsync
            // 
            this.cbGetByIdAsync.AutoSize = true;
            this.cbGetByIdAsync.Checked = true;
            this.cbGetByIdAsync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGetByIdAsync.Location = new System.Drawing.Point(128, 74);
            this.cbGetByIdAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbGetByIdAsync.Name = "cbGetByIdAsync";
            this.cbGetByIdAsync.Size = new System.Drawing.Size(114, 16);
            this.cbGetByIdAsync.TabIndex = 21;
            this.cbGetByIdAsync.Text = "Get...ByIdAsync";
            this.cbGetByIdAsync.UseVisualStyleBackColor = true;
            // 
            // cbGetAllAsync
            // 
            this.cbGetAllAsync.AutoSize = true;
            this.cbGetAllAsync.Checked = true;
            this.cbGetAllAsync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGetAllAsync.Location = new System.Drawing.Point(10, 74);
            this.cbGetAllAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbGetAllAsync.Name = "cbGetAllAsync";
            this.cbGetAllAsync.Size = new System.Drawing.Size(108, 16);
            this.cbGetAllAsync.TabIndex = 20;
            this.cbGetAllAsync.Text = "GetAll...Async";
            this.cbGetAllAsync.UseVisualStyleBackColor = true;
            // 
            // tbDomainServiceNS
            // 
            this.tbDomainServiceNS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDomainServiceNS.Location = new System.Drawing.Point(82, 20);
            this.tbDomainServiceNS.Name = "tbDomainServiceNS";
            this.tbDomainServiceNS.Size = new System.Drawing.Size(262, 21);
            this.tbDomainServiceNS.TabIndex = 18;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(8, 23);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(71, 12);
            this.label28.TabIndex = 19;
            this.label28.Text = "NameSpace：";
            // 
            // tbDomainServiceClassName
            // 
            this.tbDomainServiceClassName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDomainServiceClassName.Location = new System.Drawing.Point(82, 44);
            this.tbDomainServiceClassName.Name = "tbDomainServiceClassName";
            this.tbDomainServiceClassName.Size = new System.Drawing.Size(262, 21);
            this.tbDomainServiceClassName.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 19;
            this.label10.Text = "Class Name：";
            // 
            // tbFunName
            // 
            this.tbFunName.Location = new System.Drawing.Point(51, 10);
            this.tbFunName.Name = "tbFunName";
            this.tbFunName.Size = new System.Drawing.Size(120, 21);
            this.tbFunName.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "功能名：";
            // 
            // tpController
            // 
            this.tpController.Controls.Add(this.tbControllerName);
            this.tpController.Controls.Add(this.label32);
            this.tpController.Location = new System.Drawing.Point(4, 22);
            this.tpController.Margin = new System.Windows.Forms.Padding(2);
            this.tpController.Name = "tpController";
            this.tpController.Size = new System.Drawing.Size(377, 391);
            this.tpController.TabIndex = 4;
            this.tpController.Text = "Controller";
            this.tpController.UseVisualStyleBackColor = true;
            // 
            // tbControllerName
            // 
            this.tbControllerName.Location = new System.Drawing.Point(108, 10);
            this.tbControllerName.Name = "tbControllerName";
            this.tbControllerName.Size = new System.Drawing.Size(236, 21);
            this.tbControllerName.TabIndex = 14;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(8, 11);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(107, 12);
            this.label32.TabIndex = 15;
            this.label32.Text = "Controller 名称：";
            // 
            // btnRefreshCBB
            // 
            this.btnRefreshCBB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshCBB.Location = new System.Drawing.Point(720, 6);
            this.btnRefreshCBB.Name = "btnRefreshCBB";
            this.btnRefreshCBB.Size = new System.Drawing.Size(98, 23);
            this.btnRefreshCBB.TabIndex = 1;
            this.btnRefreshCBB.Text = "刷新ComboBox";
            this.btnRefreshCBB.UseVisualStyleBackColor = true;
            // 
            // btnCodeCreate
            // 
            this.btnCodeCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCodeCreate.Location = new System.Drawing.Point(846, 6);
            this.btnCodeCreate.Name = "btnCodeCreate";
            this.btnCodeCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCodeCreate.TabIndex = 0;
            this.btnCodeCreate.Text = "生成代码";
            this.btnCodeCreate.UseVisualStyleBackColor = true;
            this.btnCodeCreate.Click += new System.EventHandler(this.btnCodeCreate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "label4";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnRefreshCBB);
            this.panel2.Controls.Add(this.btnCodeCreate);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 473);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(934, 36);
            this.panel2.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbComment);
            this.panel1.Controls.Add(this.tbCodeRootPath);
            this.panel1.Controls.Add(this.cbTableName);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label29);
            this.panel1.Controls.Add(this.btnConn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(934, 56);
            this.panel1.TabIndex = 9;
            // 
            // tbComment
            // 
            this.tbComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbComment.Location = new System.Drawing.Point(468, 32);
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(452, 21);
            this.tbComment.TabIndex = 19;
            this.tbComment.TextChanged += new System.EventHandler(this.tbComment_TextChanged);
            // 
            // tbCodeRootPath
            // 
            this.tbCodeRootPath.Location = new System.Drawing.Point(94, 8);
            this.tbCodeRootPath.Name = "tbCodeRootPath";
            this.tbCodeRootPath.Size = new System.Drawing.Size(1016, 21);
            this.tbCodeRootPath.TabIndex = 16;
            // 
            // cbTableName
            // 
            this.cbTableName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTableName.FormattingEnabled = true;
            this.cbTableName.Location = new System.Drawing.Point(132, 34);
            this.cbTableName.Name = "cbTableName";
            this.cbTableName.Size = new System.Drawing.Size(253, 20);
            this.cbTableName.TabIndex = 15;
            this.cbTableName.SelectedIndexChanged += new System.EventHandler(this.cbTableName_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "代码的根目录：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(432, 34);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 14;
            this.label12.Text = "说明：";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(92, 37);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(41, 12);
            this.label29.TabIndex = 14;
            this.label29.Text = "表名：";
            // 
            // btnConn
            // 
            this.btnConn.Location = new System.Drawing.Point(9, 32);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(64, 20);
            this.btnConn.TabIndex = 13;
            this.btnConn.Text = "连接";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // gbFields
            // 
            this.gbFields.Controls.Add(this.gvFields);
            this.gbFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFields.Location = new System.Drawing.Point(0, 56);
            this.gbFields.Name = "gbFields";
            this.gbFields.Size = new System.Drawing.Size(546, 417);
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
            this.Property,
            this.HasInputDto,
            this.HasOutputDto,
            this.TableColWidth,
            this.IsEdit});
            this.gvFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvFields.Location = new System.Drawing.Point(3, 17);
            this.gvFields.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gvFields.Name = "gvFields";
            this.gvFields.RowTemplate.Height = 23;
            this.gvFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvFields.Size = new System.Drawing.Size(540, 397);
            this.gvFields.TabIndex = 0;
            // 
            // NAME
            // 
            this.NAME.DataPropertyName = "NAME";
            this.NAME.Frozen = true;
            this.NAME.HeaderText = "字段名";
            this.NAME.Name = "NAME";
            this.NAME.Width = 140;
            // 
            // TYPE
            // 
            this.TYPE.DataPropertyName = "TYPE";
            this.TYPE.Frozen = true;
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
            this.comments.Width = 61;
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
            this.Property.DataPropertyName = "Property";
            this.Property.HeaderText = "类中的属性名";
            this.Property.Name = "Property";
            this.Property.Width = 160;
            // 
            // HasInputDto
            // 
            this.HasInputDto.DataPropertyName = "HasInputDto";
            this.HasInputDto.HeaderText = "InputDto";
            this.HasInputDto.Name = "HasInputDto";
            // 
            // HasOutputDto
            // 
            this.HasOutputDto.DataPropertyName = "HasOutputDto";
            this.HasOutputDto.HeaderText = "OutputDto";
            this.HasOutputDto.Name = "HasOutputDto";
            // 
            // TableColWidth
            // 
            this.TableColWidth.DataPropertyName = "TableColWidth";
            this.TableColWidth.HeaderText = "列宽(%)";
            this.TableColWidth.Name = "TableColWidth";
            // 
            // IsEdit
            // 
            this.IsEdit.DataPropertyName = "IsEdit";
            this.IsEdit.HeaderText = "IsEdit";
            this.IsEdit.Name = "IsEdit";
            this.IsEdit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsEdit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 509);
            this.Controls.Add(this.gbFields);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainFrm";
            this.Text = "DM.UBP代码生成器";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.gbLP.ResumeLayout(false);
            this.gbLP.PerformLayout();
            this.gbDP.ResumeLayout(false);
            this.gbDP.PerformLayout();
            this.tpWebView.ResumeLayout(false);
            this.gbClass.ResumeLayout(false);
            this.gbClass.PerformLayout();
            this.tpEntity.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpService.ResumeLayout(false);
            this.tpService.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpController.ResumeLayout(false);
            this.tpController.PerformLayout();
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
        private System.Windows.Forms.TabPage tpWebView;
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
        private System.Windows.Forms.TabPage tpService;
        private System.Windows.Forms.TabPage tpController;
        private System.Windows.Forms.TextBox tbFunName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbDeleteAsync;
        private System.Windows.Forms.CheckBox cbUpdateAsync;
        private System.Windows.Forms.CheckBox cbCreateAsync;
        private System.Windows.Forms.CheckBox cbGetByIdAsync;
        private System.Windows.Forms.CheckBox cbGetAllAsync;
        private System.Windows.Forms.TextBox tbDomainServiceNS;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox tbDomainServiceClassName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbAppServiceNS;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox tbAppServiceClassName;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox tbPermDelete;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox tbPermEdit;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbPermCreate;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbMenuPerm;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbInputDtoClassName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbOutputDtoClassName;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox tbControllerName;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn LENGTH;
        private System.Windows.Forms.DataGridViewTextBoxColumn SCALE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nullable;
        private System.Windows.Forms.DataGridViewTextBoxColumn comments;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsIdentity;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsPk;
        private System.Windows.Forms.DataGridViewTextBoxColumn Property;
        private System.Windows.Forms.DataGridViewCheckBoxColumn HasInputDto;
        private System.Windows.Forms.DataGridViewCheckBoxColumn HasOutputDto;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableColWidth;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsEdit;
    }
}

