namespace DTO
{
    public partial class Comment
    {


        public int ID_Comment { get; set; }
        public int? ID_Post { get; set; }
        public int? ID_User { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedDate { get; set; }

        public Comment()
        {

        }
        public Comment(int iD_Comment, int? iD_Post, int? iD_User, string? content, DateTime? createdDate)
        {
            ID_Comment = iD_Comment;
            ID_Post = iD_Post;
            ID_User = iD_User;
            Content = content;
            CreatedDate = createdDate;
        }
    }


}
