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
            return vek.GetDataTable<Customer>();
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
