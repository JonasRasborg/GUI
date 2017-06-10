using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class EventEventArgs : EventArgs
    {
        public List<Event> Events { get; set; }
    }
}
