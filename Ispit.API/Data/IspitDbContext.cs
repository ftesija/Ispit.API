using Ispit.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ispit.API.Data;

public class IspitDbContext : DbContext

{
    public IspitDbContext(DbContextOptions<IspitDbContext> options) : base(options)
    {

    }

    public DbSet<ShoppingItem> ShoppingItems { get; set; }
}
