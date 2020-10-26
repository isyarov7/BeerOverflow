using AutoMapper;
using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTOs;
using BeerOverflow.Services.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Test.BreweryTests
{
    [TestClass]
    public class DeleteBreweryShould
    {
        [TestMethod]
        public void ReturnDeletedDTO_Should()
        {
            //Arange
            var options = Utils.GetOptions(nameof(ReturnDeletedDTO_Should));

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
                var result = sut.DeleteBreweryAsync(1);
                
                Assert.IsTrue(brewery.IsDeleted == true);
            }
        }
    }
}