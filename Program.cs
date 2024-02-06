using Caisse.Data;
using Caisse.Models;
using Caisse.Repositories;
using Caisse.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepository<Categorie>, CategoryRepository>();
builder.Services.AddScoped<IRepository<Produit>, ProduitRepository>();
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StringConnexion")));
// Ajout middleware pour utiliser les sessions
builder.Services.AddSession();
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
// Utilisation du MiddleWare Session (/!\  l'ordre des middleware a un impact)
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Produit}/{action=Index}/{id?}");

app.Run();
