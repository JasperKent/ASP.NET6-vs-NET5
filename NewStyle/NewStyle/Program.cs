// Main
using DateTimeProblems.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure Services

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSqlServer<PeopleContext>(builder.Configuration.GetConnectionString("SQLServer"));

//builder.Services.AddDbContext<PeopleContext>(options =>
//  options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));

// Main
var app = builder.Build();

// Configure
// Configure the HTTP request pipeline.

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PeopleContext>();

    InitializeDb(context);
}


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
    pattern: "{controller=People}/{action=Index}/{id?}");

// Main
app.Run();

static void InitializeDb(PeopleContext context)
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    context.People.Add(new Person { Name = "Fred", DateOfBirth = new DateTime(1990, 3, 1) });
    context.People.Add(new Person { Name = "Wilma", DateOfBirth = new DateTime(1995, 4, 2) });
    context.People.Add(new Person { Name = "Betty", DateOfBirth = new DateTime(1997, 9, 10) });
    context.People.Add(new Person { Name = "Barney", DateOfBirth = new DateTime(1991, 6, 6) });

    context.SaveChanges();
}

//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.MapGet("/index", () => "My minimal website");

//app.Run();