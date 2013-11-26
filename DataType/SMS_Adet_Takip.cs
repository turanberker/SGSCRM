using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VeriErisimKatmani;

namespace DataType
{
    public enum SMS_TYPE
    {
        BirtDate = 0,
        Informing = 1,
        Celebration = 2
    }
    public class SMS_Adet_Takip
    {
        [TableProp("PK")]
        public long SMS_Count_ID { get; set; }

        [TableProp("Not")]
        public int SMS_Count { get; set; }

        [TableProp("Not")]
        public DateTime? Date { get; set; }

        [TableProp("Not")]
        public SMS_TYPE SMS_Type { get; set; }

    }
}
