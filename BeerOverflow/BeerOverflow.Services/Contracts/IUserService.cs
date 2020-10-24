using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.Contracts
{
    public interface IUserService
    {
        public interface IUserService
        {
            public User BanUser(int id);

            public User DeleteUser(int id);

            public string GetName(int id);

            public User GetUser(int id);

            public UserDTO GetUser(string username);

            public ICollection<User> GetAllUsers();

        }
    }
}
