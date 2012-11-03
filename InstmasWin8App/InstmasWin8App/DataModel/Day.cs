using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstmasWin8App.DataModel
{
    public class Day
    {
        public int DayNumber { get; set; }

        public override string ToString()
        {
            return "Dag " + DayNumber;
        }
    }
}
