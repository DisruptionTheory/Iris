using System.Threading.Tasks;
using Iris.Infrastructure.Models;

namespace Iris.Infrastructure.Contracts.Services
{
    public interface IMouseService
    {
        event MousePositionUpdateEventHandler MousePositionChanged;

        Task<bool> SetMousePosition(MousePosition position);
    }
}
