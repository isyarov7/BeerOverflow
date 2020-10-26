using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeerOverflow.Database;

namespace BeerOverflow.Test
{
    public class Utils
    {
        public static DbContextOptions<BeerOverflowDbContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<BeerOverflowDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
