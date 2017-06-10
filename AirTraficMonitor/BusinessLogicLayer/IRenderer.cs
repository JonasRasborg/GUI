using System;
using System.Collections.Generic;
using DTO;

namespace BusinessLogicLayer
{
    public interface IRenderer
    {
        event EventHandler<TrackEventArgs> RenderTracks;
        event EventHandler<EventEventArgs> RenderEvent;

    }
}