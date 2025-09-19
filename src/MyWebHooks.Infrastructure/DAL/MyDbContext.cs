using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyWebHooks.Infrastructure.Models;

namespace MyWebHooks.Infrastructure.DAL;

public class MyDbContext : DbContext
{
    public DbSet<Item> Item { get; set; }
    public DbSet<WebhookEvent> WebhookEvent  { get; set; }
    public DbSet<WebhookSubscription>  WebhookSubscription { get; set; }
    public DbSet<WebhookSubscriptionEvent> WebhookSubscriptionEvent { get; set; }
    
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {}
}
