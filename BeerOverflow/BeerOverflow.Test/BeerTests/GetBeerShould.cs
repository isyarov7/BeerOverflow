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
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Tests.NewFolder
{
    [TestClass]
    public class GetBeerShould
    {
        [TestMethod]
        public async Task Return_When_Beer_IsCreated()
        {
            var options = Utils.GetOptions(nameof(Return_When_Beer_IsCreated));

            var beer = new BeerDTO
            {
                Name = "Kamenitza"
            };

            using (var arrangeContext = new BeerOverflowDbContext(options))
            {
                var sut = new BeerService(arrangeContext);
                await sut.CreateBeerAsync(beer);
                Assert.IsTrue(beer.Name == "Kamenitza");
            }
        }
        [TestMethod]
        public async Task ReturnCorrectBeerDTO_When_Id_IsCorrect()
        {
            var options = Utils.GetOptions(nameof(ReturnCorrectBeerDTO_When_Id_IsCorrect));

            var beer = new BeerDTO
            {
                Id = 2,
            };

            using (var arrangeContext = new BeerOverflowDbContext(options))
            {
                var sut = new BeerService(arrangeContext);
                await sut.CreateBeerAsync(beer);
                await arrangeContext.SaveChangesAsync();
                var result = sut.GetBeer(2);
                Assert.AreEqual(result.Id == 2, beer.Id == 2);
            }
        }

        [TestMethod]
        public void Throw_When_BeerNotFound()
        { 
            var options = Utils.GetOptions(nameof(Throw_When_BeerNotFound));

            var beer = new Beer
            {
                Id = 1,
            };
            using (var actContext = new BeerOverflowDbContext(options))
            {
                actContext.Beers.Add(beer);
                var sut = new BeerService(actContext);
                var result = sut.GetBeer(2);

                Assert.ThrowsException<ArgumentNullException>(() => result);
            }
        }
        [TestMethod]
        public void Return_All_Beers()
        {
            var options = Utils.GetOptions(nameof(Throw_When_BeerNotFound));

            using (var actContext = new BeerOverflowDbContext(options))
            {
                var sut = new BeerService(actContext);
                var beers = actContext.Beers.Count();
                actContext.SaveChanges();

                var result = sut.GetAllBeers();

                Assert.AreEqual(beers, result.Count);

            }
        }
        [TestMethod]
        public void Return_All_Beers_Count()
        {
            var options = Utils.GetOptions(nameof(Return_All_Beers_Count));
            var providerMock = new Mock<IDateTimeProvider>();

            using (var actContext = new BeerOverflowDbContext(options))
            {
                var sut = new BeerService(actContext);
                var beers = actContext.Beers.Count();
                var result = sut.GetAllBeers();
                Assert.AreEqual(beers, result.Count);
            }
        }
       
    }
}
