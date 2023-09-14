using AppCore.DataAccess.EntityFramework.Bases;
using Business.Services;
using DataAccess.Contexts;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

#region Localization

List<CultureInfo> cultures = new List<CultureInfo>()
{
    new CultureInfo("en-US") // Turkce icin parametresini ("tr-TR") yapmak yeterlidir.
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name);
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});
#endregion


#region IoC Container
// Alternatif olarak Business katmanýnda Autofac ve Ninject gibi kütüphaneler de kullanýlabilir.

// Unable to resolve service hatalarý burada çözümlenmelidir.

// AddScoped: istek (request) boyunca objenin referansýný (genelde interface veya abstract class) kullandýðýmýz yerde obje (somut class'tan oluþturulacak)
// bir kere oluþturulur ve yanýt (response) dönene kadar bu obje hayatta kalýr.
// AddSingleton: web uygulamasý baþladýðýnda objenin referansný (genelde interface veya abstract class) kullandýðýmýz yerde obje (somut class'tan oluþturulacak)
// bir kere oluþturulur ve uygulama çalýþtýðý (IIS üzerinden uygulama durdurulmadýðý veya yeniden baþlatýlmadýðý) sürece bu obje hayatta kalýr.
// AddTransient: istek (request) baðýmsýz ihtiyaç olan objenin referansýný (genelde interface veya abstract class) kullandýðýmýz her yerde bu objeyi new'ler.
// Genelde AddScoped methodu kullanýlýr.

var connectionString = builder.Configuration.GetConnectionString("Db");
builder.Services.AddDbContext<Db>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(RepoBase<>), typeof(Repo<>));

builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
#endregion


builder.Services.AddControllersWithViews();


#region Authentication
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    // projeye Cookie authentication default'larýný kullanarak kimlik doðrulama ekliyoruz

    .AddCookie(config =>
    {
        config.LoginPath = "/Account/Users/Login";
        // eðer sisteme giriþ yapýlmadan bir iþlem yapýlmaya çalýþýlýrsa kullanýcýyý Account area -> Users controller -> Login action'ýna yönlendir

        config.AccessDeniedPath = "/Account/Users/AccessDenied";
        // eðer sisteme giriþ yapýldýktan sonra yetki dýþý bir iþlem yapýlmaya çalýþýlýrsa kullanýcýyý Account area -> Users controller -> AccessDenied
        // action'ýna yönlendir

        config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        // sisteme giriþ yapýldýktan sonra oluþan cookie 30 dakika boyunca kullanýlabilsin

        config.SlidingExpiration = true;
        // SlidingExpiration true yapýlarak kullanýcý sistemde her iþlem yaptýðýnda cookie kullaným süresi yukarýda belirtilen 30 dakika uzatýlýr,
        // eðer false atanýrsa kullanýcýnýn cookie ömrü yukarýda belirtilen 30 dakika sonra sona erer ve yeniden giriþ yapmak zorunda kalýr
    });
#endregion

#region Session
builder.Services.AddSession(config =>
{

config.IdleTimeout = TimeSpan.FromMinutes(30); //Default süre 20dk
config.IOTimeout = Timeout.InfiniteTimeSpan;

}); 
#endregion

var app = builder.Build();

#region Localization
app.UseRequestLocalization(new RequestLocalizationOptions()
{
    DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name),
    SupportedCultures = cultures,
    SupportedUICultures = cultures,
});
#endregion


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


app.UseAuthentication(); //Kullnıcı sorgulama

app.UseAuthorization(); //Yetkili olup olmadığını sorguluyor


#region Session

app.UseSession(); 

#endregion


#region Area
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
}); 
#endregion


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
