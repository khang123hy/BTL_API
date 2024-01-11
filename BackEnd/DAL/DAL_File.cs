using DAL.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class DAL_File : ITF_DAL_File
    {

        private IConfiguration _configuration;
        public DAL_File(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreatePathFile(string RelativePathFileName)
        {
            try
            {
                // Lấy đường dẫn gốc của máy chủ web từ cấu hình
                string serverRootPathFolder = _configuration["AppSettings:WEB_SERVER_FULL_PATH"].ToString();

                // Tạo đường dẫn tuyệt đối dựa trên đường dẫn tương đối
                string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";

                // Lấy đường dẫn thư mục từ đường dẫn tệp tin
                string fullPathFolder = System.IO.Path.GetDirectoryName(fullPathFile);

                // Kiểm tra và tạo thư mục nếu nó không tồn tại
                if (!Directory.Exists(fullPathFolder))
                    Directory.CreateDirectory(fullPathFolder);

                // Trả về đường dẫn tuyệt đối đã tạo
                return fullPathFile;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và trả về thông báo lỗi nếu có lỗi
                return ex.Message;
            }
        }

    }
}
