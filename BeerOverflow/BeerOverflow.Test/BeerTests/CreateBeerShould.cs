using AutoMapper;
using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTOs;
using BeerOverflow.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Test.BeerTests
{
    [TestClass]
    class CreateBeerShould
    {
        [TestMethod]
        public void ReturnCorrectBeerDTO_When_IsValid()
        {
            //Arange
            var options = Utils.GetOptions(nameof(ReturnCorrectBeerDTO_When_IsValid));

            var beer = new Beer
            {
                Id = 1,
                Name = "Kamenitza"
            };

            using (var arrangeContext = new BeerOverflowDbContext(options))
            {
                arrangeContext.Beers.Add(beer);
                arrangeContext.SaveChanges();
            }

            //Act&Assert

            var mapper = new Mock<IMapper>();

            mapper.Setup(x => x.Map<BeerDTO>(It.IsAny<Beer>()))
                .Returns(new BeerDTO
                {
                    Id = 1,
                    Name = "Kamenitza",
                });

            using (var actContext = new BeerOverflowDbContext(options))
            {
                var sut = new BeerService(actContext);
                var result = sut.GetBeerAsync(1);

                Assert.IsTrue(result.Id == beer.Id);
            }
        }

    }
}
