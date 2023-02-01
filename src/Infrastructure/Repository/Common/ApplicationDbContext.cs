using Ardalis.GuardClauses;
using Domain.Entities;
using Infrastructure.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Repository.Common;

public class ApplicationDbContext : DbContext
{
    private readonly IMediator mediator;
    private readonly AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
                                DbContextOptions options,
                                AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
                                IMediator mediator) : base(options)
    {
        this.mediator =  Guard.Against.Null(mediator, nameof(mediator));

        this.auditableEntitySaveChangesInterceptor = Guard.Against.Null(auditableEntitySaveChangesInterceptor, nameof(auditableEntitySaveChangesInterceptor)); ;
    }

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<Product> Products => Set<Product>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
