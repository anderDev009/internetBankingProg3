using InternetBanking.Infrastructure.Persistence;
using InternetBanking.Infrastructure.Identity;
using InternetBanking.Infrastructure.Shared;
using InternetBanking.Infrastructure.Identity.Entities;
using InternetBanking.Infrastructure.Identity.Seeds;
using Microsoft.AspNetCore.Identity;
using InternetBanking.Core.Application;
using WebApp.MiddledWares;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews();
//Application
builder.Services.AddApplicationLayer();
//persistense
builder.Services.AddInfrastructureLayer(builder.Configuration);
//Identity
builder.Services.AddIdentityInfrastructure(builder.Configuration);
//Shaared
builder.Services.AddSharedInfrastructure(builder.Configuration);

//Añadir sesiones
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//dependencias de filtro
builder.Services.AddTransient<ValidateUser, ValidateUser>();
builder.Services.AddScoped<LoginAuthorize>();
var app = builder.Build();

//Metodo para correr los Seeds
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        //dependency injection 
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        //Metodos de los servicios para crear roles y default users con roles
        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultAdministratorUser.SeedAsync(userManager, roleManager);
        await DefaultClientUser.SeedAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
    }
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//usa sesiones
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
