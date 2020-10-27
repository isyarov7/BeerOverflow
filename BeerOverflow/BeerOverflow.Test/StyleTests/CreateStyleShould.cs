using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTOs;
using BeerOverflow.Services.Services;
using BeerOverflow.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Tests.StyleServiceTests
{
    [TestClass]
    public class CreateStyleShould
    {
        [TestMethod]
        public void GetCorrectStyle()
        {
            var options = Utils.GetOptions(nameof(GetCorrectStyle));

            var style = new Style
            {
                Id = 1,
            };

            using (var arrangeContext = new BeerOverflowDbContext(options))
            {
                arrangeContext.Styles.Add(style);
                arrangeContext.SaveChanges();
                var sut = new StyleService(arrangeContext);
                var result = sut.GetStyleAsync(1);
                Assert.AreEqual(style.Id, result.Id);

            }
        }
        [TestMethod]
        public async Task Return_When_Style_IsCreated()
        {
            var options = Utils.GetOptions(nameof(Return_When_Style_IsCreated));

            var style = new StyleDTO
            {
                Name = "NewStyle"
            };

            using (var arrangeContext = new BeerOverflowDbContext(options))
            {
                var sut = new StyleService(arrangeContext);
                await sut.CreateStyleAsync(style);
            }

            using (var actContext = new BeerOverflowDbContext(options))
            {
                var sut = new StyleService(actContext);
                var result = sut.GetStyleAsync(1);
                Assert.AreEqual(style.Name, result.Result.Name);

            }
        }
        [TestMethod]
        public async Task Return_When_Style_IsEdited()
        {
            var options = Utils.GetOptions(nameof(Return_When_Style_IsEdited));

            using (var arrangeContext = new BeerOverflowDbContext(options))
            {
                var style = new StyleDTO
                {
                    Id = 1,
                    Name = "OldStyle"
                };
                var newStyle = new StyleDTO
                {
                    Id = 1,
                    Name = "NewStyle"
                };

                var sut = new StyleService(arrangeContext);
                style.Name = newStyle.Name;
                await sut.UpdateStyleAsync(1, newStyle);

                Assert.AreEqual(style.Name, newStyle.Name);

            }
        }
    }
}

