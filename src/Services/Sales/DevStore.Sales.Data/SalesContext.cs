using System;
using System.Linq;
using System.Threading.Tasks;

using DevStore.Core.Communication.Bus;
using DevStore.Core.Data;
using DevStore.Core.Messages;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;
using DevStore.Sales.Domain;

using Microsoft.EntityFrameworkCore;

namespace DevStore.Sales.Data
{
    public class SalesContext : DbContext, IUnitOfWork
    {
        private readonly IBusHandler _bus;

        public SalesContext(DbContextOptions<SalesContext> options,
                            IBusHandler bus)
            : base(options)
        {
            _bus = bus;
            // System.Diagnostics.Debugger.Launch(); 
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                    .SelectMany(t => t.GetProperties())
                    .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                if (property.GetColumnType() == null)
                    property.SetColumnType("decimal(18,2)");
            }

            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<IntegrationEvent>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.HasSequence<int>("MySequence").StartsAt(1000).IncrementsBy(1);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedDate").IsModified = false;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("UpdatedDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("UpdatedDate").IsModified = false;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedDate").CurrentValue = DateTime.Now;
                }
            }

            var sucess = await base.SaveChangesAsync() > 0;
            if (sucess) await _bus.PublishEvents(this);

            return sucess;
        }
    }
}