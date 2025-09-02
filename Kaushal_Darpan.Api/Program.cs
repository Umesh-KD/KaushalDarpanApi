using FluentValidation;
using FluentValidation.AspNetCore;
using Kaushal_Darpan.Api.Email;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text;
using Hangfire;
using Kaushal_Darpan.Infra;
//using static Org.BouncyCastle.Math.EC.ECCurve;
//using Hangfire.MemoryStorage;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
  



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add di services
// configuration
//builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

// path
var RootPath = builder.Environment.ContentRootPath;
// configuration
ConfigurationHelper.Configure(builder.Configuration, RootPath);


builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddScoped<DBContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailService, EmailService>();
//builder.Services.AddTransient<RestrictUrlFactory>();// middleware
//builder.Services.AddTransient<LogUserActivityFilter>();// action filter

//builder.Services.AddScoped<IValidator<ProductDetailsCreateModel>, ProductDetailsCreateModelValidator>();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters().AddValidatorsFromAssemblyContaining<Program>(); // register validators

// remove system default api validation checking
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// select origin
//builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
//{
//    builder.WithOrigins("http://localhost:4200", "http://localhost:5230/").AllowAnyMethod().AllowAnyHeader();
//}));
// all origin
builder.Services.AddCors(x => x.AddPolicy("corepolicy", x1 => x1.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("X-AuthToken", "no-loader", "Content-Disposition")));

//security
string[] allowedMethods = ["GET", "POST", "PUT", "DELETE", "PATCH"];

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("corepolicy", policy =>
//    {
//        policy.WithOrigins("http://10.68.231.141", "http://103.203.138.236", "http://localhost:4200", "http://103.203.136.199:81/", "http://103.203.136.199/", "https://kdhte.rajasthan.gov.in/") // Allow only this domain
//              .WithMethods(allowedMethods) // Allow specific HTTP methods (e.g., "GET", "POST", "PUT", etc.)
//              .WithHeaders("Content-Type", "Accept", "Authorization", "no-loader") // Allow only these request headers
//              .WithExposedHeaders("X-AuthToken", "no-loader")// Expose specific response headers
//              .AllowCredentials(); //cookies, HTTP authentication information, or client-side SSL certificates
//    });
//});


//// session time
//var sessionTimeOut = TimeSpan.FromMinutes(ConfigurationHelper.SessionTime);
//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(x =>
//{
//    x.IdleTimeout = sessionTimeOut;
//    //x.Cookie.HttpOnly = true;
//    //x.Cookie.IsEssential = true; 
//});

//cookie
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This sets the minimum SameSite mode applied to all cookies
    options.MinimumSameSitePolicy = SameSiteMode.None;

    // This controls whether to apply the cookie policy globally
    options.OnAppendCookie = context =>
    {
        context.CookieOptions.SameSite = SameSiteMode.None;
        context.CookieOptions.Secure = true;
        context.CookieOptions.HttpOnly = false;
        context.CookieOptions.Expires = DateTimeOffset.UtcNow.AddDays(1);
    };
});


// jwt login input Swagger
builder.Services.AddSwaggerGen(c =>
{
    //c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        //Contact = new OpenApiContact
        //{
        //    Name = "Click here to LOGOUT",
        //    Url = new Uri(configuration["SiteKeys:User-Logout-url"])
        //}
    });
    //c.EnableAnnotations();
    //var filePath = Path.Combine(System.AppContext.BaseDirectory, "MyApi.xml");
    //c.IncludeXmlComments(filePath);

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
// end jwt login input Swagger

// auth with jwt and jwt token
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = ConfigurationHelper.JwtIssuer,
        ValidAudience = ConfigurationHelper.JwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationHelper.JwtSecret)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true
    };
});
builder.Services.AddAuthorization();
// end auth with jwt and jwt token

builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false; // Disable 'Server' header
});


//// action filter
//builder.Services.AddControllersWithViews(options =>
//{
//    options.Filters.Add<LogUserActivityFilter>();
//});


// Add Hangfire services
builder.Services.AddHangfire(config =>
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          //.UseMemoryStorage()); // Replace with .UseSqlServerStorage(...) in production
          .UseSqlServerStorage(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddHangfireServer();

// Register your service
builder.Services.AddScoped<IMyService, MyService>();


// ------------ pipeline ----------------------
var app = builder.Build();

// Set up static helper with accessor
var httpContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
CommonFuncationHelper.Configure(httpContextAccessor);

app.UseHangfireDashboard(); // optional dashboard
app.MapHangfireDashboard(); // dashboard URL: /hangfire

// Configure the HTTP request pipeline.
if (ConfigurationHelper.IsLocal == true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
}
else
{
    //security
    // Strict-Transport-Security (HSTS) header (forces HTTPS)
    app.UseHsts();
}


// Use CORS policy here only
app.UseCors("corepolicy");

//routing
app.UseRouting();
app.UseXfo(o => o.Deny());



//context 

app.Use(async (context, next) =>
{
    //security
    // Remove unwanted headers
    context.Response.Headers.Remove("Server");
    context.Response.Headers.Remove("X-AspNet-Version");
    context.Response.Headers.Remove("X-AspNetMvc-Version");

    // the X-Powered-By header is an HTTP response header that typically indicates which web framework or technology is being used to serve the request
    context.Response.Headers.Remove("X-Powered-By");

    //dummy set
    context.Response.Headers["Server"] = "noinfo";
    context.Response.Headers["X-AspNet-Version"] = "noinfo";
    context.Response.Headers["X-AspNetMvc-Version"] = "noinfo";
    context.Response.Headers["X-Powered-By"] = "noinfo";

    // Set 'X-Content-Type-Options' header to 'nosniff' to prevent MIME sniffing
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";

    // Set the 'X-Frame-Options' header to 'DENY' to prevent clickjacking
    context.Response.Headers["X-Frame-Options"] = "DENY"; // Or "SAMEORIGIN"
    app.UseXfo(o => o.Deny());

    // Content-Security-Policy (CSP) header (restricts sources of content)
    context.Response.Headers["Content-Security-Policy"] = "default-src 'self'; script-src 'self' https://trusted.cdn.com; object-src 'none'; frame-ancestors 'none';";

    //end security

    //handle allow methos
    if (!allowedMethods.Contains(context.Request.Method))
    {
        context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
        return;//short-circut
    }

    //next
    await next();
});
//Enable directory browsing     
//app.UseDirectoryBrowser(new DirectoryBrowserOptions
//{
//    FileProvider = new PhysicalFileProvider(
//                Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
//    RequestPath = "/StaticFiles"
//});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
    RequestPath = "/StaticFiles"
});

//app.UseMiddleware<RestrictUrlFactory>();

app.UseCookiePolicy();

//security
// Force HTTPS (in production, make sure you force HTTP to HTTPS redirection)
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.Run();

