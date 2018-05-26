using System;
using System.Linq;
using Client.UI.Infrastructure.Context;
using Client.UI.Infrastructure.Context.Enum;
using HReception.Core;
using HReception.Core.Context.Enum;
using HReception.Logic.Services.Interfaces.Common;

namespace HReception.Logic.Services.Implementations.Common
{
    public class Generator : IGenerator
    {
        public string Next<T>() where T : IEntityBase
        {
            using (var context = SimulatorContext.CreateContext())
            {
                var name = typeof(T).FullName;
                var gencode = context.Gencodes.FirstOrDefault(aa => aa.EntityTypeFullName == name);
                if (gencode == null)
                    throw new ArgumentNullException(nameof(gencode));
                var next = gencode.Next();
                context.SaveChanges();
                return next;
            }
        }
    }
}