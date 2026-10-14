using CargoTrack.Business;
using CargoTrack.Business.Services.Abouts;
using CargoTrack.Business.Services.Branches;
using CargoTrack.Business.Services.Cities;
using CargoTrack.DataAccess.Context;
using CargoTrack.DataAccess.Repositories.Abouts;
using CargoTrack.DataAccess.Repositories.Branches;
using CargoTrack.DataAccess.Repositories.Cities;
using CargoTrack.Entity.Entities;
using CargoTrack.Entity.Entities.Enums;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//IOC Container
//builder.Services.AddFluentValidationAutoValidation()
//    .AddFluentValidationClientsideAdapters()
//    .AddValidatorsFromAssemblyContaining<BusinessAssembly>();
builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssembly(typeof(BusinessAssembly).Assembly);

builder.Services.AddScoped<IAboutRepository, AboutRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();


builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<ICityService, CityService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseLazyLoadingProxies();

});
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<AppDbContext>();
builder.Services.ConfigureApplicationCookie(config=>
{
    config.LoginPath = "/Login/Index";
    config.LogoutPath = "/Login/Logout";
    config.AccessDeniedPath = "/ErrorPages/AccessDenied";
    config.Cookie.Name = "CargoTrackCookie";
});
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
    if (!dbContext.Cities.Any())
    {
        var cities=new List<City>
        {
            new City { Id = Guid.NewGuid(), Name = "İstanbul" },
            new City { Id = Guid.NewGuid(), Name = "Ankara" },
            new City { Id = Guid.NewGuid(), Name = "İzmir" },
            new City { Id = Guid.NewGuid(), Name = "Bursa" },
            new City { Id = Guid.NewGuid(), Name = "Antalya" },
            new City { Id = Guid.NewGuid(), Name = "Adana" },
            new City { Id = Guid.NewGuid(), Name = "Konya" },
            new City { Id = Guid.NewGuid(), Name = "Şanlıurfa" },
            new City { Id = Guid.NewGuid(), Name = "Gaziantep" },
            new City { Id = Guid.NewGuid(), Name = "Kocaeli" },
            new City { Id = Guid.NewGuid(), Name = "Mersin" },
            new City { Id = Guid.NewGuid(), Name = "Diyarbakır" },
            new City { Id = Guid.NewGuid(), Name = "Hatay" },
            new City { Id = Guid.NewGuid(), Name = "Kayseri" },
            new City { Id = Guid.NewGuid(), Name = "Samsun" },
            new City { Id = Guid.NewGuid(), Name = "Balıkesir" },
            new City { Id = Guid.NewGuid(), Name = "Kahramanmaraş" },
            new City { Id = Guid.NewGuid(), Name = "Van" },
            new City { Id = Guid.NewGuid(), Name = "Aydın" },
            new City { Id = Guid.NewGuid(), Name = "Tekirdağ" }
        };
        dbContext.Cities.AddRange(cities);
        dbContext.SaveChanges();
    }
    if (!dbContext.Roles.Any())
    {
        var roles = new List<AppRole>
        {
            new AppRole {Name = "Admin", NormalizedName = "ADMIN" },
            new AppRole {Name = "User", NormalizedName = "USER" },
            new AppRole {Name = "Manager", NormalizedName = "MANAGER" }
        };
        dbContext.Roles.AddRange(roles);
        dbContext.SaveChanges();
    }
    if(!dbContext.Cargos.Any())
    {
        var cargo = new Cargo
        {
                Id=Guid.NewGuid(),
                SenderId=Guid.Parse("3873b2dc-39be-463b-13ef-08df251252a6"),
                ReceiverId=Guid.Parse("2664d048-454b-4db6-fa14-08df2568babb"),
                OriginBranchId=Guid.Parse("223db65d-0af9-4853-81ce-48693f6f3375"),
                DestinationBranchId=Guid.Parse("bb6cb08a-b18e-4877-90b5-74ce3810f45a"),
                TrackCode = "CT202610101234",
                ShipmentDate = DateTime.Now,
                EstimatedArrivalDate = DateTime.Now.AddDays(3),
                Weight = 10.5,
                CargoType=CargoType.Standard,
                CargoStatus = CargoStatus.DispatchedFromTransferCenter
        };
        dbContext.Add(cargo);
        dbContext.SaveChanges();
    }
}
app.Run();
