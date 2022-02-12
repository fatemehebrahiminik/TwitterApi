using System;

namespace TwitterServer.Graph.Inputs
{
    public record CreateTwitteInput(Guid AuterId, string TwitteText);
}
