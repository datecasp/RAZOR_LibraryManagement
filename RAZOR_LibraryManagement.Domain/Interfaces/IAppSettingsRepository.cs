using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAZOR_LibraryManagement.Models.Entities;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IAppSettingsRepository
    {
        Task<IEnumerable<AppSettingsEntity>> GetAllSettings();
    }
}
