﻿namespace SGSCRM
{
    partial class FormAcilis
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
            this.lblYapilanIslem = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblYapilanIslem
            // 
            this.lblYapilanIslem.AutoSize = true;
            this.lblYapilanIslem.Location = new System.Drawing.Point(112, 164);
            this.lblYapilanIslem.Name = "lblYapilanIslem";
            this.lblYapilanIslem.Size = new System.Drawing.Size(35, 13);
            this.lblYapilanIslem.TabIndex = 1;
            this.lblYapilanIslem.Text = "label1";
            // 
            // FormAcilis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lblYapilanIslem);
            this.Name = "FormAcilis";
            this.Text = "SGS Beauty";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblYapilanIslem;
    }
}

