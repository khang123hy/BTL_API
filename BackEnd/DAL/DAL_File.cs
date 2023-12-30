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
                string serverRootPathFolder = _configuration["AppSettings:WEB_SERVER_FULL_PATH"].ToString();
                string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";
                string fullPathFolder = System.IO.Path.GetDirectoryName(fullPathFile);
                if (!Directory.Exists(fullPathFolder))
                    Directory.CreateDirectory(fullPathFolder);
                return fullPathFile;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
