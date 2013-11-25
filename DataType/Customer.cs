using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DataType
{
    public class Customer
    {
        [TableProp("PK")]
        public long Customer_ID { get; set; }

        [TableProp("Not")]
        public string FNAME { get; set; }

        [TableProp("Not")]
        public string LNAME { get; set; }

        [TableProp("Not")]
        public string TC_Number { get; set; }

        [TableProp("Not")]
        public string Phone_Number { get; set; }

        [TableProp("Not")]
        public int? Skin_Type_ID { get; set; }

        [TableProp("Not")]
        public string E_Mail { get; set; }

        [TableProp("Not")]
        public int? Occupation_Type_ID { get; set; }

        [TableProp("Not")]
        public DateTime? Birth_Date { get; set; }
    }
}
