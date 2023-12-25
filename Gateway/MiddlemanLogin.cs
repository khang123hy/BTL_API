using DAL.Helper.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Gateway
{
    public class MiddlemanLogin
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly IDatabaseHelper _databaseHelper;
        private readonly IConfiguration _configuration;

        public MiddlemanLogin(RequestDelegate next, IOptions<AppSettings> appSettings, IDatabaseHelper databaseHelper, IConfiguration configuration)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _databaseHelper = databaseHelper;
            _configuration = configuration;
        }


        public Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Expose-Headers", "*");
            if (!context.Request.Path.Equals("/api/token", StringComparison.Ordinal))
            {
                return _next(context);
            }
            if (context.Request.Method.Equals("POST") && context.Request.HasFormContentType)
            {
                return GenerateToken(context);
            }
            context.Response.StatusCode = 400;
            return context.Response.WriteAsync("Bad request.");
        }

        public async Task GenerateToken(HttpContext context)
        {
            string msgError = "";
            try
            {
                var Taikhoan = context.Request.Form["Taikhoan"].ToString();
                var Matkhau = context.Request.Form["Matkhau"].ToString();


                var query = _databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "CheckLogin", "@AccountName", Taikhoan, "@Password", Matkhau);

                if (query == null || query.Rows.Count == 0)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var result = JsonConvert.SerializeObject(new { code = (int)HttpStatusCode.BadRequest, error = "Tài khoản hoặc mật khẩu không đúng" });
                    await context.Response.WriteAsync(result);
                    return;
                }

                // Get the values from the DataTable
                var fullName = query.Rows[0]["FullName"].ToString();
                var AccountName = query.Rows[0]["AccountName"].ToString();
                var Role = query.Rows[0]["Role"].ToString();
                var Password = query.Rows[0]["Password"].ToString();
                var ID_User = query.Rows[0]["ID_User"].ToString();

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, fullName),
                        new Claim(ClaimTypes.Name, AccountName),
                        new Claim(ClaimTypes.Role, Role),
                        new Claim(ClaimTypes.DenyOnlyWindowsDeviceGroup, Password)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var tmp = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(tmp);
                var response = new { ID_User = ID_User, FullName = fullName, AccountName = AccountName, Password = Password, Role = Role, Token = token };
                var serializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response, serializerSettings));
                return;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
