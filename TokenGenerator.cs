using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Selectors;

namespace Project12_JsonWebToken.JWT
{
    public class TokenGenerator
    {
        public string GenerateJwtToken(string username,string email, string name, string surname) // token generate metot
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("20Derste20ProjeToken+-*/1234tokenJWT"));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256Signature);
            var claimsExample = new[]   // istekte bulunacak olan kişi
            {
                new Claim(JwtRegisteredClaimNames.Sub,username), // sub dedigi kullanıcı adı için
                new Claim(JwtRegisteredClaimNames.GivenName,name),
                new Claim(JwtRegisteredClaimNames.FamilyName,surname),
                new Claim(JwtRegisteredClaimNames.Email,email), // bunları tokenın çözüldüğü payload kısmında görcez.
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claimsExample,
                expires: DateTime.Now.AddMinutes(5), // Token 5 dk gecerli olacak
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token); // GenerateToken cagrilinca geriye bu parametreler doncek.
        }

        public string GenerateJwtToken2(string username) // bu metotu login kısmında cagircaz
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("20Derste20ProjeToken+-*/1234tokenJWT"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var claimsExample = new[]   
            {
                new Claim(JwtRegisteredClaimNames.Sub,username),  
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claimsExample,
                expires: DateTime.Now.AddMinutes(5), // Token 5 dk gecerli olacak
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token); // GenerateToken cagrilinca geriye bu parametreler doncek.
        }
    }
}
