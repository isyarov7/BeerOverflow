using BeerOverflow.Database;
using BeerOverflow.Models;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTO;
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
            }

            using (var actContext = new BeerOverflowDbContext(options))
            {
                var sut = new BeerService(actContext);
                var result = sut.GetBeerAsync(1);
                Assert.IsTrue(beer.Name == "Kamenitza");

            }
        }
        [TestMethod]
        public void ReturnCorrectBeerDTO_When_Id_IsCorrect()
        {
            var options = Utils.GetOptions(nameof(ReturnCorrectBeerDTO_When_Id_IsCorrect));

            var beer = new Beer
            {
                Id = 1,
            };


            using (var arrangeContext = new BeerOverflowDbContext(options))
            {
                arrangeContext.Beers.Add(beer);
                var sut = new BeerService(arrangeContext);
                var result = sut.GetBeerAsync(1);
                Assert.IsTrue(result.Id == 1);
            }
        }

        [TestMethod]
        public void Throw_When_BeerNotFound()
        {
            var options = Utils.GetOptions(nameof(Throw_When_BeerNotFound));

            using (var actContext = new BeerOverflowDbContext(options))
            {
                var sut = new BeerService(actContext);

                Assert.ThrowsException<ArgumentNullException>(() => sut.GetBeerAsync(0));
            }
        }
        [TestMethod]
        public void Return_All_Beers()
        {
            var options = Utils.GetOptions(nameof(Throw_When_BeerNotFound));

            using (var actContext = new BeerOverflowDbContext(options))
            {
                var sut = new BeerService(actContext);
                var beers = actContext.Beers.ToListAsync();
                actContext.SaveChangesAsync();

                var result = sut.GetAllBeersAsync();

                Assert.AreEqual(beers, result.Result.Count);

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
                var result = sut.GetAllBeersAsync();
                Assert.AreEqual(beers, result.Result.Count);
            }
        }
       
    }
}
