namespace DTO
{
    public partial class Like
    {


        public int ID_Like { get; set; }
        public int? ID_Post { get; set; }
        public int? ID_User { get; set; }

        public Like(int iD_Like, int? iD_Post, int? iD_User)
        {
            ID_Like = iD_Like;
            ID_Post = iD_Post;
            ID_User = iD_User;
        }
    }
}
