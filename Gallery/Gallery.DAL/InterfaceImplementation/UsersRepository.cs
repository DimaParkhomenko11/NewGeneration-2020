﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;

namespace Gallery.DAL.InterfaceImplementation
{
    public class UsersRepository : IRepository
    {
        private readonly UserContext _context;

        public UsersRepository(UserContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public async Task<bool> IsUserExistAsync(string username, string plainPassword)
        {

            return await _context.Users.AnyAsync(u => u.Email == username.Trim().ToLower() && u.Password == plainPassword.Trim());
            
        }
    }
}