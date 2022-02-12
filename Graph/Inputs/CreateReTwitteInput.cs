using System;

namespace TwitterServer.Graph.Inputs
{
    public record CreateReTwitteInput(Guid TwitteId, Guid UserId);
}
