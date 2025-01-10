using Entities.Concrete.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Security
{
    public class TokenHandler : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private Token _token;
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
            _token = Configuration.GetSection("Token").Get<Token>();
        }
        public Token CreateToken(AppUser user)
        {
            //_token = DateTime.Now.AddMinutes(_token.Expiration);
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            _token.Expiration = DateTime.Now.AddMinutes(Convert.ToInt16(Configuration["Token:Expiration"]));

            _token = new Token
            {
                Expiration = DateTime.Now.AddMinutes(Convert.ToInt16(Configuration["Token:Expiration"]))
            };
            JwtSecurityToken jwtSecurityToken = new(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: _token.Expiration,
                notBefore: DateTime.Now,
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.FullName),
                },
                signingCredentials: credentials
            );
            JwtSecurityTokenHandler tokenHandler = new();
            _token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);
            byte[] numbers = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(numbers);
            _token.RefreshToken = Convert.ToBase64String(numbers);

            return _token;  

        }
        //public static Token CreateToken(IConfiguration configuration)
        //{
        //    Token token = new();
        //    SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));

        //    SigningCredentials credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        //    token.Expiration = DateTime.Now.AddMinutes(Convert.ToInt16(configuration["Token:Expiration"]));

        //    JwtSecurityToken jwtSecurityToken = new(
        //        issuer: configuration["Token:Issuer"],
        //        audience: configuration["Token:Audience"],
        //        expires: token.Expiration,
        //        notBefore: DateTime.Now,
        //        signingCredentials: credentials
        //    );
        //    JwtSecurityTokenHandler tokenHandler = new();
        //    token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);
        //    byte[] numbers = new byte[32];
        //    using RandomNumberGenerator random =RandomNumberGenerator.Create(); 
        //    random.GetBytes(numbers);
        //    token.RefreshToken=Convert.ToBase64String(numbers);

        //    return token;
        //}

    }
}
