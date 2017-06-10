using System;
using System.Collections.Generic;
using DataAccesLayer;
using DTO;

namespace BusinessLogicLayer
{
    public interface IController
    {
        event EventHandler<TrackEventArgs> ControllerEvent;
        event EventHandler<EventEventArgs> DetectEvent;

    }
}