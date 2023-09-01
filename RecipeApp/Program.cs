using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeAppBLL.Services.IService;
using RecipeAppBLL.Services;
using RecipeAppDAL.DataContext;
using RecipeAppDAL.Repositories;
using RecipeAppDAL.Repositories.IRepositories;

var builder = WebApplication.CreateBuilder(args);

//Database Connection, DefaultConnection should be stored in secrets 
builder.Services.AddDbContext<RecipeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"
)));
// Add services to the container.
builder.Services.AddScoped<IRecipeFilteringRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
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

app.Run();
