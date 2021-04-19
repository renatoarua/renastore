using Microsoft.VisualStudio.TestTools.UnitTesting;
using RenaStore.Domain.StoreContext.ValueObjects;

namespace RenaStore.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        private Document validDocument;
        private Document invalidDocument;
        public DocumentTests()
        {
            validDocument = new Document("");
            invalidDocument = new Document("12345678901");
        }

        // [TestMethod]
        // public void ShouldReturnNotificationWhenDocumentIsNotValid()
        // {
        //     Assert.AreEqual(false, invalidDocument.IsValid);
        // }

        // [TestMethod]
        // public void ShouldReturnNotNotificationWhenDocumentIsValid()
        // {
        //     Assert.AreEqual(true, validDocument.IsValid);
        // }
    }
}
