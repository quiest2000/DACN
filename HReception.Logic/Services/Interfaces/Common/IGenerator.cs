using Client.UI.Infrastructure.Context.Enum;
using HReception.Core.Context.Enum;

namespace HReception.Logic.Services.Interfaces.Common
{
    public interface IGenerator
    {
        string Next<T>() where T : IEntityBase;
    }
}