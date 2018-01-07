﻿namespace SOA_A1
{
    partial class frmSelectedService
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
            this.cmdDisconnect = new System.Windows.Forms.Button();
            this.cmdExecute = new System.Windows.Forms.Button();
            this.lblServiceTeamName = new System.Windows.Forms.Label();
            this.lblServiceName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblServicePort = new System.Windows.Forms.Label();
            this.lblServiceIP = new System.Windows.Forms.Label();
            this.lblTeamName = new System.Windows.Forms.Label();
            this.lblTeamID = new System.Windows.Forms.Label();
            this.lblExpiration = new System.Windows.Forms.Label();
            this.lblRegPort = new System.Windows.Forms.Label();
            this.lblRegIP = new System.Windows.Forms.Label();
            this.txtServiceTeamName = new System.Windows.Forms.TextBox();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtServicePort = new System.Windows.Forms.TextBox();
            this.txtServiceIP = new System.Windows.Forms.TextBox();
            this.txtTeamName = new System.Windows.Forms.TextBox();
            this.txtTeamID = new System.Windows.Forms.TextBox();
            this.txtExpiration = new System.Windows.Forms.TextBox();
            this.txtRegistryIP = new System.Windows.Forms.TextBox();
            this.txtRegistryPort = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmdDisconnect
            // 
            this.cmdDisconnect.Location = new System.Drawing.Point(93, 242);
            this.cmdDisconnect.Name = "cmdDisconnect";
            this.cmdDisconnect.Size = new System.Drawing.Size(75, 23);
            this.cmdDisconnect.TabIndex = 0;
            this.cmdDisconnect.Text = "Disconnect";
            this.cmdDisconnect.UseVisualStyleBackColor = true;
            this.cmdDisconnect.Click += new System.EventHandler(this.cmdDisconnect_Click);
            // 
            // cmdExecute
            // 
            this.cmdExecute.Location = new System.Drawing.Point(12, 242);
            this.cmdExecute.Name = "cmdExecute";
            this.cmdExecute.Size = new System.Drawing.Size(75, 23);
            this.cmdExecute.TabIndex = 1;
            this.cmdExecute.Text = "Execute";
            this.cmdExecute.UseVisualStyleBackColor = true;
            // 
            // lblServiceTeamName
            // 
            this.lblServiceTeamName.AutoSize = true;
            this.lblServiceTeamName.Location = new System.Drawing.Point(331, 87);
            this.lblServiceTeamName.Name = "lblServiceTeamName";
            this.lblServiceTeamName.Size = new System.Drawing.Size(107, 13);
            this.lblServiceTeamName.TabIndex = 2;
            this.lblServiceTeamName.Text = "Service Team Name:";
            // 
            // lblServiceName
            // 
            this.lblServiceName.AutoSize = true;
            this.lblServiceName.Location = new System.Drawing.Point(361, 113);
            this.lblServiceName.Name = "lblServiceName";
            this.lblServiceName.Size = new System.Drawing.Size(77, 13);
            this.lblServiceName.TabIndex = 3;
            this.lblServiceName.Text = "Service Name:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(375, 139);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Description:";
            // 
            // lblServicePort
            // 
            this.lblServicePort.AutoSize = true;
            this.lblServicePort.Location = new System.Drawing.Point(370, 165);
            this.lblServicePort.Name = "lblServicePort";
            this.lblServicePort.Size = new System.Drawing.Size(68, 13);
            this.lblServicePort.TabIndex = 5;
            this.lblServicePort.Text = "Service Port:";
            // 
            // lblServiceIP
            // 
            this.lblServiceIP.AutoSize = true;
            this.lblServiceIP.Location = new System.Drawing.Point(379, 191);
            this.lblServiceIP.Name = "lblServiceIP";
            this.lblServiceIP.Size = new System.Drawing.Size(59, 13);
            this.lblServiceIP.TabIndex = 6;
            this.lblServiceIP.Text = "Service IP:";
            // 
            // lblTeamName
            // 
            this.lblTeamName.AutoSize = true;
            this.lblTeamName.Location = new System.Drawing.Point(370, 9);
            this.lblTeamName.Name = "lblTeamName";
            this.lblTeamName.Size = new System.Drawing.Size(68, 13);
            this.lblTeamName.TabIndex = 7;
            this.lblTeamName.Text = "Team Name:";
            // 
            // lblTeamID
            // 
            this.lblTeamID.AutoSize = true;
            this.lblTeamID.Location = new System.Drawing.Point(387, 35);
            this.lblTeamID.Name = "lblTeamID";
            this.lblTeamID.Size = new System.Drawing.Size(51, 13);
            this.lblTeamID.TabIndex = 8;
            this.lblTeamID.Text = "Team ID:";
            // 
            // lblExpiration
            // 
            this.lblExpiration.AutoSize = true;
            this.lblExpiration.Location = new System.Drawing.Point(356, 61);
            this.lblExpiration.Name = "lblExpiration";
            this.lblExpiration.Size = new System.Drawing.Size(82, 13);
            this.lblExpiration.TabIndex = 9;
            this.lblExpiration.Text = "Expiration Time:";
            // 
            // lblRegPort
            // 
            this.lblRegPort.AutoSize = true;
            this.lblRegPort.Location = new System.Drawing.Point(368, 242);
            this.lblRegPort.Name = "lblRegPort";
            this.lblRegPort.Size = new System.Drawing.Size(70, 13);
            this.lblRegPort.TabIndex = 11;
            this.lblRegPort.Text = "Registry Port:";
            // 
            // lblRegIP
            // 
            this.lblRegIP.AutoSize = true;
            this.lblRegIP.Location = new System.Drawing.Point(377, 216);
            this.lblRegIP.Name = "lblRegIP";
            this.lblRegIP.Size = new System.Drawing.Size(61, 13);
            this.lblRegIP.TabIndex = 10;
            this.lblRegIP.Text = "Registry IP:";
            // 
            // txtServiceTeamName
            // 
            this.txtServiceTeamName.Location = new System.Drawing.Point(444, 84);
            this.txtServiceTeamName.Name = "txtServiceTeamName";
            this.txtServiceTeamName.ReadOnly = true;
            this.txtServiceTeamName.Size = new System.Drawing.Size(133, 20);
            this.txtServiceTeamName.TabIndex = 12;
            // 
            // txtServiceName
            // 
            this.txtServiceName.Location = new System.Drawing.Point(444, 110);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.ReadOnly = true;
            this.txtServiceName.Size = new System.Drawing.Size(133, 20);
            this.txtServiceName.TabIndex = 13;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(444, 136);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(133, 20);
            this.txtDescription.TabIndex = 14;
            // 
            // txtServicePort
            // 
            this.txtServicePort.Location = new System.Drawing.Point(444, 162);
            this.txtServicePort.Name = "txtServicePort";
            this.txtServicePort.ReadOnly = true;
            this.txtServicePort.Size = new System.Drawing.Size(133, 20);
            this.txtServicePort.TabIndex = 15;
            // 
            // txtServiceIP
            // 
            this.txtServiceIP.Location = new System.Drawing.Point(444, 188);
            this.txtServiceIP.Name = "txtServiceIP";
            this.txtServiceIP.ReadOnly = true;
            this.txtServiceIP.Size = new System.Drawing.Size(133, 20);
            this.txtServiceIP.TabIndex = 16;
            // 
            // txtTeamName
            // 
            this.txtTeamName.Location = new System.Drawing.Point(444, 6);
            this.txtTeamName.Name = "txtTeamName";
            this.txtTeamName.ReadOnly = true;
            this.txtTeamName.Size = new System.Drawing.Size(133, 20);
            this.txtTeamName.TabIndex = 17;
            // 
            // txtTeamID
            // 
            this.txtTeamID.Location = new System.Drawing.Point(444, 32);
            this.txtTeamID.Name = "txtTeamID";
            this.txtTeamID.ReadOnly = true;
            this.txtTeamID.Size = new System.Drawing.Size(133, 20);
            this.txtTeamID.TabIndex = 18;
            // 
            // txtExpiration
            // 
            this.txtExpiration.Location = new System.Drawing.Point(444, 58);
            this.txtExpiration.Name = "txtExpiration";
            this.txtExpiration.ReadOnly = true;
            this.txtExpiration.Size = new System.Drawing.Size(133, 20);
            this.txtExpiration.TabIndex = 19;
            // 
            // txtRegistryIP
            // 
            this.txtRegistryIP.Location = new System.Drawing.Point(444, 213);
            this.txtRegistryIP.Name = "txtRegistryIP";
            this.txtRegistryIP.ReadOnly = true;
            this.txtRegistryIP.Size = new System.Drawing.Size(133, 20);
            this.txtRegistryIP.TabIndex = 20;
            // 
            // txtRegistryPort
            // 
            this.txtRegistryPort.Location = new System.Drawing.Point(444, 239);
            this.txtRegistryPort.Name = "txtRegistryPort";
            this.txtRegistryPort.ReadOnly = true;
            this.txtRegistryPort.Size = new System.Drawing.Size(133, 20);
            this.txtRegistryPort.TabIndex = 21;
            // 
            // frmSelectedService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 271);
            this.Controls.Add(this.txtRegistryPort);
            this.Controls.Add(this.txtRegistryIP);
            this.Controls.Add(this.txtExpiration);
            this.Controls.Add(this.txtTeamID);
            this.Controls.Add(this.txtTeamName);
            this.Controls.Add(this.txtServiceIP);
            this.Controls.Add(this.txtServicePort);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtServiceName);
            this.Controls.Add(this.txtServiceTeamName);
            this.Controls.Add(this.lblRegPort);
            this.Controls.Add(this.lblRegIP);
            this.Controls.Add(this.lblExpiration);
            this.Controls.Add(this.lblTeamID);
            this.Controls.Add(this.lblTeamName);
            this.Controls.Add(this.lblServiceIP);
            this.Controls.Add(this.lblServicePort);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblServiceName);
            this.Controls.Add(this.lblServiceTeamName);
            this.Controls.Add(this.cmdExecute);
            this.Controls.Add(this.cmdDisconnect);
            this.Name = "frmSelectedService";
            this.Text = "Service Finder - Selected Service";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdDisconnect;
        private System.Windows.Forms.Button cmdExecute;
        private System.Windows.Forms.Label lblServiceTeamName;
        private System.Windows.Forms.Label lblServiceName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblServicePort;
        private System.Windows.Forms.Label lblServiceIP;
        private System.Windows.Forms.Label lblTeamName;
        private System.Windows.Forms.Label lblTeamID;
        private System.Windows.Forms.Label lblExpiration;
        private System.Windows.Forms.Label lblRegPort;
        private System.Windows.Forms.Label lblRegIP;
        public System.Windows.Forms.TextBox txtServiceTeamName;
        public System.Windows.Forms.TextBox txtServiceName;
        public System.Windows.Forms.TextBox txtDescription;
        public System.Windows.Forms.TextBox txtServicePort;
        public System.Windows.Forms.TextBox txtServiceIP;
        public System.Windows.Forms.TextBox txtTeamName;
        public System.Windows.Forms.TextBox txtTeamID;
        public System.Windows.Forms.TextBox txtExpiration;
        public System.Windows.Forms.TextBox txtRegistryIP;
        public System.Windows.Forms.TextBox txtRegistryPort;
    }
}