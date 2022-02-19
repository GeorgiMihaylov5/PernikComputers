using PernikComputers.Domain;

namespace PernikComputers.Abstraction
{
    public interface IComputerService
    {
        public bool Create(string processorId, string motherboardId, string ramId);  
    }
}
