using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace diet_server_api.Helpers.Auth
{
    public class TokenGenerator
    {
        public static JwtSecurityToken GenerateToken(int userId, string role, string firstName, string lastName, SigningCredentials creds){
            

            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userId)),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, firstName),
                new Claim(ClaimTypes.Surname, lastName)
            };

            var token = new JwtSecurityToken(
                issuer: "diet-app-server",
                audience: "diet-app-client",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
                );

            return token;
        }
    }
}