﻿using Microsoft.EntityFrameworkCore;
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
                Phone = phone,
                User = context.Users.Find(userId)
            };
            context.Clients.Add(client);
            return context.SaveChanges() != 0;
        }

        public Client GetClient(string id)
        {
            return context.Clients.FirstOrDefault(x => x.UserId == id); 
        }
        public List<Client> GetClients()
        {
            return context.Clients.ToList(); 
        }

        public string GetFullName(string clientId)
        {
            var client = context.Clients.Find(clientId);

            if (client == null)
            {
                return null;
            }
            return $"{client.FirstName} {client.LastName}";
        }

        public bool Remove(string clientId)
        {
            var client = context.Clients.Find(clientId);
            var myOrders = context.Orders.Where(x => x.CustomerId == client.UserId);

            if (client == null)
            {
                return false;
            }

            foreach (var order in myOrders)
            {
                context.Orders.Remove(order);
            }

            var user = context.Users.Find(client.UserId);

            context.Users.Remove(user);
            context.Clients.Remove(client);

            return context.SaveChanges() != 0;
        }

        public bool Update(string id, string firstName, string lastName, string phone, string address)
        {
            var client = context.Clients.Find(id);

            if (client == null)
            {
                return false;
            }
            client.FirstName = firstName;
            client.LastName = lastName;
            client.Phone = phone;
            client.Address = address;
            
            context.Clients.Update(client);
            return context.SaveChanges() != 0;
        }
    }
}
