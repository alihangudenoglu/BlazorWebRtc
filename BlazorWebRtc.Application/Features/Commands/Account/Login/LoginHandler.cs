using BlazorWebRtc.Domain;
using BlazorWebRtc.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlazorWebRtc.Application.Features.Commands.Account.Login;

public class LoginHandler : IRequestHandler<LoginCommand, (bool, string)>
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public LoginHandler(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<(bool,string)> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u=>u.UserName == request.UserName,cancellationToken);

        if (user == null || VerifyPassword(request.Password,user.PasswordHash,user.PasswordSalt))
        {
            return (false,string.Empty);
        }

        var token = GenerateJwtToken(user);

        return (true, token);

    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings.GetValue<string>("Secretkey");
        var issuer = jwtSettings.GetValue<string>("Issuer");
        var audience = jwtSettings.GetValue<string>("Audience");
        var expirationMinutes = jwtSettings.GetValue<int>("ExpirationMinutes");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials=new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Name,user.UserName),
            new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
        };

        var token = new JwtSecurityToken(issuer, audience, claims,expires:DateTime.Now.AddMinutes(expirationMinutes),signingCredentials:credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private bool VerifyPassword(string password,string storedHash,string storedSalt)
    {

        byte[] salt=Convert.FromBase64String(storedHash);

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8
            ));

        return hashed==storedHash;

    }

}
