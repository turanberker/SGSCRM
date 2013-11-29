using Businness;
using DataType;
using SGSCRM.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;

namespace SGSCRM
{
    public partial class MusteriGuncelleEkle : Form
    {
        Musteriler Musterilerformu;
        public MusteriGuncelleEkle(Musteriler MusterilerFormu)
        {
            InitializeComponent();
            Musterilerformu = MusterilerFormu;
            CMBDoldur();
        }
        public MusteriGuncelleEkle(Musteriler MusterilerFormu, long MusteriID)
        {
            InitializeComponent();
            Musterilerformu = MusterilerFormu;
            using (CustomerBS bs = new CustomerBS())
            {
                Customer item = new Customer()
                {
                    Customer_ID = MusteriID
                };
                item = bs.AndListele(item)[0];
                txtAdi.Text = item.FNAME;
                txtSoyadi.Text = item.LNAME;
                txtTCNo.Text = item.TC_Number;
                txtEPosta.Text = item.E_Mail;
                mtxtCepTel.Text = item.Phone_Number;
                mtxtDogumTarihi.Text = item.Birth_Date.Value.ToString(Main.TarihFormat);
                CMBDoldur();
                cmbCiltTipi.SelectedValue = item.Skin_Type_ID;
                cmbMeslegi.SelectedItem = item.Occupation_Type_ID;
                button1.Tag = item.Customer_ID;
                button1.Text = "Güncelle";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    if (string.IsNullOrEmpty(txtAdi.Text) || string.IsNullOrEmpty(txtTCNo.Text) || string.IsNullOrEmpty(txtSoyadi.Text) || mtxtCepTel.Text.Length != 13 || string.IsNullOrEmpty(txtEPosta.Text))
                    {
                        MessageBox.Show("İlgili Alanları Doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    DateTime tarih;
                    if (!DateTime.TryParse(mtxtDogumTarihi.Text, out tarih))
                    {
                        MessageBox.Show("Tarih Alanına Yanlış Veri Girdiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int? ciltipiID = null;
                    if (cmbCiltTipi.SelectedValue == null && string.IsNullOrEmpty(cmbCiltTipi.SelectedText) && !string.IsNullOrEmpty(cmbCiltTipi.Text))
                    {
                        using (Skin_TypeBS bs = new Skin_TypeBS())
                        {
                            Skin_Type item = new Skin_Type()
                            {
                                Skin_Type_Name = cmbCiltTipi.Text.TrimEnd()
                            };
                            ciltipiID = Convert.ToInt32(bs.Insert(item));
                        }
                    }
                    int? MeslekId = null;
                    if (cmbMeslegi.SelectedValue == null && string.IsNullOrEmpty(cmbMeslegi.SelectedText) && !string.IsNullOrEmpty(cmbMeslegi.Text))
                    {
                        using (Occupation_TypeBS bs = new Occupation_TypeBS())
                        {
                            Occupation_Type item = new Occupation_Type()
                            {
                                Occupation_Type_Name = cmbMeslegi.Text.TrimEnd()
                            };
                            MeslekId = (bs.Insert(item));
                        }
                    }
                    if (!ciltipiID.HasValue)
                        ciltipiID = Convert.ToInt32(cmbCiltTipi.SelectedValue);
                    if (!MeslekId.HasValue)
                        MeslekId = Convert.ToInt32(cmbMeslegi.SelectedValue);
                    using (CustomerBS bs = new CustomerBS())
                    {
                        Customer item = new Customer()
                        {
                            FNAME = txtAdi.Text,
                            LNAME = txtSoyadi.Text,
                            Occupation_Type_ID = MeslekId,
                            Birth_Date = tarih,
                            Skin_Type_ID = ciltipiID,
                            E_Mail = txtEPosta.Text,
                            Phone_Number = mtxtCepTel.Text,
                            SMS_Request = cmbSMSReq.Checked,
                            TC_Number = txtTCNo.Text
                        };
                        if (button1.Text == "Ekle")
                        {
                            if (bs.Insert(item) > 0)
                            {
                                MessageBox.Show("Kayıt İşlemi Başarıyla Tamamlanmıştır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Kayıt Sırasında Hata Oluşmuştur!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        if (button1.Text == "Güncelle")
                        {
                            item.Customer_ID = Convert.ToInt64(button1.Tag);
                            if (bs.Update(item))
                            {
                                MessageBox.Show("Kayıt İşlemi Başarıyla Tamamlanmıştır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Kayıt Sırasında Hata Oluşmuştur!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    Musterilerformu.DataGridDoldur();
                    this.Close();
                    scope.Complete();
                }
                catch (Exception exp)
                {
                    StackTrace st = new StackTrace();
                    StackFrame sf = new StackFrame();
                    new Helper.ExceptionLogger().ThrowExp(exp, this as Form, sf.GetMethod().Name);
                    return;
                }
            }
        }

        private void MusteriGuncelleEkle_Load(object sender, EventArgs e)
        {

        }

        private void CMBDoldur()
        {
            cmbCiltTipi.ValueMember = "Skin_Type_ID";
            cmbCiltTipi.DisplayMember = "Skin_Type_Name";
            cmbCiltTipi.DataSource = new cmbDoldurucular().CiltTipiDoldur();

            cmbMeslegi.ValueMember = "Occupation_Type_ID";
            cmbMeslegi.DisplayMember = "Occupation_Type_Name";
            cmbMeslegi.DataSource = new cmbDoldurucular().MeslekDoldur();
        }
    }
}
