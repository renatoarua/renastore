using FluentValidator;
using FluentValidator.Validation;

namespace RenaStore.Domain.StoreContext.ValueObjects
{
    public class Email : Notifiable
    {
        public Email(string address)
        {
            Address = address;

            AddNotifications(new ValidationContract()
                .Requires()
                .IsEmail(Address, "Email", "O email e invalido."));
        }

        public string Address { get; private set; }

        public override string ToString()
        {
            return Address;
        }
    }
}