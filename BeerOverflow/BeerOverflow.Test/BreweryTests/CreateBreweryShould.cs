using AutoMapper;
using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTOs;
using BeerOverflow.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace BeerOverflow.Test.BreweryTests
{
    [TestClass]
    public class CreateBreweryShould
    {
        [TestMethod]
        public void ReturnCorrectBreweryDTO_When_IsValid()
        {
            //Arange
            var options = Utils.GetOptions(nameof(ReturnCorrectBreweryDTO_When_IsValid));

            var country = new Country
            {
                Id = 1,
                Name = "Bulgaria",
                Breweries = new List<Brewery>()
            };

            var brewery = new Brewery
            {
                Id = 1,
                Name = "TopBrewery",
                Country = country
            };

            using (var arrangeContext = new BeerOverflowDbContext(options))
            {
                arrangeContext.Breweries.Add(brewery);
                arrangeContext.SaveChanges();
            }

            //Act&Assert

            var mapper = new Mock<IMapper>();

            mapper.Setup(x => x.Map<BreweryDTO>(It.IsAny<Brewery>()))
                .Returns(new BreweryDTO
                {
                    Id = 1,
                    Name = "TopBrewery",
                    Beers = null,
                    CountryId = 1
                });

            using (var actContext = new BeerOverflowDbContext(options))
            {
                var sut = new BreweryService(actContext);
                var result = sut.GetBreweryAsync(1);

                Assert.IsTrue(result.Id == brewery.Id);
            }
        }

        [TestMethod]
        public void ReturnFalseWhen_Invalid()
        {
            var options = Utils.GetOptions(nameof(ReturnFalseWhen_Invalid));

            var country = new Country
            {
                Id = 1,
                Name = "Bulgaria",
                Breweries = new List<Brewery>()
            };

            var brewery = new Brewery
            {
                Id = 1,
                Name = "TopBrewery",
                Country = country
            };

            var falseBrewery = new Brewery
            {
                Id = 4,
                Name = "falseBrewery",
                Country = country
            };

            using (var arrangeContext = new BeerOverflowDbContext(options))
            {
                arrangeContext.Breweries.Add(brewery);
                arrangeContext.SaveChanges();
            }

            //Act&Assert

            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<BreweryDTO>(It.IsAny<Brewery>()))
                .Returns(new BreweryDTO
                {
                    Id = 1,
                    Name = "TopBrewery",
                    Beers = null,
                    CountryId = 1
                });

            using (var actContext = new BeerOverflowDbContext(options))
            {
                var sut = new BreweryService(actContext);
                var result = sut.GetBreweryAsync(1);

                Assert.IsFalse(result.Id == falseBrewery.Id);
            }
        }
    }
}
