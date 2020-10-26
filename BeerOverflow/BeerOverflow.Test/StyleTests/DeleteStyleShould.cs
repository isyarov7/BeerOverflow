using BeerOverflow.Database;
using BeerOverflow.Models;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Services;
using BeerOverflow.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerOverflow.Tests.StyleServiceTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public void ReturnTrue_When_ParamsAreValid()
        {
            {
                var options = Utils.GetOptions(nameof(ReturnTrue_When_ParamsAreValid));


                var style = new Style
                {
                    Id = 1,
                };

                using (var arrangeContext = new BeerOverflowDbContext(options))
                {
                    arrangeContext.Styles.Add(style);
                    arrangeContext.SaveChangesAsync();

                }

                using (var actContext = new BeerOverflowDbContext(options))
                {
                    var sut = new StyleService(actContext);

                    var result = sut.DeleteStyleAsync(1);

                }
                using (var assertContext = new BeerOverflowDbContext(options))
                {
                    var actual = assertContext.Styles.First(x => x.Id == 1);

                    Assert.IsTrue(actual.IsDeleted);

                }
            }
        }
    }
}
