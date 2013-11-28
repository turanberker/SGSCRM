using DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Businness
{
    public class EmployeeBS:DisposeEt
    {
        VeriErisimKatmani.Vek vek;
        public EmployeeBS()
        {
            vek = new VeriErisimKatmani.Vek(DAL.Default.localcon.ToString());
        }
        public List<Employee> Select(Employee item)
        {
            return vek.AndSelect<Employee>(item);
        }
    }
}
