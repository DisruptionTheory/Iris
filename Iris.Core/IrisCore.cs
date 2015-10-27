using Iris.Infrastructure.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Core
{
    public static class IrisCore
    {
        private readonly static TinyIoC.TinyIoCContainer Container = new TinyIoC.TinyIoCContainer();

        static IrisCore()
        {
            RegisterDependencies();
        }

        private static void RegisterDependencies()
        {
            Container.AutoRegister();
        }

        public static IMouseService MouseService => Container.Resolve<IMouseService>();
    }
}
