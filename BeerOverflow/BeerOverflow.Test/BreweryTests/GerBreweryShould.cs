using BeerOverflow.Database;
using BeerOverflow.Models;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTO;
using BeerOverflow.Services.DTOMappers;
using BeerOverflow.Services.DTOs;
using BeerOverflow.Services.Services;
using BeerOverflow.Test;
using Elasticsearch.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Tests.BreweryServiceTests
{
    [TestClass]
    public class GetBreweryShould
    {
        [TestMethod]
        public void Return_Brewery_ById()
        {
            var options = Utils.GetOptions(nameof(Return_Brewery_ById));

            var brewery = new Brewery
            {
                Id = 1,
            };

            using (var arrangeContext = new BeerOverflowDbContext(options))
            {
                arrangeContext.Breweries.Add(brewery);
                arrangeContext.SaveChanges();
                var sut = new BreweryService(arrangeContext);
                var result = sut.GetBrewery(1);
                Assert.AreEqual(brewery.Id == 1, result.Id == 1);
            }
        }
        [TestMethod]
        public void Return_Breweries_Count()
        {
            var options = Utils.GetOptions(nameof(Return_Breweries_Count));
            var providerMock = new Mock<IDateTimeProvider>();

            using (var actContext = new BeerOverflowDbContext(options))
            {
                var sut = new BreweryService(actContext);
                var beers = actContext.Breweries.Count();
                var result = sut.GetAllBreweries();
                Assert.AreEqual(beers, result.Count);
            }
        }
        [TestMethod]
        public void Return_Beers_From_Brewery()
        {
            var options = Utils.GetOptions(nameof(Return_Beers_From_Brewery));

            var beer = new Beer
            {
                Id = 1
            };
            var coutry = new Country
            {
                Id = 1
            };
            var brewery = new Brewery
            {
                Id = 1
            };

            using (var actContext = new BeerOverflowDbContext(options))
            {
                actContext.Countries.Add(coutry);
                actContext.Breweries.Add(brewery);
                actContext.Beers.Add(beer);
                actContext.SaveChanges();
                var sut = new BreweryService(actContext);
                var result = sut.GetBrewery(1);
                Assert.AreEqual(beer.Id, result.Id);
            }
        }
        [TestMethod]
        public async Task Return_When_Brewery_IsCreated()
        {
            var options = Utils.GetOptions(nameof(Return_When_Brewery_IsCreated));

            var brewery = new BreweryDTO
            {
                Id = 1,
                Name = "Brewery"
            };

            using (var arrangeContext = new BeerOverflowDbContext(options))
            {
                var sut = new BreweryService(arrangeContext);
                await sut.CreateBreweryAsync(brewery);
                var result = sut.GetBrewery(1);
                Assert.AreEqual(brewery.Id, result.Id);
            }
        }
    }
}
