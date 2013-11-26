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
                        lblYapilanIslem.Text = "Bugün Doğmuş Müşteriler Listeleniyor.";
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
                            lblYapilanIslem.Text = "Doğum Günü Kutlama Mesajları Gönderiliyor";
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
                            lblYapilanIslem.Text = counter.ToString() + " kişiye SMS Gönderilmiştir.";
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
    }
}
