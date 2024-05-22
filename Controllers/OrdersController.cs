using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.Include(o => o.OrderDetails).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.Include(o => o.OrderDetails)
                                              .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        //[HttpPost]
        //public async Task<ActionResult<Order>> PostOrder(Order order)
        //{
        //    foreach (var orderDetail in order.OrderDetails)
        //    {
        //        var product = await _context.Products.FindAsync(orderDetail.ProductId);
        //        if (product == null)
        //        {
        //            return BadRequest($"Product with ID {orderDetail.ProductId} not found.");
        //        }

        //        if (product.StockQuantity < orderDetail.Quantity)
        //        {
        //            return BadRequest($"Not enough stock for product {product.Name}. Available: {product.StockQuantity}, Requested: {orderDetail.Quantity}");
        //        }

        //        product.StockQuantity -= orderDetail.Quantity;
        //    }

        //    _context.Orders.Add(order);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteOrder(int id)
        //{
        //    var order = await _context.Orders
        //        .Include(o => o.OrderDetails)
        //        .FirstOrDefaultAsync(o => o.OrderId == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    foreach (var orderDetail in order.OrderDetails)
        //    {
        //        var product = await _context.Products.FindAsync(orderDetail.ProductId);
        //        if (product != null)
        //        {
        //            product.StockQuantity += orderDetail.Quantity;
        //        }
        //    }

        //    _context.Orders.Remove(order);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }

}
