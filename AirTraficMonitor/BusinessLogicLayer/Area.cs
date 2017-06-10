using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BusinessLogicLayer
{
    public class Area:IArea
    {
        private int _xMin;
        private int _xMax;
        private int _yMin;
        private int _yMax;

        public Area()
        {
            _xMin = 10000;
            _xMax = 90000;
            _yMin = 10000;
            _yMax = 90000;
        }

        public List<Track> CheckArea(List<Track> tracks)
        {
            foreach (var item in tracks)
            {
                if (item.Position.X >= _xMin && item.Position.X <= _xMax && item.Position.Y >= _yMin &&
                    item.Position.Y <= _yMax)
                {
                    item.InArea = true;
                }
                else
                {
                    item.InArea = false;
                }
            }
            return tracks;
        }

    }
}
