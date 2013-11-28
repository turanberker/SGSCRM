using DataType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Transactions;
using System.Diagnostics;
using Businness;
using SGSCRM.Helper;

namespace SGSCRM
{
    public partial class FormAcilis : Form
    {
        public FormAcilis()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKullaniciAdi.Text) || string.IsNullOrEmpty(txtSifre.Text))
            {
                MessageBox.Show("Kullanıcı Adı ve Şifre Alanları Boş Geçilemez", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                using (EmployeeBS bs = new EmployeeBS())
                {
                    Employee item = new Employee()
                    {
                        User_Name = txtKullaniciAdi.Text,
                        Password = txtSifre.Text
                    };
                    List<Employee> emp = bs.Select(item);                   
                    if (emp.Count() == 0)
                    {
                        MessageBox.Show("Kullanıcı Adı ve(ya) Şifre Hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Main.GirisYapan = emp[0];
                        Main m = this.MdiParent as Main;
                        m.tsGirisYapan.Text = Main.GirisYapan.FNAME + " " + Main.GirisYapan.LNAME;
                        m.tanımlamalarToolStripMenuItem.Enabled = true;
                        m.yönetimToolStripMenuItem.Enabled = true;
                        m.bağlanToolStripMenuItem.Enabled = false;
                        m.çıkışToolStripMenuItem.Enabled = true;
                        m.btnYeniMusteri.Enabled = true;
                        this.Close();
                        Musteriler musteriform = new Musteriler(m) { MdiParent = m };
                        musteriform.Show();
                    }
                }
            }
        }
    }
}
