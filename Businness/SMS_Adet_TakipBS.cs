using DataType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Businness
{
   public class SMS_Adet_TakipBS: DisposeEt
    {
        VeriErisimKatmani.Vek vek;
        public SMS_Adet_TakipBS()
        {
            vek = new VeriErisimKatmani.Vek(DAL.Default.localcon.ToString());
        }
        public bool Delete(SMS_Adet_Takip Entity)
        {
            return vek.Delete<SMS_Adet_Takip>(Entity);
        }
        public bool Update(SMS_Adet_Takip Entity)
        {
            return vek.Update<SMS_Adet_Takip>(Entity);
        }
        public long Insert(SMS_Adet_Takip Entity)
        {
            return vek.Insert<SMS_Adet_Takip>(Entity);
        }
        public List<SMS_Adet_Takip> Listele(SMS_Adet_Takip Entity)
        {
            return vek.Select<SMS_Adet_Takip>(Entity);
        }
        public List<SMS_Adet_Takip> Listele()
        {
            return vek.Select<SMS_Adet_Takip>();
        }
        public DataTable SelectAsDataTable()
        {
            return vek.GetDataTable<SMS_Adet_Takip>();
        }
        public DataTable SelectAsDataTable(SMS_Adet_Takip Entity)
        {
            return vek.GetDataTable<SMS_Adet_Takip>(Entity);
        }
    }
}
