﻿using DTO;

namespace DAL.Interfaces
{
    public partial interface ITF_DAL_Post
    {
        Post getpost(int id);
        Post3 getpost_by_id_User(int id);

        bool Create_post_list(Post_list model);
        bool Update_post_list(Post_list model);


        Post Delete_Post(int id);

        bool Deletes_Post(LIST_Post model);

        List<Post> Search_Posts(int pageIndex, int pageSize, out long total, string Keywords);
        List<Post_list> Search_Posts_Admin(int pageIndex, int pageSize, out long total, string Keywords);

        List<list_json_post> Search_PostDetails_User(out long total, string Keywords);
        List<Post2> Search_Posts_User_Asc(int pageIndex, int pageSize, out long total, string Keywords, string OrderBy);
        List<Post2> Search_Posts_User_Desc(int pageIndex, int pageSize, out long total, string Keywords, string OrderBy);

        List<Post2> Search_Posts_by_Topic_User_Asc(int pageIndex, int pageSize, out long total, string Keywords, string OrderBy, string ID_Topic);
        List<Post2> Search_Posts_by_Topic_User_Desc(int pageIndex, int pageSize, out long total, string Keywords, string OrderBy, string ID_Topic);


    }
}
