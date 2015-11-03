using Iris.Infrastructure.Models;

namespace Iris.Infrastructure.Contracts.Services
{
    public interface INetworkManager
    {
        void Initialize();

        void SendMousePositionUpdate(MousePosition position);

        event MousePositionUpdateEventHandler MousePositionUpdate;
    }
}
