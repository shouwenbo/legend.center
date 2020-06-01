using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Legend.Api.Service
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetToken(string userName)
        {
            var sk = @"-----BEGIN PUBLIC KEY-----MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDYLmZKhs+uiW8xANDsBiZUkDsJ10C1D93kQqW9nXQ7qrFO8Q9BtBoVScDVbAyqk+LgIo5ThfvUCUN5POyeNggeDPLKfetxJxOBo3rqtyypze2epqqx55c5t+9mYB89rYcEc9gtGrBNpWp5jgA+43JnrivIkdYC/AkkqUKDajLDWQIDAQAB-----END PUBLIC KEY-----";
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sk));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["issuer"],
                audience: _configuration["audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds);
            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
        }

        public string TestGet()
        {
            return "abcdefg";
        }
    }
}
