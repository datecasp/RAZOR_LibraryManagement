using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Infra.DataContext;
using RAZOR_LibraryManagement.Models.Entities;

namespace RAZOR_LibraryManagement.Infra.Repositories
{
    public class AppSettingsRepository : GenericRepository<AppSettingsEntity>, IAppSettingsRepository
    {
        private readonly LM_DbContext _lM_DbContext;

        public AppSettingsRepository(LM_DbContext lM_DbContext) : base(lM_DbContext)
        {
            _lM_DbContext = lM_DbContext;
        }

        public async Task<IEnumerable<AppSettingsEntity>> GetAllSettings()
        {
            var result = new List<AppSettingsEntity>();
            try
            {
                result = await _lM_DbContext.AppSettings.ToListAsync();
            }
            catch (Exception ex)
            {

            }
            return result;

        }
    }
}
