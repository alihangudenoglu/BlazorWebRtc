using BlazorWebRtc.Application.Features.Commands.Account.Login;
using BlazorWebRtc.Application.Features.Commands.Account.Register;
using BlazorWebRtc.Application.Features.Commands.Feature;
using BlazorWebRtc.Application.Features.Commands.MessageCommand.SendMessage;
using BlazorWebRtc.Application.Features.Commands.RequestFeature;
using BlazorWebRtc.Application.Features.Queries.RequestFeature;
using BlazorWebRtc.Application.Features.Queries.UserFriend;
using BlazorWebRtc.Application.Features.Queries.UserInfo;
using BlazorWebRtc.Application.Interface.Services;
using BlazorWebRtc.Application.Models;
using BlazorWebRtc.Application.Services;
using BlazorWebRtc.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped(typeof(BaseResponseModel));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IUserFriendService, UserFriendService>();
builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.RegisterServicesFromAssembly(typeof(RegisterHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(LoginHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UserListHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(RequestHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(RequestsHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UserFriendHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UserFriendListQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(SendMessageHandler).Assembly);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSwaggerGen();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("SecretKey");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
        ValidAudience = jwtSettings.GetValue<string>("Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
            "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
            "Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
{
    {
       new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header
        },
        new List<string>()
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
