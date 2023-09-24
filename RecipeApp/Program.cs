
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using RecipeAppBLL.Services.IService;
using RecipeAppBLL.Services;
using RecipeAppDAL.DataContext;
using RecipeAppDAL.Repositories.IRepositories;
using RecipeAppDAL.Repositories;
using RecipeAppBLL.Utilities.AutoMaper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Database Connection, DefaultConnection should be stored in secrets 
builder.Services.AddDbContext<RecipeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
//builder.Services.AddScoped<IGenericRepository, GenericRepository>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoriesRepository,CategoriesRepository>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddControllers();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = "https://localhost:7288",
               ValidAudience = "https://localhost:7288",
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Token:Key"))
           };
       });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure logging
builder.Logging.AddConsole();

//Auto Mapper
builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddTransient<ErrorHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Enable Cross-Origin Resource Sharing (CORS) to allow cross-origin requests.
app.UseCors(options =>
{
    // Allow any header to be sent in requests.
    options.AllowAnyHeader();

    // Allow any HTTP method (GET, POST, PUT, DELETE, etc.).
    options.AllowAnyMethod();

    // Allow requests from any origin (domain) to access this API.
    options.AllowAnyOrigin();
});

app.UseMiddleware<ErrorHandlingMiddleware>();
// Serve static files from the wwwroot directory
app.UseStaticFiles();


app.Run();