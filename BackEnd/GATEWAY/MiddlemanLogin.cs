using DAL.Helper.Interfaces;
using DTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

        // Phương thức Invoke được gọi khi có một yêu cầu HTTP được gửi đến ứng dụng
        public Task Invoke(HttpContext context)
        {
            // Thiết lập headers để cho phép truy cập từ mọi nguồn
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Expose-Headers", "*");

            // Nếu đường dẫn không phải "/api/token", chuyển giao yêu cầu cho middleware tiếp theo
            if (!context.Request.Path.Equals("/api/token", StringComparison.Ordinal))
            {
                return _next(context);
            }

            // Nếu đường dẫn là "/api/token" và phương thức là POST, gọi phương thức GenerateToken để xử lý đăng nhập và tạo token
            if (context.Request.Method.Equals("POST") && context.Request.HasFormContentType)
            {
                return GenerateToken(context);
            }

            // Nếu điều kiện trên không được đáp ứng, trả về lỗi và mã trạng thái HTTP 400
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

                // Nếu thông tin đăng nhập không hợp lệ, trả về lỗi và mã trạng thái HTTP 400
                if (query == null || query.Rows.Count == 0)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var result = JsonConvert.SerializeObject(new { code = (int)HttpStatusCode.BadRequest, error = "Tài khoản hoặc mật khẩu không đúng" });
                    await context.Response.WriteAsync(result);
                    return;
                }

                // Nếu thông tin đăng nhập hợp lệ, tạo token JWT và trả về thông tin người dùng và token
                var fullName = query.Rows[0]["FullName"].ToString();
                var AccountName = query.Rows[0]["AccountName"].ToString();
                var Role = query.Rows[0]["Role"].ToString();
                var Password = query.Rows[0]["Password"].ToString();
                var ID_User = query.Rows[0]["ID_User"].ToString();
                var Avatar = query.Rows[0]["Avatar"].ToString();

                // tạo và xác thực JWT
                var tokenHandler = new JwtSecurityTokenHandler();

                //chuyển Secret thành mảng byte
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                //mô tả cách JWT được tạo
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, fullName),
                        new Claim(ClaimTypes.Name, AccountName),
                        new Claim(ClaimTypes.Role, Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    //sử dụng thuật toán HmacSha256Signature và khóa bí tạo từ chuỗi Secret
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var tmp = tokenHandler.CreateToken(tokenDescriptor);
                //chuyển đổi tmp sang kiểu chuỗi
                var token = tokenHandler.WriteToken(tmp);

                // Tạo một đối tượng chứa thông tin phản hồi và JWT
                var response = new { ID_User = ID_User, FullName = fullName, AccountName = AccountName, Role = Role, Avatar = Avatar, Token = token };

                // Cấu hình serializer settings để định dạng dữ liệu JSON
                var serializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };

                // Thiết lập loại nội dung của phản hồi là JSON
                context.Response.ContentType = "application/json";

                // Chuyển đối tượng response thành chuỗi JSON và gửi về client
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response, serializerSettings));

                // Kết thúc xử lý yêu cầu và trả về phản hồi
                return;


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
