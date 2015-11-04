using Iris.Core;
using Iris.Infrastructure.Models;

namespace Iris.WinForms.Mouse
{
    class MousePositionReceiver
    {
        public MousePositionReceiver()
        {
            IrisCore.MouseService.MousePositionChanged += MouseService_MousePositionChanged;
        }

        private void MouseService_MousePositionChanged(MousePosition position)
        {
            throw new System.NotImplementedException();
        }
    }
}
