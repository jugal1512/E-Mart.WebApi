using E_Mart.Domain.Carts;
using E_Mart.Domain.Categories;
using E_Mart.Domain.Products;
using E_Mart.Domain.Users;
using E_Mart.Domain.Wishlists;
using E_Mart.EFCore.Data;
using E_Mart.EFCore.Repositories;
using E_Mart.Utility.FirebaseImageUpload;
using E_Mart.Utility.Shared;
using E_Mart.WebApi.Settings;
using E_Mart.WebApi.Utilities.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;

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

        //App Settings
        services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));
        //Mail Settings
        services.Configure<MailSettings>(_configuration.GetSection("MailSettings"));
        //JWT Settings
        services.Configure<JWTSettings>(_configuration.GetSection("JWT"));
        //FileUpload Settings
        services.Configure<FileUploadSettings>(_configuration.GetSection("FileUploadSettings"));

        //Image Upload Service
        services.AddTransient<IFirebaseImageUploadService, FirebaseImageUploadService>();

        //firebase
        string bucketName = "practice-bdcd1.appspot.com"; // Replace with your actual bucket name
        string firebaseStorageUrl = $"https://firebasestorage.googleapis.com/v0/b/{bucketName}/o/";

        services.AddSingleton(new FirebaseStorageService(bucketName, firebaseStorageUrl));

        //Add Dependency Injection
        services.AddScoped<RoleService>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddScoped<UserService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddScoped<CategoryService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddScoped<SubCategoriesService>();
        services.AddTransient<ISubCategoriesService, SubCategoriesService>();
        services.AddTransient<ISubCategoriesRepository, SubCategoriesRepository>();
        services.AddScoped<ProductService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddScoped<CartService>();
        services.AddTransient<ICartService, CartService>();
        services.AddTransient<ICartRepository, CartRepository>();
        services.AddScoped<CartItemService>();
        services.AddTransient<ICartItemService, CartItemService>();
        services.AddTransient<ICartItemRepository, CartItemRepository>();
        services.AddScoped<WishlistService>();
        services.AddTransient<IWishlistService, WishlistService>();
        services.AddTransient<IWishlistRepository, WishlistRepository>();
        services.AddTransient<IEmailService, EmailService>();

        //services.AddControllers();
        services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            }
        );

        // Adding Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })

        // Adding Jwt Bearer
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = _configuration["JWT:ValidAudience"],
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]))
            };
        });

        // add cors service
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOrigin", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });

        services.AddEndpointsApiExplorer();
        //services.AddSwaggerGen();
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
        });
    }

    public void Configure(WebApplication app,IWebHostEnvironment webHostEnvironment)
    {
        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI();
        //}
            
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors("AllowAnyOrigin");

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllerRoute(
            name:"default",
            pattern:"{controller=CustomerController}/{action=Index}"
            );

        app.Run();
    }
}
