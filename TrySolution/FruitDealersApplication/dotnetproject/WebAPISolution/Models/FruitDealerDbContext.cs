using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookStoreDBFirst.Models;

public class FruitDealerDbContext : DbContext
{
    public FruitDealerDbContext(DbContextOptions<FruitDealerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Fruit> Fruits { get; set; }
    public virtual DbSet<Dealer> Dealers { get; set; }
}
