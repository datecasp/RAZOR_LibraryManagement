using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Infra.DataContext;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LM_DbContext _lM_DbContext;
        private readonly IMapper _mapper;

        public UserRepository(LM_DbContext lM_DbContext, IMapper mapper)
        {
            _lM_DbContext = lM_DbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            var result = new List<UserModel>();
            try
            {
                var usersList = await _lM_DbContext.Users.ToListAsync();
                result = _mapper.Map<List<UserModel>>(usersList);
            }
            catch (Exception ex)
            {

            }
            return result;

        }

        /**
         * Search for an user email 
         * 
         * params -> string email: The email to seek
         * 
         * returns the user if exists or null if not
         */
        public async Task<UserModel> GetUserByEmail(string email) 
        {
            var user = await _lM_DbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> CreateUser(UserModel userModel) 
        {
            var user = _mapper.Map<User>(userModel);
            try
            {
                _lM_DbContext.Users.Add(user);
                return _mapper.Map<UserModel>(user);
            }
            catch(Exception ex) 
            {
                return null;
            }
        }
    }
}
