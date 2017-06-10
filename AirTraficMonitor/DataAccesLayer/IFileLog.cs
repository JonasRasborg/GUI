using System.Collections.Generic;
using DTO;

namespace DataAccesLayer
{
    public interface IFileLog
    {
        void Log(List<Event> newEvents);
    }
}