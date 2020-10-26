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

namespace BeerOverflow.Tests.BeerServiceTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]

        public void ReturnTrue_When_ParamsAreValid()
        {
            {
                var options = Utils.GetOptions(nameof(ReturnTrue_When_ParamsAreValid));
                var providerMock = new Mock<IDateTimeProvider>();


                var beer = new Beer
                {
                    Id = 1,
                };

                using (var arrangeContext = new BeerOverflowDbContext(options))
                {
                    arrangeContext.Beers.Add(beer);
                    arrangeContext.SaveChangesAsync();

                }

                using (var actContext = new BeerOverflowDbContext(options))
                {
                    var sut = new BeerService(actContext);

                    var result = sut.DeleteBeerAsync(1);

                }
                using (var assertContext = new BeerOverflowDbContext(options))
                {
                    var actual = assertContext.Beers.First(x => x.Id == 1);

                    Assert.IsFalse(actual.IsDeleted);

                }
            }
        }
    }
}
