using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidator;
using RenaStore.Domain.StoreContext.Enums;
using RenaStore.Shared.Entities;

namespace RenaStore.Domain.StoreContext.Entities
{
    public class Order : Entity
    {
        private readonly IList<OrderItem> _items;
        private readonly IList<Delivery> _deliveries;
        public Order(Customer customer)
        {
            Customer = customer;
            CreateDate = DateTime.Now;
            Status = EOrderStatus.Created;
            _items = new List<OrderItem>();
            _deliveries = new List<Delivery>();
        }

        public Customer Customer { get; private set; }
        public string Number { get; private set; }
        public DateTime CreateDate { get; private set; }
        public EOrderStatus Status { get; private set; } 
        public IReadOnlyCollection<OrderItem> Items => _items.ToArray();
        public IReadOnlyCollection<Delivery> Deliveries => _deliveries.ToArray();

        public void AddItem(Product product, decimal quantity)
        {
            if (quantity > product.QuantityOnHand)
                AddNotification("OrderItem", $"Produto {product.Title} nao tem {quantity} itens em estoque.");

            var item = new OrderItem(product, quantity);
            _items.Add(item);
        }

        // Criar um pedido
        public void Place()
        {
            // Gera o numero do pedido
            Number = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();

            // validar
            if (_items.Count == 0)
                AddNotification("Order", "Esse pedido nao possui itens");
        }

        // Pagar um pedido
        public void Pay()
        {
            Status = EOrderStatus.Paid;
        }

        // Enviar um pedido
        public void Ship()
        {
            // A cada 5 produtos e uma entrega
            var deliveries = new List<Delivery>();
            var count = 1;

            // quebra as entrega
            foreach (var item in _items)
            {
                if (count == 5)
                {
                    count = 1;
                    deliveries.Add(new Delivery(DateTime.Now.AddDays(5)));
                }
                count++;
            }

            // envia as entregas 
            deliveries.ForEach(c => c.Ship());

            // adiciona as entregas ao pedido
            deliveries.ForEach(c => _deliveries.Add(c));
        }

        // Cancelar um pedido
        public void Cancel()
        {
            Status = EOrderStatus.Canceled;
            _deliveries.ToList().ForEach(c => c.Cancel());
        }
    }
}