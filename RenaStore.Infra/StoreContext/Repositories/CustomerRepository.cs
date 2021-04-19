using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using RenaStore.Domain.StoreContext.Entities;
using RenaStore.Domain.StoreContext.Queries;
using RenaStore.Domain.StoreContext.Repositories;
using RenaStore.Infra.StoreContext.DataContexts;

namespace RenaStore.Infra.StoreContext.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RenaDataContext _context;
        public CustomerRepository(RenaDataContext context)
        {
            _context = context;
        }

        public bool CheckDocument(string document)
        {
            return _context
                .Connection
                .Query<bool>(
                    "spCheckDocument",
                    new { Document = document },
                    commandType: CommandType.StoredProcedure)
                .FirstOrDefault();
        }

        public bool CheckEmail(string email)
        {
            return _context
                .Connection
                .Query<bool>(
                    "spCheckEmail",
                    new { Email = email },
                    commandType: CommandType.StoredProcedure)
                .FirstOrDefault();
        }

        public IEnumerable<ListCustomerResult> Get()
        {
            return _context
                .Connection
                .Query<ListCustomerResult>(
                    "SELECT [Id], CONCAT([FirstName], '', [LastName]) as [Name], [Document], [Email] FROM [Customer]");
        }

        public GetCustomerResult Get(Guid id)
        {
            return _context
                .Connection
                .Query<GetCustomerResult>(
                    "SELECT [Id], CONCAT([FirstName], '', [LastName]) as [Name], [Document], [Email] FROM [Customer] WHERE [Id] = @Id",
                    new { Id = id }
                    ).FirstOrDefault();
        }

        public CustomerOrdersCountResult GetCustomerOrdersCount(string document)
        {
            return _context
                .Connection
                .Query<CustomerOrdersCountResult>(
                "spGetCustomerOrdersCount",
                new { Document = document },
                commandType: CommandType.StoredProcedure)
            .FirstOrDefault();
        }

        public IEnumerable<ListCustomerOrdersResult> GetOrders(Guid id)
        {
            return _context
                .Connection
                .Query<ListCustomerOrdersResult>(
                    "SELECT [Id], CONCAT([FirstName], '', [LastName]) as [Name], [Document], [Email] FROM [Customer] WHERE [Id] = @Id",
                    new { Id = id });
        }

        public void Save(Customer customer)
        {
            _context.Connection.Execute("spCreateCustomer",
                new
                {
                    Id = customer.Id,
                    FirstName = customer.Name.FirstName,
                    LastName = customer.Name.LastName,
                    Document = customer.Document.Number,
                    Email = customer.Email.Address,
                    Phone = customer.Phone,
                }, commandType: CommandType.StoredProcedure);

            foreach (var address in customer.Addresses)
            {
                _context.Connection.Execute("spCreateAddress",
                new
                {
                    Id = address.Id,
                    CustomerId = customer.Id,
                    Number = address.Number,
                    Complement = address.Complement,
                    District = address.District,
                    City = address.City,
                    State = address.State,
                    Country = address.Country,
                    ZipCode = address.ZipCode,
                    Tyoe = address.Type,
                }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}