﻿using EcommerceWeb.API.Repositories.Interfaces;
using EcommerceWeb.Data.DatabaseContext;
using EcommerceWeb.Data.Entities;

namespace EcommerceWeb.API.Repositories
{
    public class UserRepository : GenericRepository<AUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }
    }
}
