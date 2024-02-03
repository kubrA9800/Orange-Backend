using Microsoft.AspNetCore.Identity;
using Orange_Backend.Data;
using Orange_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Orange_Backend.Services.Interfaces;
using Orange_Backend.Services;
using Orange_Backend.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.
builder.Services.AddControllersWithViews();

// map emailconfig to emailsettings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailConfig"));



builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(option =>
{
    option.Password.RequireNonAlphanumeric = true; //simvol olab biler
    option.Password.RequireDigit = true; //reqem olmalidir
    option.Password.RequireLowercase = true; //balaca herf olmalidir
    option.Password.RequireUppercase = true; //boyuk olmalidir
    option.Password.RequiredLength = 6; //minimum 6 

    option.User.RequireUniqueEmail = true;

    option.SignIn.RequireConfirmedEmail = true;
    //Default lockout  settings

    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    option.Lockout.MaxFailedAccessAttempts = 5;
    option.Lockout.AllowedForNewUsers = true;

});



builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<ILayoutService, LayoutService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<IInfoService, InfoService>();
builder.Services.AddScoped<ITreatmentService, TreatmentService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IMagazineService, MagazineService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IBannerService, BannerService>();
builder.Services.AddScoped<IValuesService, ValuesService>();
builder.Services.AddScoped<IAchievmentService, AchievmentService>();
builder.Services.AddScoped<IResultService, ResultService>();
builder.Services.AddScoped<ISubscribeService, SubscribeService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();




var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/Error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.UseEndpoints(endpoints =>
{
    _ = app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
