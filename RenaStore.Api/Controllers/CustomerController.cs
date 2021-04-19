using System.Collections.Generic;
using RenaStore.Domain.StoreContext.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using RenaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using RenaStore.Domain.StoreContext.Queries;
using RenaStore.Domain.StoreContext.Repositories;
using RenaStore.Domain.StoreContext.Handlers;
using RenaStore.Domain.StoreContext.Commands.CustomerCommands.Outputs;
using RenaStore.Shared.Commands;

namespace RenaStore.Api.Controllers
{
    [ApiController]
    [Route("v1/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        private readonly CustomerHandler _handler;
        public CustomerController(ICustomerRepository repository, CustomerHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpGet]
        [ResponseCache(Duration = 60)]
        public IEnumerable<ListCustomerResult> get()
        {
            return _repository.Get();
        }

        [HttpGet("{id}")]
        public GetCustomerResult getById(Guid id)
        {
            return _repository.Get(id);
        }

        [HttpGet("{id}/orders")]
        public IEnumerable<ListCustomerOrdersResult> getOrders(Guid id)
        {
            return _repository.GetOrders(id);
        }

        [HttpPost]
        public ICommandResult post([FromBody] CreateCustomerCommand command)
        {
            var result = _handler.Headle(command);
            return result;
        }
    }
}