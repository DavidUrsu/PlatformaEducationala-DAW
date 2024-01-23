using Microsoft.EntityFrameworkCore;
using PlatformaEducationalaAPI.Models;
using PlatformaEducationalaAPI.Repositories.BlogPostRepository;
using PlatformaEducationalaAPI.Repositories.CourseRepository;
using PlatformaEducationalaAPI.Repositories.UserRepository;
using PlatformaEducationalaAPI.Services.BlogPostService;
using PlatformaEducationalaAPI.Services.CourseService;
using PlatformaEducationalaAPI.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ICourseRepository, CourseRepository>();
builder.Services.AddTransient<ICourseService, CourseService>();

builder.Services.AddTransient<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddTransient<IBlogPostService, BlogPostService>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddDbContext<PlatformaDbContext>(options =>
{
	options.UseSqlServer("Server=(localdb)\\PlatformaEducationalaDB;Database=PlatformaEducationala;Trusted_Connection=True;TrustServerCertificate=True;");
});

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

app.UseAuthorization();

app.MapControllers();

app.Run();
