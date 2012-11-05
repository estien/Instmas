using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Instmas.Data.Models;

namespace InstmasWin8App.DataModel
{
    public class CalendarWindow
    {
        public int DayNumber { get; set; }
        public bool WindowOpened { get; set; }
        public Picture Picture { get; set; }

        public override string ToString()
        {
            return "Dag " + DayNumber;
        }



        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CalendarWindow) obj);
        }

        protected bool Equals(CalendarWindow other)
        {
            return DayNumber == other.DayNumber;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = DayNumber;
                hashCode = (hashCode * 397) ^ WindowOpened.GetHashCode();
                hashCode = (hashCode * 397) ^ (Picture != null ? Picture.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
