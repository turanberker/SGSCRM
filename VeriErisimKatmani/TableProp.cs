using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VeriErisimKatmani
{
   public class TableProp : System.Attribute
    {
        public readonly string PK;

        public TableProp(string PK)
        {
            this.PK = PK;
        }
    }
}
