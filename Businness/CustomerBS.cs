using DataType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Businness
{
    public class CustomerBS : DisposeEt
    {
        VeriErisimKatmani.Vek vek;
        public CustomerBS()
        {
            vek = new VeriErisimKatmani.Vek(DAL.Default.localcon.ToString());
        }
        public bool Delete(Customer Entity)
        {
            return vek.Delete<Customer>(Entity);
        }
        public bool Update(Customer Entity)
        {
            return vek.Update<Customer>(Entity);
        }
        public long Insert(Customer Entity)
        {
            return vek.Insert<Customer>(Entity);
        }
        public List<Customer> AndListele(Customer Entity)
        {
            return vek.AndSelect<Customer>(Entity);
        }
        public List<Customer> Listele()
        {
            return vek.Select<Customer>();
        }
        public DataTable SelectAsDataTable()
        {
            return vek.GetDataTable(CommandType.Text,"select c.Customer_ID, c.FNAME,c.LNAME, c.TC_Number, c.Phone_Number, c.Birth_Date, oc.Occupation_Type_Name, sk.Skin_Type_Name from Customer c inner join Occupation_Type oc on oc.Occupation_Type_ID=c.Occupation_Type_ID inner join Skin_Type sk on sk.Skin_Type_ID=c.Skin_Type_ID");
        }
        public DataTable SelectAsDataTable(Customer Entity)
        {
            return vek.GetDataTable<Customer>(Entity);
        }
        public List<Customer> ListelebyBirtdate(DateTime gun)
        {
            return vek.ExecuteReader<Customer>(CommandType.Text, "Select * from Customer where Birth_Date=@a or Birth_Date=@b", gun.Date, gun.AddDays(1).Date);
        }
        
    }
}
