using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class AppSettingsService : IAppSettingsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppSettingsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppSettingsModel>> GetAllSettings()
        {
            var appSettingsList = await _unitOfWork.AppSettingsRepository.GetAllSettings();
            return _mapper.Map<IEnumerable<AppSettingsModel>>(appSettingsList);
        }
    }
}
