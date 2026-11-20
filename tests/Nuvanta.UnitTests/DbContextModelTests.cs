using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Nuvanta.Infrastructure.Persistence;

namespace Nuvanta.UnitTests;

/// <summary>Validates the EF model without needing a running SQL Server.</summary>
public class DbContextModelTests
{
    private static IModel BuildModel()
    {
        var options = new DbContextOptionsBuilder<NuvantaDbContext>()
            .UseSqlServer("Server=.;Database=nuvanta;Trusted_Connection=True;TrustServerCertificate=True")
            .Options;
        using var db = new NuvantaDbContext(options);
        return db.GetService<IDesignTimeModel>().Model;
    }

    [Fact]
    public void Model_Contains_All_41_Tables()
        => Assert.Equal(41, BuildModel().GetEntityTypes().Count());

    [Fact]
    public void Tables_With_Triggers_Are_Declared()
    {
        var withTriggers = BuildModel().GetEntityTypes()
            .Where(e => e.GetDeclaredTriggers().Any())
            .Select(e => e.GetTableName()!)
            .ToHashSet();

        var expected = new HashSet<string>
    {
        "BudgetTransfers", "Budgets", "InventoryBalances",
        "LedgerEntries", "PurchaseRequests", "Users"
    };

        Assert.True(expected.SetEquals(withTriggers));
    }
}
