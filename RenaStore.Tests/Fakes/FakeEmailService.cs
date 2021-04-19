using RenaStore.Domain.StoreContext.Entities;
using RenaStore.Domain.StoreContext.Repositories;
using RenaStore.Domain.StoreContext.Services;

namespace RenaStore.Tests.Fakes
{
    public class FakeEmailService : IEmailService
    {
        void IEmailService.Send(string to, string from, string subject, string body)
        {
            
        }
    }
}
