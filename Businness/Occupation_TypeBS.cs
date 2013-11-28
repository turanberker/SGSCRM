using DataType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Businness
{
    public class Occupation_TypeBS:DisposeEt
    {
        VeriErisimKatmani.Vek vek;
        public Occupation_TypeBS()
        {
            vek = new VeriErisimKatmani.Vek(DAL.Default.localcon.ToString());
        }
        public int Insert(Occupation_Type Entity)
        {
            return Convert.ToInt32( vek.Insert<Occupation_Type>(Entity));
        }
        public DataTable Listele()
        {
            return vek.GetDataTable<Occupation_Type>();
        }
    }
}
