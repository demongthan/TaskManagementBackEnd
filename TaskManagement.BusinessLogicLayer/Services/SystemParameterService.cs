using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Dynamic;
using TaskManagement.BusinessLogicLayer.Common.DataShaping.AstractClass;
using TaskManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using TaskManagement.BusinessLogicLayer.Common.Reponse;
using TaskManagement.BusinessLogicLayer.DataDomains.SystemParameter;
using TaskManagement.BusinessLogicLayer.Services.AstractClass;
using TaskManagement.DataAccessLayer.DataModels;
using TaskManagement.DataAccessLayer.Repository.AstractClass;
using TaskManagement.DataAccessLayer.UnitOfWork.AstractClass;

namespace TaskManagement.BusinessLogicLayer.Services
{
    public class SystemParameterService : ISystemParameterService
    {
        private readonly ISystemParameterRepository _systemParameterService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDataShaper<SystemParameterDto> _dataShaper;
        private readonly ILoggerManager _loggerManager;

        public SystemParameterService(
            ISystemParameterRepository systemParameterRepository,
            IUnitOfWork unitOfWork,
            IDataShaper<SystemParameterDto> dataShaper,
            IMapper mapper,
            ILoggerManager loggerManager)
        {
            _dataShaper = dataShaper;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggerManager = loggerManager;
            _systemParameterService = systemParameterRepository;
        }

        public async Task<ApiReponse<ExpandoObject>> CreateSystemParameter(SystemParameter systemParameterEntity, string fileds)
        {
            _systemParameterService.CreateSystemParameterAsyn(systemParameterEntity);

            if (await _unitOfWork.SaveChangesAsync())
            {
                var systemParameterReturn = _mapper.Map<SystemParameterDto>(systemParameterEntity);
                var result = _dataShaper.ShapeData(systemParameterReturn, fileds);

                _loggerManager.LogError(string.Format("System Parameter create successfully {0}", systemParameterEntity.Code));

                return new ApiReponse<ExpandoObject>(true, "successfully", StatusCodes.Status200OK, result);
            }
            else
            {
                _loggerManager.LogError(string.Format("System Parameter create is failed {0}", systemParameterEntity.Code));

                return new ApiReponse<ExpandoObject>(false, "Failed", StatusCodes.Status500InternalServerError);
            }
        }
    }
}
