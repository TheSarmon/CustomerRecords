﻿using CustomerRecords.Application.Models;
using MongoDB.Driver;

namespace CustomerRecords.Application.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private MongoClient _mongoClient;
        private IMongoDatabase _database;
        private IMongoCollection<Customer> _customersTable;

        public CustomerRepository()
        {
            _mongoClient = new MongoClient("mongodb://localhost:27017/");
            _database = _mongoClient.GetDatabase("CustomerRecordsDB");
            _customersTable = _database.GetCollection<Customer>("Customers");
        }

        public List<Customer> GetAll()
        {
            return _customersTable.Find(FilterDefinition<Customer>.Empty).ToList();
        }

        public Customer Create(Customer customer)
        {
            var customerObj = _customersTable.Find(c => c.Id == customer.Id).FirstOrDefault();
            if (customerObj == null)
            {
                _customersTable.InsertOne(customer);
            }
            else
            {
                _customersTable.ReplaceOne(c => c.Id == customer.Id, customer);
            }
            return customer;
        }

        public Customer GetById(string customerId)
        {
            return _customersTable.Find(c => c.Id == customerId).FirstOrDefault();
        }

        public void Delete(string customerId) 
        {
            _customersTable.DeleteOne(c => c.Id == customerId);
        }
    }
}