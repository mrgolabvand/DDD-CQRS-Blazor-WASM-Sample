using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _0_Framework.Application
{
    public class JwtAuthTools
    {
        public static string CreateToken(AuthViewModel account)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("AccountId", account.Id.ToString()),
                new Claim("UserName", account.UserName),
                new Claim("RoleId", account.RoleId.ToString()),
                new Claim(ClaimTypes.Role, account.Role),
            };

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Q4FvbkpFaCaNPyF25EXcx20lYRNjBvWg94u83IkDS68L"));
            //var key1 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ASEFRFDDWSDRGYHF"));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //var encryptingCreds = new EncryptingCredentials(key1, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
            //var handler = new JwtSecurityTokenHandler();
            //var t = handler.CreateJwtSecurityToken();
            //var token = handler.CreateJwtSecurityToken("https://localhost:5001/", "https://localhost:5001/"
            //    , new ClaimsIdentity(claims)
            //    , expires: DateTime.Now.AddMinutes(1)
            //    , signingCredentials: creds
            //    , encryptingCredentials: encryptingCreds
            //    , notBefore: DateTime.Now
            //    , issuedAt: DateTime.Now);
            //return new JwtSecurityTokenHandler().WriteToken(token);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Q4FvbkpFaCaNPyF25EXcx20lYRNjBvWg94u83IkDS68L"));

            var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityTokenHandler().CreateJwtSecurityToken(
                "https://localhost:5001",
                "https://localhost:5001",
                new ClaimsIdentity(claims),
                expires: DateTime.Now.AddDays(10),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
