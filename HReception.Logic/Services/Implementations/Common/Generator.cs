using System;
using System.Linq;
using HReception.Logic.Context;
using HReception.Logic.Context.Enum;
using HReception.Logic.Services.Interfaces.Common;

namespace HReception.Logic.Services.Implementations.Common
{
    public class Generator : IGenerator
    {
        public string Next<T>() where T : IEntityBase
        {
            using (var context = SimulatorContext.Create())
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