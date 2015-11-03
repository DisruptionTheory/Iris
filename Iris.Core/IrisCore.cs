using Iris.Infrastructure.Contracts.Services;
using Iris.Network;
using TinyIoC;

namespace Iris.Core
{
    public static class IrisCore
    {
        private readonly static TinyIoCContainer Container = new TinyIoCContainer();

        static IrisCore()
        {
            RegisterDependencies();

            InitializeNetworking();
        }

        private static void RegisterDependencies()
        {
            Container.Register<IMouseService, MouseService>().AsSingleton();

            Container.Register<INetworkManager, IrisNetworkManager>().AsSingleton();
        }

        private static void InitializeNetworking()
        {
            NetworkManager.Initialize();
        }

        private static INetworkManager NetworkManager => Container.Resolve<INetworkManager>();

        public static IMouseService MouseService => Container.Resolve<IMouseService>();
    }
}
