using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOs;
using BeerOverflow.Services.MappersDTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflow.Services.Services
{
    public class UserService : IUserService
    {
        private readonly BeerOverflowDbContext context;
        private readonly IBeerService beerService;
        public UserService(BeerOverflowDbContext context, IBeerService beerService)
        {
            this.context = context;

            this.beerService = beerService;
        }

        public User BanUser(int id)
        {
            var user = this.context.Users
                .FirstOrDefault(u => u.Id == id);


            user.IsBanned = true;

            this.context.SaveChanges();

            return user;
        }

        public User DeleteUser(int id)
        {
            var user = this.context.Users
                .FirstOrDefault(u => u.Id == id);

            user.IsDeleted = true;

            this.context.SaveChanges();

            return user;
        }

        public string GetName(int id)
        {
            return this.context.Users.FirstOrDefault(u => u.Id == id).UserName;
        }

        public User GetUser(int id)
        {

            return this.context.Users.FirstOrDefault(u => u.Id == id);
        }

        public UserDTO GetUser(string username)
        {

            var user = this.context.Users
                .Include(u => u.Wishlist)
                .FirstOrDefault(u => u.UserName == username).GetDTO();

            return user;
        }

        public ICollection<User> GetAllUsers()
        {
            ICollection<User> users = this.context.Users.ToList();

            return users;
        }
    }
}