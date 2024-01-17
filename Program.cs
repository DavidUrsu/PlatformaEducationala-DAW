using Microsoft.EntityFrameworkCore;
using PlatformaEducationala_DAW.Models;
using PlatformaEducationala_DAW.Services.CourseService;
using PlatformaEducationala_DAW.Repositories.CourseRepository;
using PlatformaEducationala_DAW.Services.UserService;
using PlatformaEducationala_DAW.Repositories.UserRepository;
using PlatformaEducationala_DAW.Services.BlogPostService;
using PlatformaEducationala_DAW.Repositories.BlogPostRepository;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
