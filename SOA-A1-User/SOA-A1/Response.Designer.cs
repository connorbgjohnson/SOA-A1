namespace SOA_A1
{
    partial class Response
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblRespPosition = new System.Windows.Forms.Label();
            this.lblRespName = new System.Windows.Forms.Label();
            this.lblDataType = new System.Windows.Forms.Label();
            this.txtRespValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblRespPosition
            // 
            this.lblRespPosition.AutoSize = true;
            this.lblRespPosition.Location = new System.Drawing.Point(4, 4);
            this.lblRespPosition.Name = "lblRespPosition";
            this.lblRespPosition.Size = new System.Drawing.Size(72, 13);
            this.lblRespPosition.TabIndex = 0;
            this.lblRespPosition.Text = "Resp Position";
            // 
            // lblRespName
            // 
            this.lblRespName.AutoSize = true;
            this.lblRespName.Location = new System.Drawing.Point(82, 6);
            this.lblRespName.Name = "lblRespName";
            this.lblRespName.Size = new System.Drawing.Size(63, 13);
            this.lblRespName.TabIndex = 1;
            this.lblRespName.Text = "Resp Name";
            // 
            // lblDataType
            // 
            this.lblDataType.AutoSize = true;
            this.lblDataType.Location = new System.Drawing.Point(225, 6);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(85, 13);
            this.lblDataType.TabIndex = 2;
            this.lblDataType.Text = "Resp Data Type";
            // 
            // txtRespValue
            // 
            this.txtRespValue.Location = new System.Drawing.Point(316, 3);
            this.txtRespValue.Name = "txtRespValue";
            this.txtRespValue.Size = new System.Drawing.Size(100, 20);
            this.txtRespValue.TabIndex = 3;
            // 
            // Response
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtRespValue);
            this.Controls.Add(this.lblDataType);
            this.Controls.Add(this.lblRespName);
            this.Controls.Add(this.lblRespPosition);
            this.Name = "Response";
            this.Size = new System.Drawing.Size(419, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblRespPosition;
        public System.Windows.Forms.Label lblRespName;
        public System.Windows.Forms.Label lblDataType;
        public System.Windows.Forms.TextBox txtRespValue;
    }
}
