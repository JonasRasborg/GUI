using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BusinessLogicLayer
{
    public interface IArea
    {
        List<Track> CheckArea(List<Track> tracks);
    }
}
