using BLL.Interfaces;
using DAL.Interfaces;
using DTO;

namespace BLL
{
    public class BLL_Like : ITF_BLL_Like
    {
        public ITF_DAL_Like _DAL_Like;
        public BLL_Like(ITF_DAL_Like dAL_Like)
        {
            _DAL_Like = dAL_Like;
        }

        public Like GetlikebyID(int id)
        {
            return _DAL_Like.GetlikebyID(id);
        }
    }
}
