using LuftBorn.Service.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RecruitmentApp.Data;
using RecruitmentApp.Service;
using RecruitmentApp.Shared.Middleware;

string myCors = "_public";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRecruitmentServices();
builder.Services.AddSharedCore(builder.Configuration);


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myCors, option =>
    {
        option.AllowAnyHeader();
        option.AllowAnyMethod();
        option.AllowAnyOrigin();

    });
});
builder.Services.Configure<IdentityServerSettings>(builder.Configuration.GetSection("IdentityServerSettings"));

builder.Services.AddAuthentication("Bearer").AddIdentityServerAuthentication("Bearer",
    options =>
    {
        options.Authority = "https://localhost:5443";
        options.ApiName = "weatherapi";
        options.ApiSecret = "ScopeSecret";


    });

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler("/Error");
app.UseHsts();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseHttpsRedirection();

app.UseCors(myCors);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
