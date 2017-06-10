using System;
using System.Collections.Generic;
using DTO;
using TransponderReceiver;

namespace DataAccesLayer
{
    public interface IRelay
    {
        event EventHandler<TrackEventArgs> RelayEvent;
    }
}