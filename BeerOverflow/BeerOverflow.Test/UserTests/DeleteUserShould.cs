using BeerOverflow.Database;
using BeerOverflow.Models;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Services;
using BeerOverflow.Test;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerOverflow.Tests.UserServiceTests
{
    [TestClass]
    public class DeleteUserShould
    {
        [TestMethod]
        public void ReturnTrue_When_ParamsAreValid()
        {
            {
                {
                    var options = Utils.GetOptions(nameof(ReturnTrue_When_ParamsAreValid));

                    using (var actContext = new BeerOverflowDbContext(options))
                    {
                        var user = new User
                        {
                            Id = 1,
                        };
                        actContext.Users.Add(user);

                        var result = actContext.Users.Remove(user);
                        Assert.IsTrue(actContext.Users.Count<User>() == 0);

                    }
                }
            }
        }
    }
}
