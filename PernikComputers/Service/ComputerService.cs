using PernikComputers.Abstraction;
using PernikComputers.Data;
using PernikComputers.Domain;

namespace PernikComputers.Service
{
    public class ComputerService : IComputerService
    {
        private readonly ApplicationDbContext context;

        public ComputerService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public bool Create(string processorId, string motherboardId, string ramId)
        {
            var computer = new Computer
            {
                ProcessorId = processorId,
                MotherboardId = motherboardId,
                RamId = ramId
            };
            context.Computers.Add(computer);
            return context.SaveChanges() != 0;
        }

    }
}
