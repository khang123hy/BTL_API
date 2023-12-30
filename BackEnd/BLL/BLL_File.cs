using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL
{
    public class BLL_File : ITF_BLL_File
    {
        ITF_DAL_File _iTF_DAL_File;
        public BLL_File(ITF_DAL_File iTF_DAL_File)
        {
            _iTF_DAL_File = iTF_DAL_File;
        }

        public string CreatePathFile(string RelativePathFileName)
        {
            return _iTF_DAL_File.CreatePathFile(RelativePathFileName);
        }
    }
}
