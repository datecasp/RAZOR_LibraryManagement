using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public AdminService(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<vmAdminUserList>> GetAdminsListService()
        {
            var adminsList = (await _userManager.GetUsersInRoleAsync("admin")).ToList();
            var vmAdminsList = new List<vmAdminUserList>();
            foreach(var admin in adminsList)
            {
                vmAdminsList.Add(new vmAdminUserList
                {
                    Id = admin.Id,
                    UserName = admin.UserName,
                    Email = admin.Email,
                    Role = _userManager.GetRolesAsync(admin).Result.Max()
                });
            }
            return vmAdminsList;
        }
    }
}
