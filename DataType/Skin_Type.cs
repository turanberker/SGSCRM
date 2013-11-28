using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VeriErisimKatmani;

namespace DataType
{
    public class Skin_Type
    {
        [TableProp("PK")]
        public int Skin_Type_ID { get; set; }

        [TableProp("Not")]
        public string Skin_Type_Name { get; set; }
    }
}
