using Application;
using Application.Application.Helpers;
using Application.Contracts;
using Application.DataAccess.Helper;
using Application.DataAccess.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


string connection = builder.Configuration.GetConnectionString("application_api");
builder.Services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql(connection));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var basePath = AppContext.BaseDirectory;

    var xmlPath = Path.Combine(basePath, "AppAPI.xml");
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddTransient<IApplicationsRepository, ApplicationsRepository>();
builder.Services.AddTransient<IDbConnectionStringProvider, DbConnectionStringProvider>();
builder.Services.AddTransient<IMappings, Mappings>();
builder.Services.AddTransient<ActivityDto>();
builder.Services.AddTransient<Validator>();
builder.Services.AddTransient<IConvertorEnum, ConvertorEnum>();


using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<DatabaseContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
