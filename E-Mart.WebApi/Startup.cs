using E_Mart.Domain.Users;
using E_Mart.EFCore.Data;
using E_Mart.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace E_Mart.WebApi;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;
    public Startup(IConfiguration configuration,IWebHostEnvironment webHostEnvironment)
    {
        _configuration = configuration;
        _environment = webHostEnvironment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        //Add Db Connection
        services.AddDbContext<EMartDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

        //Add Automapper
        services.AddAutoMapper(typeof(Startup));

        //Add Dependency Injection
        services.AddScoped<RoleService>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddScoped<UserService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IUserRepository, UserRepository>();

        //services.AddControllers();
        services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            }
        );
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(WebApplication app,IWebHostEnvironment webHostEnvironment)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllerRoute(
            name:"default",
            pattern:"{controller=CustomerController}/{action=Index}"
            );

        app.Run();
    }
}
