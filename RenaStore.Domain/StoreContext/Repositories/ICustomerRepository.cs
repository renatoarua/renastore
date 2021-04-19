using System;
using System.Collections.Generic;
using RenaStore.Domain.StoreContext.Entities;
using RenaStore.Domain.StoreContext.Queries;

namespace RenaStore.Domain.StoreContext.Repositories
{
    public interface ICustomerRepository
    {
        bool CheckDocument(string document);
        bool CheckEmail(string email);
        void Save(Customer customer);
        CustomerOrdersCountResult GetCustomerOrdersCount(string document);
        IEnumerable<ListCustomerResult> Get();
        GetCustomerResult Get(Guid id);
        IEnumerable<ListCustomerOrdersResult> GetOrders(Guid id);
    }
}