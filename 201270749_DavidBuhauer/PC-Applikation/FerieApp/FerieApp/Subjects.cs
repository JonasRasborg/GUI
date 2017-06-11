using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerieApp
{
    public class Subjects : ObservableCollection<string>
    {
        public Subjects()
        {
            Add("Trøjer");
            Add("T-Shirts");
            Add("Strømper");
            Add("Underbukser");
            Add("Bukser");
            Add("Skjorter");
        }
    }
}
