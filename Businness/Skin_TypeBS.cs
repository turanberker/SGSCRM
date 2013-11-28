using DataType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Businness
{
    public class Skin_TypeBS:DisposeEt
    {
         VeriErisimKatmani.Vek vek;
         public Skin_TypeBS()
        {
            vek = new VeriErisimKatmani.Vek(DAL.Default.localcon.ToString());
        }
         public long Insert(Skin_Type Entity)
         {
             return vek.Insert<Skin_Type>(Entity);
         }
         public DataTable Listele()
         {
             return vek.GetDataTable<Skin_Type>();
         }
         public List<Skin_Type> andListele(Skin_Type Entity)
         {
             return vek.AndSelect<Skin_Type>(Entity);
         }
    }
}
