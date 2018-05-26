using FreshMvvm;
using HReception.Logic.Services.Interfaces.Common;
using HReception.Logic.Services.Implementations.Common;
using HReception.Logic.Services.Interfaces.Patients;
using HReception.Logic.Services.Interfaces.Payment;
using HReception.Logic.Services.Implementations.Payment;
using HReception.Logic.Services.Implementations.Patients;
using HReception.Logic.Services.Interfaces.Settings;
using HReception.Logic.Services.Implementations.Settings;
namespace HReception.Logic.Infrastructure
{
    public sealed class Bootstrap
    {
        private Bootstrap()
        {
            //Hidden ctor
        }
        public static void Register()
        {
            FreshIOC.Container.Register<IGenerator, Generator>();
            FreshIOC.Container.Register<ISecurityService, SecurityService>();
            FreshIOC.Container.Register<IPatientService, PatientService>();
            FreshIOC.Container.Register<IPaymentService, PaymentService>();
            FreshIOC.Container.Register<ISettingService, SettingService>();
        }
    }
}
