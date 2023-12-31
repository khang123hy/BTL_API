namespace DTO
{
    public partial class Comment
    {


        public int ID_Comment { get; set; }
        public int? ID_Post { get; set; }
        public int? ID_User { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedDate { get; set; }


    }
    public partial class CommentAndNotfication
    {


        public int ID_Comment { get; set; }
        public int? ID_Post { get; set; }
        public int? ID_User { get; set; }
        public int? ID_User_Nhan { get; set; }
        public string? Content { get; set; }
        public string? Title { get; set; }
        public string? Link { get; set; }
        public DateTime? CreatedDate { get; set; }


    }
    public partial class Comment2
    {


        public int ID_Comment { get; set; }
        public int? ID_Post { get; set; }
        public int? ID_User { get; set; }
        public string? Content { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public DateTime? CreatedDate { get; set; }


    }
    public partial class LIST_Comment
    {
        public List<CT_Comment> list_comment { get; set; }
    }

    public partial class CT_Comment
    {
        public int ID_Comment { get; set; }

    }
}
