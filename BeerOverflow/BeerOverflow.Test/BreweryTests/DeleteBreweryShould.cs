using BeerOverflow.Database;
using BeerOverflow.Models;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Services;
using BeerOverflow.Test;
using Elasticsearch.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerOverflow.Tests.BreweryServiceTests
{
    [TestClass]
    public class DeleteBreweryShould
    {
        [TestMethod]
        public void Return_When_IsDeleted()
        {
            {
                var options = Utils.GetOptions(nameof(Return_When_IsDeleted));
                var providerMock = new Mock<IDateTimeProvider>();


                var brewery = new Brewery
                {
                    Id = 1,
                    Name = "Test Brewery",
                };

                using (var arrangeContext = new BeerOverflowDbContext(options))
                {
                    arrangeContext.Breweries.Add(brewery);
                    arrangeContext.SaveChangesAsync();

                }

                using (var actContext = new BeerOverflowDbContext(options))
                {
                    var sut = new BreweryService(actContext);

                    var result = sut.DeleteBreweryAsync(1);
                }
                using (var assertContext = new BeerOverflowDbContext(options))
                {
                    var actual = assertContext.Breweries.First(x => x.Id == 1);

                    Assert.IsTrue(actual.IsDeleted);
                }
            }

        }
    }
}
