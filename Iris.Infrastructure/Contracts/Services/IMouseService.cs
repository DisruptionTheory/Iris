using Iris.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Infrastructure.Contracts.Services
{
    public interface IMouseService
    {
        Task<bool> SetMousePosition(MousePosition position);
    }
}
