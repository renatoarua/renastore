using System;
using FluentValidator;
using RenaStore.Domain.StoreContext.Enums;
using RenaStore.Shared.Entities;

namespace RenaStore.Domain.StoreContext.Entities
{
    public class Delivery : Entity
    {
        public Delivery(DateTime estimatedDeliveryDate)
        {
            CreateDate = DateTime.Now;
            EstimatedDeliveryDate = estimatedDeliveryDate;
            Status = EDeliveryStatus.Waiting;
        }

        public DateTime CreateDate { get; private set; }
        public DateTime EstimatedDeliveryDate { get; private set; }
        public EDeliveryStatus Status { get; private set; }

        public void Ship()
        {
            // Se Data estimada de entrega for passdo nao entregar
            Status = EDeliveryStatus.Shipped;
        }

        public void Cancel()
        {
            // Se o status ja estivr entregue, n pode cancelar
            
            
            
            Status = EDeliveryStatus.Canceled;
        }
    }
}