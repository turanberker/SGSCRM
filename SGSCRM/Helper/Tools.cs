using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSCRM.Helper
{
    class Tools
    {
        public Form Varmi(string formname, Main Anaform)
        {
            Form frm = null;
            foreach (Form item in Anaform.MdiChildren)
            {
                if (item.Name == formname)
                {
                    frm = item;
                    break;
                }
            }
            return frm;
        }
    }
}
