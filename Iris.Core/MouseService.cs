using Iris.Infrastructure.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iris.Infrastructure.Models;

namespace Iris.Core
{
    public class MouseService : IMouseService
    {
        public Task<bool> SetMousePosition(MousePosition position)
        {
            throw new NotImplementedException();
        }
    }
}
