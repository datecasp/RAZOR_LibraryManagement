using Microsoft.EntityFrameworkCore;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Domain.Models;
using RAZOR_LibraryManagement.Infra.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LM_DbContext _lM_DbContext;

        public UserRepository(LM_DbContext lM_DbContext)
        {
            _lM_DbContext = lM_DbContext;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var result = new List<User>();
            try
            {
                result = await _lM_DbContext.Users.ToListAsync();
            }
            catch (Exception ex)
            {

            }
            return result;

        }

        public async Task<User> CreateUser(User user) 
        {
            try
            {
                _lM_DbContext.Users.Add(user);
                _lM_DbContext.SaveChangesAsync();
                return user;
            }
            catch(Exception ex) 
            {
                return null;
            }
        }
    }
}
