﻿using RAZOR_LibraryManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IAdminService
    {
        Task<List<vmAdminUserList>> GetAdminsListService();
        Task<vmNotification> CreateAdminService(vmAdminUserCreate admin);
    }
}
