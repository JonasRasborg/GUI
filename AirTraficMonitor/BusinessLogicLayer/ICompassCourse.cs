using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BusinessLogicLayer
{
    public interface ICompassCourse
    {
        List<Track> CalcCourse(List<Track> tracks);
    }
}
