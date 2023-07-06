using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Infra.DataContext;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Infra.Repositories
{
    public class AppSettingsRepository : IAppSettingsRepository
    {
        private readonly LM_DbContext _lM_DbContext;
        private readonly IMapper _mapper;

        public AppSettingsRepository(LM_DbContext lM_DbContext, IMapper mapper)
        {
            _lM_DbContext = lM_DbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppSettingsModel>> GetAllSettings()
        {
            var result = new List<AppSettingsModel>();
            try
            {
                var entitiesList = await _lM_DbContext.AppSettings.ToListAsync();
                result = _mapper.Map<List<AppSettingsModel>>(entitiesList);
            }
            catch (Exception ex)
            {

            }
            return result;

        }

        public async Task<AppSettingsModel> UpdateSetting(AppSettingsModel setting)
        {
            var result = new AppSettingsModel();
            try
            {
                var settingEntity = _mapper.Map<AppSettingsEntity>(setting);
                result = _mapper.Map<AppSettingsModel>(
                    _lM_DbContext.Update(settingEntity).Entity
                    );
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
