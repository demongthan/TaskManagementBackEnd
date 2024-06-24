using TaskManagement.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.ConfigureDbContext();

builder.ConfigureLoggerService();

builder.ConfigureDIAutoMapper();

builder.ConfigureDIDataShaper();

builder.ConfigureDIRepsitory();

builder.ConfigureDIActionFilters();

builder.ConfigureDIService();

builder.ConfigureAddCors(MyAllowSpecificOrigins);

builder.ConfigureAddNewtonsoftJson();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
