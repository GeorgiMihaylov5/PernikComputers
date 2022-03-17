using PernikComputers.Abstraction;
using PernikComputers.Data;
using PernikComputers.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PernikComputers.Service
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext context;

        public ClientService(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public bool CreateClient(string firstName, string lastName, string phone, string address, string userId)
        {
            if (context.Clients.Any(x => x.UserId == userId))
            {
                throw new InvalidOperationException("User already exist.");
            }

            Client client = new Client
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                UserId = userId,
                Phone = phone
            };
            context.Clients.Add(client);
            return context.SaveChanges() != 0;
        }

        public Client GetClient(string employeeId)
        {
            throw new System.NotImplementedException();
        }

        public List<Client> GetClients()
        {
            throw new System.NotImplementedException();
        }

        public string GetFullName(string employeeId)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(string employeeId)
        {
            throw new System.NotImplementedException();
        }
    }
}
