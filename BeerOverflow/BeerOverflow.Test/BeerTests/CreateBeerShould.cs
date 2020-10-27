using AutoMapper;
using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTO;
using BeerOverflow.Services.DTOMappers;
using BeerOverflow.Services.DTOs;
using BeerOverflow.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerOverflow.Test.BeerTests
{
    [TestClass]
    class CreateBeerShould
    {
        [TestMethod]
        public async Task ReturnCorrectBeerDTO_When_IsValid()
        {
            var options = Utils.GetOptions(nameof(ReturnCorrectBeerDTO_When_IsValid));

            var beer = new Beer
            {
                Id = 1,
                Name = "Kamenitza"
            };

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

            //Act&Assert

            var mapper = new Mock<IMapper>();

            mapper.Setup(x => x.Map<BeerDTO>(It.IsAny<Beer>()))
                .Returns(new BeerDTO
                {
                    Id = 1,
                    Name = "Kamenitza",
                    BreweryId = 1,
                });
            mapper.Setup(x => x.Map<BreweryDTO>(It.IsAny<Brewery>()))
                .Returns(new BreweryDTO
                {
                    Id = 1,
                    Name = "TopBrewery",
                    Beers = null,
                    CountryId = 1
                });
            mapper.Setup(x => x.Map<CountryDTO>(It.IsAny<Country>()))
                .Returns(new CountryDTO
                {
                    Id = 1,
                    Name = "Bulgaria"
                });

            using (var arrangeContext = new BeerOverflowDbContext(options))
            {
                arrangeContext.Countries.Add(country);
                arrangeContext.Breweries.Add(brewery);
                arrangeContext.Beers.Add(beer);
                arrangeContext.SaveChanges();
                var sut1 = new CountryService(arrangeContext);
                var sut2 = new BreweryService(arrangeContext);
                var sut3 = new BeerService(arrangeContext);
                await sut1.CreateCountryAsync(country.GetDTO());
                await sut2.CreateBreweryAsync(brewery.GetDTO());
                await sut3.CreateBeerAsync(beer.GetDTO());

                var result = sut3.GetBeer(1);

                Assert.IsTrue(result.Id == beer.Id);
            }
        }
    }
}
