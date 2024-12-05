using Ispit.API.Data;
using Ispit.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ispit.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingItemsController : ControllerBase
{
    private readonly IspitDbContext _context;

    public ShoppingItemsController(IspitDbContext context)
    {
        _context = context;
    }

    // GET: api/ShoppingItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShoppingItem>>> GetShoppingItems()
    {
        return await _context.ShoppingItems.ToListAsync();
    }

    // GET: api/ShoppingItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ShoppingItem>> GetShoppingItem(int id)
    {
        var shoppingItem = await _context.ShoppingItems.FindAsync(id);

        if (shoppingItem == null)
        {
            return NotFound();
        }

        return shoppingItem;
    }

    // POST: api/ShoppingItems
    [HttpPost]
    public async Task<ActionResult<ShoppingItem>> PostShoppingItem(ShoppingItem shoppingItem)
    {
        _context.ShoppingItems.Add(shoppingItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetShoppingItem", new { id = shoppingItem.Id }, shoppingItem);
    }

    // PUT: api/ShoppingItems/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutShoppingItem(int id, ShoppingItem shoppingItem)
    {
        if (id != shoppingItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(shoppingItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShoppingItemExists(id))
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

    // DELETE: api/ShoppingItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShoppingItem(int id)
    {
        var shoppingItem = await _context.ShoppingItems.FindAsync(id);
        if (shoppingItem == null)
        {
            return NotFound();
        }

        _context.ShoppingItems.Remove(shoppingItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ShoppingItemExists(int id)
    {
        return _context.ShoppingItems.Any(e => e.Id == id);
    }
}

