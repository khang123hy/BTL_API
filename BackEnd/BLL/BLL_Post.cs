﻿using BLL.Interfaces;
using DAL.Interfaces;
using DTO;

namespace BLL
{
    public class BLL_Post : ITF_BLL_Post
    {
        private ITF_DAL_Post _DAL_Post;
        public BLL_Post(ITF_DAL_Post dAL_Post)
        {
            _DAL_Post = dAL_Post;
        }
        public Post getpost(int id)
        {
            return _DAL_Post.getpost(id);
        }
        public Post3 getpost_by_id_User(int id)
        {
            return _DAL_Post.getpost_by_id_User(id);
        }
        public bool Update_Post(Post model)
        {
            return _DAL_Post.Update_Post(model);
        }
        public bool Create_Post(Post model)
        {
            return _DAL_Post.Create_Post(model);
        }

        public Post Delete_Post(int id)
        {
            return _DAL_Post.Delete_Post(id);
        }

        public bool Deletes_Post(LIST_Post model)
        {
            return _DAL_Post.Deletes_Post(model);
        }

        public List<Post> Search_Posts(int pageIndex, int pageSize, out long total, string Keywords)
        {
            return _DAL_Post.Search_Posts(pageIndex, pageSize, out total, Keywords);
        }

        public List<Post2> Search_Posts_User(int pageIndex, int pageSize, out long total, string Keywords)
        {
            return _DAL_Post.Search_Posts_User(pageIndex, pageSize, out total, Keywords);
        }
        public List<Post2> Search_Posts_by_Topic_User(int pageIndex, int pageSize, out long total, string Keywords)
        {
            return _DAL_Post.Search_Posts_by_Topic_User(pageIndex, pageSize, out total, Keywords);
        }
    }
}