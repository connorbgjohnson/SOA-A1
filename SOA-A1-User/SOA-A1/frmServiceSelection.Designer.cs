namespace SOA_A1
{
    partial class frmServiceSelection
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
            this.cboServiceSelect = new System.Windows.Forms.ComboBox();
            this.cmdSelect = new System.Windows.Forms.Button();
            this.cmdDisconnectTeam = new System.Windows.Forms.Button();
            this.lblTeamName = new System.Windows.Forms.Label();
            this.lblTeamID = new System.Windows.Forms.Label();
            this.txtTeamName = new System.Windows.Forms.TextBox();
            this.txtTeamID = new System.Windows.Forms.TextBox();
            this.txtExpiration = new System.Windows.Forms.TextBox();
            this.lblExpiration = new System.Windows.Forms.Label();
            this.txtRegPort = new System.Windows.Forms.TextBox();
            this.txtRegIP = new System.Windows.Forms.TextBox();
            this.lblRegistryPort = new System.Windows.Forms.Label();
            this.lblRegistryIP = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cboServiceSelect
            // 
            this.cboServiceSelect.FormattingEnabled = true;
            this.cboServiceSelect.Items.AddRange(new object[] {
            "Purchase-Totalizer",
            "Pay-Stub Amount Generator",
            "Car-Loan Calculator",
            "Canadian Postal-Code Validator"});
            this.cboServiceSelect.Location = new System.Drawing.Point(18, 14);
            this.cboServiceSelect.Name = "cboServiceSelect";
            this.cboServiceSelect.Size = new System.Drawing.Size(121, 21);
            this.cboServiceSelect.TabIndex = 0;
            // 
            // cmdSelect
            // 
            this.cmdSelect.Location = new System.Drawing.Point(151, 12);
            this.cmdSelect.Name = "cmdSelect";
            this.cmdSelect.Size = new System.Drawing.Size(75, 23);
            this.cmdSelect.TabIndex = 1;
            this.cmdSelect.Text = "Select";
            this.cmdSelect.UseVisualStyleBackColor = true;
            this.cmdSelect.Click += new System.EventHandler(this.cmdSelect_Click);
            // 
            // cmdDisconnectTeam
            // 
            this.cmdDisconnectTeam.Location = new System.Drawing.Point(232, 12);
            this.cmdDisconnectTeam.Name = "cmdDisconnectTeam";
            this.cmdDisconnectTeam.Size = new System.Drawing.Size(99, 23);
            this.cmdDisconnectTeam.TabIndex = 2;
            this.cmdDisconnectTeam.Text = "Disconnect Team";
            this.cmdDisconnectTeam.UseVisualStyleBackColor = true;
            this.cmdDisconnectTeam.Click += new System.EventHandler(this.cmdDisconnectTeam_Click);
            // 
            // lblTeamName
            // 
            this.lblTeamName.AutoSize = true;
            this.lblTeamName.Location = new System.Drawing.Point(2, 102);
            this.lblTeamName.Name = "lblTeamName";
            this.lblTeamName.Size = new System.Drawing.Size(71, 13);
            this.lblTeamName.TabIndex = 3;
            this.lblTeamName.Text = "Team Name: ";
            // 
            // lblTeamID
            // 
            this.lblTeamID.AutoSize = true;
            this.lblTeamID.Location = new System.Drawing.Point(222, 102);
            this.lblTeamID.Name = "lblTeamID";
            this.lblTeamID.Size = new System.Drawing.Size(54, 13);
            this.lblTeamID.TabIndex = 4;
            this.lblTeamID.Text = "Team ID: ";
            // 
            // txtTeamName
            // 
            this.txtTeamName.Location = new System.Drawing.Point(79, 99);
            this.txtTeamName.Name = "txtTeamName";
            this.txtTeamName.ReadOnly = true;
            this.txtTeamName.Size = new System.Drawing.Size(137, 20);
            this.txtTeamName.TabIndex = 5;
            // 
            // txtTeamID
            // 
            this.txtTeamID.Location = new System.Drawing.Point(282, 99);
            this.txtTeamID.Name = "txtTeamID";
            this.txtTeamID.ReadOnly = true;
            this.txtTeamID.Size = new System.Drawing.Size(100, 20);
            this.txtTeamID.TabIndex = 6;
            // 
            // txtExpiration
            // 
            this.txtExpiration.Location = new System.Drawing.Point(476, 99);
            this.txtExpiration.Name = "txtExpiration";
            this.txtExpiration.ReadOnly = true;
            this.txtExpiration.Size = new System.Drawing.Size(100, 20);
            this.txtExpiration.TabIndex = 8;
            // 
            // lblExpiration
            // 
            this.lblExpiration.AutoSize = true;
            this.lblExpiration.Location = new System.Drawing.Point(388, 102);
            this.lblExpiration.Name = "lblExpiration";
            this.lblExpiration.Size = new System.Drawing.Size(82, 13);
            this.lblExpiration.TabIndex = 7;
            this.lblExpiration.Text = "Expiration Time:";
            // 
            // txtRegPort
            // 
            this.txtRegPort.Location = new System.Drawing.Point(419, 39);
            this.txtRegPort.Name = "txtRegPort";
            this.txtRegPort.ReadOnly = true;
            this.txtRegPort.Size = new System.Drawing.Size(100, 20);
            this.txtRegPort.TabIndex = 12;
            // 
            // txtRegIP
            // 
            this.txtRegIP.Location = new System.Drawing.Point(419, 13);
            this.txtRegIP.Name = "txtRegIP";
            this.txtRegIP.ReadOnly = true;
            this.txtRegIP.Size = new System.Drawing.Size(137, 20);
            this.txtRegIP.TabIndex = 11;
            // 
            // lblRegistryPort
            // 
            this.lblRegistryPort.AutoSize = true;
            this.lblRegistryPort.Location = new System.Drawing.Point(340, 42);
            this.lblRegistryPort.Name = "lblRegistryPort";
            this.lblRegistryPort.Size = new System.Drawing.Size(73, 13);
            this.lblRegistryPort.TabIndex = 10;
            this.lblRegistryPort.Text = "Registry Port: ";
            // 
            // lblRegistryIP
            // 
            this.lblRegistryIP.AutoSize = true;
            this.lblRegistryIP.Location = new System.Drawing.Point(342, 16);
            this.lblRegistryIP.Name = "lblRegistryIP";
            this.lblRegistryIP.Size = new System.Drawing.Size(61, 13);
            this.lblRegistryIP.TabIndex = 9;
            this.lblRegistryIP.Text = "Registry IP:";
            // 
            // frmServiceSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 129);
            this.Controls.Add(this.txtRegPort);
            this.Controls.Add(this.txtRegIP);
            this.Controls.Add(this.lblRegistryPort);
            this.Controls.Add(this.lblRegistryIP);
            this.Controls.Add(this.txtExpiration);
            this.Controls.Add(this.lblExpiration);
            this.Controls.Add(this.txtTeamID);
            this.Controls.Add(this.txtTeamName);
            this.Controls.Add(this.lblTeamID);
            this.Controls.Add(this.lblTeamName);
            this.Controls.Add(this.cmdDisconnectTeam);
            this.Controls.Add(this.cmdSelect);
            this.Controls.Add(this.cboServiceSelect);
            this.Name = "frmServiceSelection";
            this.Text = "Service Finder - Service Selection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboServiceSelect;
        private System.Windows.Forms.Button cmdSelect;
        private System.Windows.Forms.Button cmdDisconnectTeam;
        private System.Windows.Forms.Label lblTeamName;
        private System.Windows.Forms.Label lblTeamID;
        public System.Windows.Forms.TextBox txtTeamName;
        public System.Windows.Forms.TextBox txtTeamID;
        public System.Windows.Forms.TextBox txtExpiration;
        private System.Windows.Forms.Label lblExpiration;
        public System.Windows.Forms.TextBox txtRegPort;
        public System.Windows.Forms.TextBox txtRegIP;
        private System.Windows.Forms.Label lblRegistryPort;
        private System.Windows.Forms.Label lblRegistryIP;
    }
}