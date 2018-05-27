using HReception.Logic.Context.Enum;
using HReception.Logic.Context;

namespace HReception.Logic.Services.Interfaces.Common
{
    public interface IGenerator
    {
        string Next<T>() where T : IEntityBase;
    }
}