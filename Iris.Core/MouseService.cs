using System;
using System.Threading.Tasks;
using Iris.Infrastructure.Contracts;
using Iris.Infrastructure.Contracts.Services;
using Iris.Infrastructure.Models;

namespace Iris.Core
{
    internal class MouseService : IMouseService
    {
        public event MousePositionUpdateEventHandler MousePositionChanged;

        public Task<bool> SetMousePosition(MousePosition position)
        {
            throw new NotImplementedException();
        }
    }
}
