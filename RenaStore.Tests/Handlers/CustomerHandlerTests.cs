using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RenaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using RenaStore.Domain.StoreContext.Handlers;
using RenaStore.Tests.Fakes;

namespace RenaStore.Tests.Handlers
{
    [TestClass]
    public class CustomerHandlerTests
    {
        [TestMethod]
        public void ShoulRegisterCustomerWhenCommandIsValid()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "Renato";
            command.LastName = "Miranda";
            command.Document = "06417907910";
            command.Email = "rena@gmail.com";
            command.Phone = "41998738495";
            
            var handler = new CustomerHandler(new FakeCustomerRepository(), new FakeEmailService());
            var result = handler.Headle(command);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(true, handler.IsValid); 
        }
    }
}
