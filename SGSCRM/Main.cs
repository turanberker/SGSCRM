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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        public static Employee GirisYapan { get; set; }
        public static string TarihFormat = "dd.MM.yyyy";
        //public Form Varmi(string formname)
        //{
        //    Form frm = null;
        //    foreach (Form item in this.MdiChildren)
        //    {
        //        if (item.Name == formname)
        //        {
        //            frm = item;
        //            break;
        //        }
        //    }
        //    return frm;
        //}
        
        private void Main_Load(object sender, EventArgs e)
        {
            toolStripLabel3.Text = DateTime.Today.ToString(TarihFormat);
            if (!this.bwBirtdaySMSSender.IsBusy)
            {
                this.bwBirtdaySMSSender.RunWorkerAsync();
            }           
            Form f =  new Helper.Tools(). Varmi("FormAcilis",this as Main);
            if (f == null)
            {
                f = new FormAcilis();
                f.MdiParent = this;
                
                f.Show();
            }
            else
            {
                f.BringToFront();
                this.ActivateMdiChild(f);
            }
        }

        private void bwBirtdaySMSSender_DoWork(object sender, DoWorkEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("tr-TR");
                    //Bugün atılmış Olan SMS Sayılarını ve Cinslerini Getiriyor.
                    SMS_Adet_Takip takip = new SMS_Adet_Takip()
                    {
                        Date = DateTime.Today,
                    };
                    List<SMS_Adet_Takip> liste = new List<SMS_Adet_Takip>();
                    using (SMS_Adet_TakipBS bs = new SMS_Adet_TakipBS())
                    {
                        liste = bs.Listele(takip);
                    }
                    string gun = DateTime.Today.DayOfWeek.ToString();
                    //Bugün Hiç Doğumgünü Kutlama Mesajı Atılmışmı. Atılmamışsa SMS atılacak.
                    if (liste.Where(c => c.SMS_Type == SMS_TYPE.BirtDate).Count() == 0)
                    {
                        //Bugün Doğmuş Müşterileri Listele
                        //Eğer gunlerden Cumartesiyse ertesi gün doğmuş müşterilerede sms atılıyor.
                        List<Customer> SMSCustList = new List<Customer>();
                        if (DateTime.Today.DayOfWeek != DayOfWeek.Wednesday)
                        {
                            using (CustomerBS cbs = new CustomerBS())
                            {
                                Customer cust = new Customer()
                                {
                                    Birth_Date = DateTime.Today,
                                    SMS_Request = true
                                };
                                SMSCustList = cbs.AndListele(cust);
                            }
                        }
                        else
                        {
                            using (CustomerBS cbs = new CustomerBS())
                            {
                                SMSCustList = cbs.ListelebyBirtdate(DateTime.Today);
                            }
                        }
                        if (SMSCustList != null && SMSCustList.Count > 0)
                        {
                            SMSHelper sms = new SMSHelper();
                            int counter = 0;
                            SMSCustList.ForEach(x =>
                            {
                                sms.Gonder(x.Phone_Number, "Doğum Gününüz Kutlu Olsun");
                                counter++;
                            });
                            if (counter > 0)//Sms göndermişse, gönderdiği adeti database kaydediyorum.
                            {
                                using (SMS_Adet_TakipBS bs = new SMS_Adet_TakipBS())
                                {
                                    SMS_Adet_Takip item = new SMS_Adet_Takip()
                                    {
                                        Date = DateTime.Today.Date,
                                        SMS_Count = counter,
                                        SMS_Type = SMS_TYPE.BirtDate
                                    };
                                    if (bs.Insert(item) < 1)
                                    {
                                        MessageBox.Show("Gönderilen SMS Adeti Kaydedilemediğinden SMS Gönderilememiştir!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception exp)
                {
                    StackTrace st = new StackTrace();
                    StackFrame sf = new StackFrame();
                    new Helper.ExceptionLogger().ThrowExp(exp, this as Form, sf.GetMethod().Name);
                    return;
                }
                //Main form2 = new Main();
                //form2.Activate();
                //this.Close();                 
                scope.Complete();
            }

        }

        private void bağlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new Helper.Tools().Varmi("FormAcilis", this as Main);
            if (f == null)
            {
                f = new FormAcilis();
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                f.BringToFront();
                this.ActivateMdiChild(f);
            }
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GirisYapan = null;
            tanımlamalarToolStripMenuItem.Enabled = false; ;
            yönetimToolStripMenuItem.Enabled = false;
            bağlanToolStripMenuItem.Enabled = true ;
            çıkışToolStripMenuItem.Enabled = false;
            btnYeniMusteri.Enabled = false;
            tsGirisYapan.Text = "Giriş Yapın";
        }

        private void btnYeniMusteri_Click(object sender, EventArgs e)
        {
            Form f = new Helper.Tools().Varmi("Musteriler", this as Main);
            if (f == null)
            {
                f = new Musteriler();
                f.MdiParent = this;
                f.Show();
            }
            else
            {
                f.BringToFront();
                this.ActivateMdiChild(f);
            }
        }
    }
}
