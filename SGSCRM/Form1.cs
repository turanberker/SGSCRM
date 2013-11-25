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

namespace SGSCRM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Customer cus = new Customer()
                    {
                        //  Customer_ID=1,
                        // FNAME = "Berker",
                        //  LNAME = "Turan",
                        //   E_Mail = @"turanberker@yahoo.com",
                        //   Occupation_Type_ID = 1,
                        //   Phone_Number = "5",
                        //   Skin_Type_ID = 1,
                        TC_Number = "20446180678",
                        // Birth_Date = DateTime.Today.AddYears(-20)

                    };
                    VeriErisimKatmani.Vek bs = new VeriErisimKatmani.Vek(@"server=.\GTIDB  ;database=SGSCRM;MultipleActiveResultSets=true;Trusted_connection=yes;");
                    // List<Customer> liste1 = bs.Select<Customer>();
                    //List<Customer> liste = bs.Select<Customer>(cus);
                   // DataTable dt = bs.GetDataTable<Customer>(cus);
                   // bs.ExecuteReader<Customer>(CommandType.Text, "Select * from Customer where Customer_ID=@ID", cus.Customer_ID);
                   // bs.Delete<Customer>(cus);
                    bs.Insert<Customer>(cus);
                    //   bool sonuc = bs.Update<Customer>(cus);
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
    }
}
