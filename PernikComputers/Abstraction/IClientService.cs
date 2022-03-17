﻿using PernikComputers.Domain;
using System.Collections.Generic;

namespace PernikComputers.Abstraction
{
    public interface IClientService
    {
        public List<Client> GetClients();
        public Client GetClient(string employeeId);
        public bool CreateClient(string firstName, string lastName, string phone, string address, string userId);
        public bool Remove(string employeeId);
        public string GetFullName(string employeeId);
    }
}