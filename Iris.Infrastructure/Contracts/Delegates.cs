using Iris.Infrastructure.Models;

namespace Iris.Infrastructure.Contracts
{
    public delegate void MousePositionUpdateEventHandler(MousePosition position);

    public delegate void NetworkDataReceivedEventHandler(byte[] data);
}
