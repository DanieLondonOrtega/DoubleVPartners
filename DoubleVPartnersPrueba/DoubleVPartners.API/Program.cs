using DoubleVPartners.Aplication.Interfaces;
using DoubleVPartners.Aplication.Services;
using DoubleVPartners.Common.Crypto;
using DoubleVPartners.Infrastructure.DataAccess.Context;
using DoubleVPartners.Infrastructure.DataAccess.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using DoubleVPartners.Aplication.Dtos;
using FluentValidation.AspNetCore;
using FluentValidation;
using DoubleVPartners.API.Models.Validators.User;
using DoubleVPartners.API.Models.Validators.Task;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<JwtDto>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Double V Partners Prueba Tecnica",
        Description = "API to obtain information and assign task",
        Contact = new OpenApiContact
        {
            Name = "Daniel Stiven Londoño Ortega",
            Email = "daniel.londono.ortega@gmail.com"
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string [] { }
        }
    });
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddDbContext<EntityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DoubleVPartnersBDConnection"))
);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ICrypto, Crypto>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<UserModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserUpdateModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskUpdateModelValidator>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder
            .WithOrigins("http://localhost:3000")  // Allow the React app
            .AllowAnyMethod()  // Allow all HTTP methods (GET, POST, etc.)
            .AllowAnyHeader()  // Allow all headers (like Content-Type, Authorization)
            .AllowCredentials(); // Allow credentials if necessary (e.g., cookies or auth tokens)
    });
});

builder.Services.AddControllers();
var app = builder.Build();

app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
