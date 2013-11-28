using Businness;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SGSCRM.Helper
{
    class cmbDoldurucular
    {
        DataTable dt = new DataTable();
        public DataTable CiltTipiDoldur()
        {
            using (Skin_TypeBS bs = new Skin_TypeBS())
            {
                dt = bs.Listele();
            }
            return dt;
        }
        public DataTable MeslekDoldur()
        {
            using (Occupation_TypeBS bs = new Occupation_TypeBS())
            {
                dt = bs.Listele();
            }
            return dt;
        }
    }
}
