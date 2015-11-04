using System.Threading.Tasks;
using Iris.Infrastructure.Contracts;
using Iris.Infrastructure.Contracts.Services;
using Iris.Infrastructure.Models;

namespace Iris.Core
{
    internal class MouseService : IMouseService
    {
        public event MousePositionUpdateEventHandler MousePositionChanged;

        public MouseService()
        {
            IrisCore.NetworkManager.MousePositionUpdate += OnMousePositionUpdate;
        }

        public async Task<bool> SetMousePosition(MousePosition position)
        {
            return await IrisCore.NetworkManager.SendMousePositionUpdate(position);
        }

        private void OnMousePositionUpdate(MousePosition position)
        {
            MousePositionChanged?.Invoke(position);
        }
    }
}
