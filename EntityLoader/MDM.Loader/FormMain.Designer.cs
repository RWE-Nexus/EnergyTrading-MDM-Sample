namespace MDM.Loader
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlHome = new System.Windows.Forms.TabControl();
            this.tabPageHome = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkCandidateData = new System.Windows.Forms.CheckBox();
            this.EntityComboBox = new System.Windows.Forms.ComboBox();
            this.EntityBrowseButton = new System.Windows.Forms.Button();
            this.EntityFileTextBox = new System.Windows.Forms.TextBox();
            this.EntityImportButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.textBoxWorkers = new System.Windows.Forms.TextBox();
            this.textBoxBaseUri = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.StatusMessage = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControlHome.SuspendLayout();
            this.tabPageHome.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlHome
            // 
            this.tabControlHome.Controls.Add(this.tabPageHome);
            this.tabControlHome.Controls.Add(this.tabPage1);
            this.tabControlHome.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControlHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlHome.Location = new System.Drawing.Point(0, 0);
            this.tabControlHome.Name = "tabControlHome";
            this.tabControlHome.SelectedIndex = 0;
            this.tabControlHome.Size = new System.Drawing.Size(625, 490);
            this.tabControlHome.TabIndex = 0;
            // 
            // tabPageHome
            // 
            this.tabPageHome.Controls.Add(this.groupBox4);
            this.tabPageHome.Controls.Add(this.groupBox1);
            this.tabPageHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageHome.Location = new System.Drawing.Point(4, 22);
            this.tabPageHome.Name = "tabPageHome";
            this.tabPageHome.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHome.Size = new System.Drawing.Size(617, 464);
            this.tabPageHome.TabIndex = 0;
            this.tabPageHome.Text = "Loader";
            this.tabPageHome.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkCandidateData);
            this.groupBox4.Controls.Add(this.EntityComboBox);
            this.groupBox4.Controls.Add(this.EntityBrowseButton);
            this.groupBox4.Controls.Add(this.EntityFileTextBox);
            this.groupBox4.Controls.Add(this.EntityImportButton);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(3, 75);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(611, 82);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "MDM Entities";
            // 
            // chkCandidateData
            // 
            this.chkCandidateData.AutoSize = true;
            this.chkCandidateData.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCandidateData.Location = new System.Drawing.Point(486, 24);
            this.chkCandidateData.Name = "chkCandidateData";
            this.chkCandidateData.Size = new System.Drawing.Size(98, 17);
            this.chkCandidateData.TabIndex = 19;
            this.chkCandidateData.Text = "Candidate data";
            this.chkCandidateData.UseVisualStyleBackColor = true;
            // 
            // EntityComboBox
            // 
            this.EntityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EntityComboBox.FormattingEnabled = true;
            this.EntityComboBox.Items.AddRange(new object[] {
            "Agreement",
            "Book",
            "BookDefault",
            "Broker",
            "BrokerCommodity",
            "BrokerRate",
            "BusinessUnit",
            "Calendar",
            "Commodity",
            "CommodityInstrumentType",
            "Counterparty",
            "Curve",
            "Exchange",
            "InstrumentType",
            "InstrumentTypeOverride",
            "LegalEntity",
            "Location",
            "Market",
            "Party",
            "PartyAccountability",
            "PartyCommodity",
            "PartyOverride",
            "PartyRole",
            "Person",
            "Portfolio",
            "PortfolioHierarchy",
            "Product",
            "ProductCurve",
            "ProductScota",
            "ProductTenorType",
            "ProductType",
            "ReferenceData",
            "SettlementContact",
            "ShipperCode",
            "SourceSystem",
            "Tenor",
            "TenorType",
            "Vessel",
            "Unit"});
            this.EntityComboBox.Location = new System.Drawing.Point(3, 21);
            this.EntityComboBox.Name = "EntityComboBox";
            this.EntityComboBox.Size = new System.Drawing.Size(165, 21);
            this.EntityComboBox.TabIndex = 15;
            // 
            // EntityBrowseButton
            // 
            this.EntityBrowseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EntityBrowseButton.Location = new System.Drawing.Point(394, 21);
            this.EntityBrowseButton.Name = "EntityBrowseButton";
            this.EntityBrowseButton.Size = new System.Drawing.Size(33, 23);
            this.EntityBrowseButton.TabIndex = 17;
            this.EntityBrowseButton.Text = "...";
            this.EntityBrowseButton.UseVisualStyleBackColor = true;
            this.EntityBrowseButton.Click += new System.EventHandler(this.EntityBrowseButton_Click);
            // 
            // EntityFileTextBox
            // 
            this.EntityFileTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.EntityFileTextBox.Location = new System.Drawing.Point(185, 24);
            this.EntityFileTextBox.Name = "EntityFileTextBox";
            this.EntityFileTextBox.Size = new System.Drawing.Size(195, 20);
            this.EntityFileTextBox.TabIndex = 16;
            this.EntityFileTextBox.Text = "..\\..\\..\\..\\data\\";
            // 
            // EntityImportButton
            // 
            this.EntityImportButton.Location = new System.Drawing.Point(515, 47);
            this.EntityImportButton.Name = "EntityImportButton";
            this.EntityImportButton.Size = new System.Drawing.Size(75, 23);
            this.EntityImportButton.TabIndex = 18;
            this.EntityImportButton.Text = "Import";
            this.EntityImportButton.UseVisualStyleBackColor = true;
            this.EntityImportButton.Click += new System.EventHandler(this.EntityImportButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.textBoxWorkers);
            this.groupBox1.Controls.Add(this.textBoxBaseUri);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(611, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nexus Service Configuration";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(515, 36);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(440, 20);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 13);
            this.label17.TabIndex = 21;
            this.label17.Text = "Workers";
            // 
            // textBoxWorkers
            // 
            this.textBoxWorkers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxWorkers.Location = new System.Drawing.Point(443, 37);
            this.textBoxWorkers.Name = "textBoxWorkers";
            this.textBoxWorkers.Size = new System.Drawing.Size(29, 20);
            this.textBoxWorkers.TabIndex = 2;
            this.textBoxWorkers.Text = "1";
            // 
            // textBoxBaseUri
            // 
            this.textBoxBaseUri.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBaseUri.Location = new System.Drawing.Point(7, 37);
            this.textBoxBaseUri.Name = "textBoxBaseUri";
            this.textBoxBaseUri.Size = new System.Drawing.Size(415, 20);
            this.textBoxBaseUri.TabIndex = 1;
            this.textBoxBaseUri.Text = "http://localhost.:8080/";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nexus URL";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.LogTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(617, 464);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // LogTextBox
            // 
            this.LogTextBox.Location = new System.Drawing.Point(0, 0);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogTextBox.Size = new System.Drawing.Size(614, 287);
            this.LogTextBox.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.StatusMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 491);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(625, 23);
            this.panel1.TabIndex = 1;
            // 
            // StatusMessage
            // 
            this.StatusMessage.AutoSize = true;
            this.StatusMessage.Location = new System.Drawing.Point(4, 5);
            this.StatusMessage.Name = "StatusMessage";
            this.StatusMessage.Size = new System.Drawing.Size(80, 13);
            this.StatusMessage.TabIndex = 1;
            this.StatusMessage.Text = "StatusMessage";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "XML files|*.xml|CSV files|*.csv";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 514);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControlHome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MDM Loader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.tabControlHome.ResumeLayout(false);
            this.tabPageHome.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlHome;
        private System.Windows.Forms.TabPage tabPageHome;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxBaseUri;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label StatusMessage;
        private System.Windows.Forms.TextBox LogTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBoxWorkers;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox EntityComboBox;
        private System.Windows.Forms.Button EntityBrowseButton;
        private System.Windows.Forms.Button EntityImportButton;
        private System.Windows.Forms.TextBox EntityFileTextBox;
        private System.Windows.Forms.CheckBox chkCandidateData;
    }
}

