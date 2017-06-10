using System.Collections.Generic;
using DataAccesLayer;
using DTO;

namespace BusinessLogicLayer
{
    public interface IEventDetector
    {
        List<Event> DetectEvents(List<Track> tracks);
    }
}