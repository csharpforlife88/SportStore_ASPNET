using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SportsStore.Models.Repository
{
    public class Repository
    {
        private readonly EFDbContext _context = new EFDbContext();

        public IEnumerable<Product> Products => _context.Products;

        public IEnumerable<Order> Orders => _context.Orders.Include(o => o.OrderLines.Select(ol => ol.Product));

        public void SaveOrder(Order order)
        {
            if (order.OrderId == 0)
            {
                order = _context.Orders.Add(order);

                foreach (var line in order.OrderLines)
                {
                    _context.Entry(line.Product).State = EntityState.Modified;
                }
            }
            else
            {
                var dbOrder = _context.Orders.Find(order.OrderId);

                if (dbOrder != null)
                {
                    dbOrder.Name = order.Name;
                    dbOrder.Line1 = order.Line1;
                    dbOrder.Line2 = order.Line2;
                    dbOrder.Line3 = order.Line3;
                    dbOrder.City = order.City;
                    dbOrder.State = order.State;
                    dbOrder.GiftWrap = order.GiftWrap;
                    dbOrder.Dispatched = order.Dispatched;
                }
            }

            _context.SaveChanges();
        }
    }
}