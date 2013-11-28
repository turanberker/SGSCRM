using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VeriErisimKatmani;

namespace DataType
{
    public class Occupation_Type
    {
        [TableProp("PK")]
        public int Occupation_Type_ID { get; set; }

        [TableProp("Not")]
        public string Occupation_Type_Name { get; set; }
    }
}
