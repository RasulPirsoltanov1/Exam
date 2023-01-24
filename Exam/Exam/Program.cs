using Exam.DataAccess.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(@"Server=B3-1\SQLEXPRESS;DataBase=Exam;Trusted_Connection=true;"));
builder.Services.AddControllersWithViews();
var app = builder.Build();
app.UseStaticFiles();




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);

app.Run();
