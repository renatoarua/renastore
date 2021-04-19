using System;
using System.Collections.Generic;
using RenaStore.Domain.StoreContext.Entities;
using RenaStore.Domain.StoreContext.Queries;
using RenaStore.Domain.StoreContext.Repositories;

namespace RenaStore.Tests.Fakes
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public bool CheckDocument(string document)
        {
            return false;
        }

        public bool CheckEmail(string email)
        {
            return false;
        }

        public IEnumerable<ListCustomerResult> Get()
        {
            throw new NotImplementedException();
        }

        public GetCustomerResult Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public CustomerOrdersCountResult GetCustomerOrdersCount(string document)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ListCustomerOrdersResult> GetOrders(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save(Customer customer)
        {
            
        }
    }
}
