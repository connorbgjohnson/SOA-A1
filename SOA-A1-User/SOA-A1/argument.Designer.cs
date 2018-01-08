namespace SOA_A1
{
    partial class Argument
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
            this.lblArgPosition = new System.Windows.Forms.Label();
            this.lblArgName = new System.Windows.Forms.Label();
            this.lblArgDataType = new System.Windows.Forms.Label();
            this.lblArgMandatory = new System.Windows.Forms.Label();
            this.txtArgValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblArgPosition
            // 
            this.lblArgPosition.AutoSize = true;
            this.lblArgPosition.Location = new System.Drawing.Point(4, 4);
            this.lblArgPosition.Name = "lblArgPosition";
            this.lblArgPosition.Size = new System.Drawing.Size(63, 13);
            this.lblArgPosition.TabIndex = 0;
            this.lblArgPosition.Text = "Arg Position";
            // 
            // lblArgName
            // 
            this.lblArgName.AutoSize = true;
            this.lblArgName.Location = new System.Drawing.Point(73, 4);
            this.lblArgName.Name = "lblArgName";
            this.lblArgName.Size = new System.Drawing.Size(54, 13);
            this.lblArgName.TabIndex = 1;
            this.lblArgName.Text = "Arg Name";
            // 
            // lblArgDataType
            // 
            this.lblArgDataType.AutoSize = true;
            this.lblArgDataType.Location = new System.Drawing.Point(133, 4);
            this.lblArgDataType.Name = "lblArgDataType";
            this.lblArgDataType.Size = new System.Drawing.Size(57, 13);
            this.lblArgDataType.TabIndex = 2;
            this.lblArgDataType.Text = "Data Type";
            // 
            // lblArgMandatory
            // 
            this.lblArgMandatory.AutoSize = true;
            this.lblArgMandatory.Location = new System.Drawing.Point(193, 4);
            this.lblArgMandatory.Name = "lblArgMandatory";
            this.lblArgMandatory.Size = new System.Drawing.Size(98, 13);
            this.lblArgMandatory.TabIndex = 3;
            this.lblArgMandatory.Text = "Mandatory|Optional";
            // 
            // txtArgValue
            // 
            this.txtArgValue.Location = new System.Drawing.Point(297, 4);
            this.txtArgValue.Name = "txtArgValue";
            this.txtArgValue.Size = new System.Drawing.Size(119, 20);
            this.txtArgValue.TabIndex = 4;
            // 
            // Argument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtArgValue);
            this.Controls.Add(this.lblArgMandatory);
            this.Controls.Add(this.lblArgDataType);
            this.Controls.Add(this.lblArgName);
            this.Controls.Add(this.lblArgPosition);
            this.Name = "Argument";
            this.Size = new System.Drawing.Size(438, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblArgPosition;
        public System.Windows.Forms.Label lblArgName;
        public System.Windows.Forms.Label lblArgDataType;
        public System.Windows.Forms.Label lblArgMandatory;
        public System.Windows.Forms.TextBox txtArgValue;
    }
}
