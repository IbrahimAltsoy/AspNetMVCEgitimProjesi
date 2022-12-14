using AspNetCoreMVCProjesi.Models;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IValidator<Kullanici>, KullaniciValidator>(); // FluentValidation kullanarak validasyon yapacağımızı uygulamaya bildiriyoruz

builder.Services.AddSession(option => option.IdleTimeout = TimeSpan.FromMinutes(3)); // servis olarak session ı ekledik

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); // Uygulamada https güvenli bağlantı yönlendirmesi kullan
app.UseStaticFiles(); // Uygulamada Statik dosyalar (css, js kütüphaneleri ve resim dosyaları) kullanılsın

app.UseSession(); // uygulamada session kullanmak istediğimizi belirttik

app.UseRouting(); // Uygulamada routing kullanılsın (/Home/Index)

app.UseAuthorization(); // Uygulamada oturum işleminden sonra yetkilendirme kullanılsın

app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Uygulamada kullanılacak olan routing yapısı

app.Run(); // Uygulamayı yukarıdaki ayarlara göre çalıştır.
