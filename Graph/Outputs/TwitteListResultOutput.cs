using System.Collections.Generic;

namespace TwitterServer.Graph.Outputs
{
    public record TwitteListResultOutput(bool result, IEnumerable<DataLayer.Models.Twitte> TwitteList);
}
