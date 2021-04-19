using Microsoft.VisualStudio.TestTools.UnitTesting;
using RenaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;

namespace RenaStore.Tests.Commands
{
    [TestClass]
    public class CreateCustomerCommandTests
    {
        [TestMethod]
        public void ShouldValidateWhenCommandIsValid()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "Renato";
            command.LastName = "Miranda";
            command.Document = "06417907910";
            command.Email = "rena@gmail.com";
            command.Phone = "41998738495";
            
            Assert.AreEqual(true, command.Valid());
        }
    }
}
