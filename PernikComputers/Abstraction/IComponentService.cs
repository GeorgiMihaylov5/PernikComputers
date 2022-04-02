using PernikComputers.Domain;
using PernikComputers.Domain.Enum;
using System.Collections.Generic;

namespace PernikComputers.Abstraction
{
    public interface IComponentService : ICreatableComponents, IUpdatableComponents
    {
        public List<Product> GetComponents();
    }
}
