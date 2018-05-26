namespace HReception.UI.Infrastructure
{
    public sealed class Bootstrap
    {
        private Bootstrap()
        {
            //Hidden ctor
        }
        public static void Register()
        {
            DependencyRegistrar();
        }
        static void DependencyRegistrar()
        {
            //FreshIOC.Container.Register<ISomeService, ISomeServiceImp>();
        }
    }
}
