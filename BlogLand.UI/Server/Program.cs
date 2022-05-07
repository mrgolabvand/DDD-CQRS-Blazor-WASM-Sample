using _0_Framework;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogLand.UI.Server;
using BlogManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserManagement.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BlogLand");

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();

builder.Services.AddTransient<IAuthHelper, AuthHelper>();

builder.Services.AddTransient<IFileUploader, FileUploader>();

UserManagementBootstrapper.Configure(builder.Services, connectionString);
BlogManagementBootstrapper.Configure(builder.Services, connectionString);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
    options.Secure = CookieSecurePolicy.Always;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/Account");
                    o.LogoutPath = new PathString("/Account");
                    o.AccessDeniedPath = new PathString("/AccessDenied");
                });


#region Jwt Auth

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = "https://localhost:5001/",
//        ValidAudience = "https://localhost:5001/",
//        TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ASEFRFDDWSDRGYHF")),
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Q4FvbkpFaCaNPyF25EXcx20lYRNjBvWg94u83IkDS68L")),
//        ClockSkew = TimeSpan.Zero
//    };
//});

#endregion


builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddMvcOptions(options => {
    options.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(Stream)));
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();


app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();