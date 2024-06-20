using Microsoft.EntityFrameworkCore;
using TaskManagement.DataAccessLayer.ApplicationDbContext;

namespace TaskManagement.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this WebApplicationBuilder builder)
            => builder.Services.AddDbContext<DataDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    }
}
