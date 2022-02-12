using System;

namespace TwitterServer.Graph.Outputs
{
    public record LoginResultOutput(bool result, string message, string value);

}
