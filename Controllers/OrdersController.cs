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
    }

}
