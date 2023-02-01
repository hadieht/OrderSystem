using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository.Common;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> logger;
    private readonly ApplicationDbContext context;


    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger,
                                            ApplicationDbContext context)
    {
        this.logger =  Guard.Against.Null(logger, nameof(logger));
        this.context = Guard.Against.Null(context, nameof(context)); ;
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data

        if (!context.Products.Any())
        {
            context.Products.Add(Product.Create(ProductType.PhotoBook, 19));
            context.Products.Add(Product.Create(ProductType.Calendar, 10));
            context.Products.Add(Product.Create(ProductType.Canvas, 16));
            context.Products.Add(Product.Create(ProductType.Cards, 4.7));
            context.Products.Add(Product.Create(ProductType.Mug, 94));

            await context.SaveChangesAsync();
        }
    }
}