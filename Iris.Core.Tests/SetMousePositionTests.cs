using Iris.Core;
using Iris.Infrastructure.Models;
using NUnit.Framework;

namespace Iris.Core.Tests
{
    public class SetMousePositionTests
    {
        [Test]
        public void ATestyTest()
        {
            MousePosition postion = new MousePosition();

            postion.X = 10;

            postion.Y = 10;

            IrisCore.MouseService.SetMousePosition(postion);
        }
    }
}
