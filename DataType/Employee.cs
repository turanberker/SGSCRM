using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VeriErisimKatmani;

namespace DataType
{
   public class Employee
    {
        [TableProp("PK")]
       public long? Employee_ID { get; set; }

        [TableProp("Not")]
        public string FNAME { get; set; }

        [TableProp("Not")]
        public string LNAME { get; set; }

        [TableProp("Not")]
        public string E_Mail { get; set; }

        [TableProp("Not")]
        public string Phone_Number { get; set; }

        [TableProp("Not")]
        public string User_Name { get; set; }

        [TableProp("Not")]
        public string Password { get; set; }
    }
}
