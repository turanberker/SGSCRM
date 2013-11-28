namespace SGSCRM
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.girişToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bağlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.çıkışToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yönetimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estetisyenlerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meslekGruplarıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tanımlamalarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tenTipiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uygulamaBölgeleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tedavilerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnYeniMusteri = new System.Windows.Forms.ToolStripButton();
            this.tsGirisYapan = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.bwBirtdaySMSSender = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.girişToolStripMenuItem,
            this.yönetimToolStripMenuItem,
            this.tanımlamalarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(950, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // girişToolStripMenuItem
            // 
            this.girişToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bağlanToolStripMenuItem,
            this.çıkışToolStripMenuItem});
            this.girişToolStripMenuItem.Name = "girişToolStripMenuItem";
            this.girişToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.girişToolStripMenuItem.Text = "Giriş";
            // 
            // bağlanToolStripMenuItem
            // 
            this.bağlanToolStripMenuItem.Name = "bağlanToolStripMenuItem";
            this.bağlanToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.bağlanToolStripMenuItem.Text = "Bağlan";
            this.bağlanToolStripMenuItem.Click += new System.EventHandler(this.bağlanToolStripMenuItem_Click);
            // 
            // çıkışToolStripMenuItem
            // 
            this.çıkışToolStripMenuItem.Enabled = false;
            this.çıkışToolStripMenuItem.Name = "çıkışToolStripMenuItem";
            this.çıkışToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.çıkışToolStripMenuItem.Text = "Çıkış";
            this.çıkışToolStripMenuItem.Click += new System.EventHandler(this.çıkışToolStripMenuItem_Click);
            // 
            // yönetimToolStripMenuItem
            // 
            this.yönetimToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.estetisyenlerToolStripMenuItem,
            this.meslekGruplarıToolStripMenuItem});
            this.yönetimToolStripMenuItem.Enabled = false;
            this.yönetimToolStripMenuItem.Name = "yönetimToolStripMenuItem";
            this.yönetimToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.yönetimToolStripMenuItem.Text = "Yönetim";
            // 
            // estetisyenlerToolStripMenuItem
            // 
            this.estetisyenlerToolStripMenuItem.Name = "estetisyenlerToolStripMenuItem";
            this.estetisyenlerToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.estetisyenlerToolStripMenuItem.Text = "Estetisyenler";
            // 
            // meslekGruplarıToolStripMenuItem
            // 
            this.meslekGruplarıToolStripMenuItem.Name = "meslekGruplarıToolStripMenuItem";
            this.meslekGruplarıToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.meslekGruplarıToolStripMenuItem.Text = "Meslek Grupları";
            // 
            // tanımlamalarToolStripMenuItem
            // 
            this.tanımlamalarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tenTipiToolStripMenuItem,
            this.uygulamaBölgeleriToolStripMenuItem,
            this.tedavilerToolStripMenuItem});
            this.tanımlamalarToolStripMenuItem.Enabled = false;
            this.tanımlamalarToolStripMenuItem.Name = "tanımlamalarToolStripMenuItem";
            this.tanımlamalarToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.tanımlamalarToolStripMenuItem.Text = "Tanımlamalar";
            // 
            // tenTipiToolStripMenuItem
            // 
            this.tenTipiToolStripMenuItem.Name = "tenTipiToolStripMenuItem";
            this.tenTipiToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.tenTipiToolStripMenuItem.Text = "Ten Tipi";
            // 
            // uygulamaBölgeleriToolStripMenuItem
            // 
            this.uygulamaBölgeleriToolStripMenuItem.Name = "uygulamaBölgeleriToolStripMenuItem";
            this.uygulamaBölgeleriToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.uygulamaBölgeleriToolStripMenuItem.Text = "Uygulama Bölgeleri";
            // 
            // tedavilerToolStripMenuItem
            // 
            this.tedavilerToolStripMenuItem.Name = "tedavilerToolStripMenuItem";
            this.tedavilerToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.tedavilerToolStripMenuItem.Text = "Tedaviler";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnYeniMusteri,
            this.tsGirisYapan,
            this.toolStripSeparator1,
            this.toolStripLabel3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(950, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnYeniMusteri
            // 
            this.btnYeniMusteri.Enabled = false;
            this.btnYeniMusteri.Image = ((System.Drawing.Image)(resources.GetObject("btnYeniMusteri.Image")));
            this.btnYeniMusteri.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnYeniMusteri.Name = "btnYeniMusteri";
            this.btnYeniMusteri.Size = new System.Drawing.Size(79, 22);
            this.btnYeniMusteri.Text = "Yeni Kayıt";
            // 
            // tsGirisYapan
            // 
            this.tsGirisYapan.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsGirisYapan.Name = "tsGirisYapan";
            this.tsGirisYapan.Size = new System.Drawing.Size(63, 22);
            this.tsGirisYapan.Text = "Giriş Yapın";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(34, 22);
            this.toolStripLabel3.Text = "Tarih";
            // 
            // bwBirtdaySMSSender
            // 
            this.bwBirtdaySMSSender.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwBirtdaySMSSender_DoWork);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 669);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "SGS Beauty";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem estetisyenlerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem meslekGruplarıToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tenTipiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uygulamaBölgeleriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tedavilerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.ComponentModel.BackgroundWorker bwBirtdaySMSSender;
        public System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripLabel tsGirisYapan;
        private System.Windows.Forms.ToolStripMenuItem girişToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem bağlanToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem çıkışToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem yönetimToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem tanımlamalarToolStripMenuItem;
        public System.Windows.Forms.ToolStripButton btnYeniMusteri;


    }
}