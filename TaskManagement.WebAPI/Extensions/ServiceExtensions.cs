using Microsoft.EntityFrameworkCore;
using TaskManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using TaskManagement.BusinessLogicLayer.Common.LoggerService;
using TaskManagement.DataAccessLayer.ApplicationDbContext;
using TaskManagement.BusinessLogicLayer.Common.AutoMapper;
using TaskManagement.BusinessLogicLayer.ActionFilters;
using TaskManagement.DataAccessLayer.Repository.AstractClass;
using TaskManagement.DataAccessLayer.UnitOfWork.AstractClass;
using TaskManagement.DataAccessLayer.UnitOfWork;
using TaskManagement.BusinessLogicLayer.Services.AstractClass;
using TaskManagement.BusinessLogicLayer.Services;
using TaskManagement.DataAccessLayer.Repository;
using TaskManagement.BusinessLogicLayer.Common.DataShaping.AstractClass;
using TaskManagement.BusinessLogicLayer.Common.DataShaping;
using TaskManagement.BusinessLogicLayer.DataDomains.SystemParameter;
using TaskManagement.BusinessLogicLayer.DataDomains.Task;

namespace TaskManagement.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this WebApplicationBuilder builder)
            => builder.Services.AddDbContext<DataDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        public static void ConfigureLoggerService(this WebApplicationBuilder builder)
            => builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureDIAutoMapper(this WebApplicationBuilder builder)
            => builder.Services.AddAutoMapper(typeof(MappingProfile));

        public static void ConfigureAddNewtonsoftJson(this WebApplicationBuilder builder)
            => builder.Services.AddControllers().AddNewtonsoftJson();

        public static void ConfigureAddCors(this WebApplicationBuilder builder, string MyAllowSpecificOrigins)
            => builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins(builder.Configuration.GetValue("UrlFrondEnd", "")).AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });

        public static void ConfigureDIActionFilters(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ValidationFilterAttribute>();
            builder.Services.AddScoped<ValidateSystemParameterExistsAttribute>();
            builder.Services.AddScoped<ValidateTaskExistsAttribute>();
        }

        public static void ConfigureDIRepsitory(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISystemParameterRepository, SystemParameterRepository>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
        }

        public static void ConfigureDIDataShaper(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IDataShaper<SystemParameterDto>, DataShaper<SystemParameterDto>>();
            builder.Services.AddScoped<IDataShaper<TaskDto>, DataShaper<TaskDto>>();
        }

        public static void ConfigureDIService(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISystemParameterService, SystemParameterService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
        }
    }
}
