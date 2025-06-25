using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Enums;
using Application.Setting;
using Microsoft.IdentityModel.Tokens;
using Application.Setting;
using Jose;

public  class TokenService 
{
    private readonly TokenOptions _jwt;

    public TokenService(TokenOptions jwt)
    {
        _jwt = jwt;
    }

    public string CreateToken(int userId, string fullName, UserRole role)
    {
        var TokenHandler = new JwtSecurityTokenHandler();

        var TokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwt.Issuer,
            Audience = _jwt.Audience,
            SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key))
            ,SecurityAlgorithms.HmacSha256),

            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, fullName),
                new Claim(ClaimTypes.Role, role.ToString())
            }),

        };

        var securityToken = TokenHandler.CreateToken(TokenDescriptor);

        var accessToken = TokenHandler.WriteToken(securityToken);

        return accessToken;
    }
}