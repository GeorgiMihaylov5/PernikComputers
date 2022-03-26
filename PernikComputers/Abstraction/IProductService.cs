﻿using PernikComputers.Domain;
using System.Collections.Generic;

namespace PernikComputers.Abstraction
{
    public interface IProductService
    {
        public List<Product> GetProducts();
        public bool RemoveProduct(string id);
        //public Product GetProduct(string id);
    }
}