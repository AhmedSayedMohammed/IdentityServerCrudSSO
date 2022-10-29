using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Data;

var seed = args.Contains("/seed");
if (seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}
var builder = WebApplication.CreateBuilder(args);
string myCors = "_public";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myCors, option =>
    {
        option.AllowAnyHeader();
        option.AllowAnyMethod();
        option.AllowAnyOrigin();

    });
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ASPNetIdentityDbContext>();
var assemply = typeof(Program).Assembly.GetName().Name;
var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (seed)
{
    SeedData.EnsureSeedData(defaultConnectionString);
}
builder.Services.AddDbContext<ASPNetIdentityDbContext>(options =>
options.UseSqlServer(defaultConnectionString, opt => opt.MigrationsAssembly(assemply))
);

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(op =>
op.ConfigureDbContext = b => b.UseSqlServer(defaultConnectionString, opt => opt.MigrationsAssembly(assemply))

    ).AddOperationalStore(
    op =>
op.ConfigureDbContext = b => b.UseSqlServer(defaultConnectionString, opt => opt.MigrationsAssembly(assemply))

    ).AddDeveloperSigningCredential();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddMvc(options =>
options.EnableEndpointRouting = false
);


var app = builder.Build();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
    endpoints.MapDefaultControllerRoute()
);
app.UseMvc();
app.Run();
