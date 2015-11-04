using System.Threading.Tasks;
using Iris.Infrastructure.Models;

namespace Iris.Infrastructure.Contracts.Services
{
    public interface INetworkManager
    {
        void Initialize();

        Task<bool> SendMousePositionUpdate(MousePosition position);

        event MousePositionUpdateEventHandler MousePositionUpdate;
    }
}
