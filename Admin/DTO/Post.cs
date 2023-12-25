namespace DTO
{
    public partial class Post
    {

        public int ID_Post { get; set; }
        public int? ID_User { get; set; }
        public int? ID_Topic { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Synopsis { get; set; }
        public string Image { get; set; }
        public DateTime? CreatedDate { get; set; }

        public partial class LIST_Post
        {
            public List<CT_Post> list_post { get; set; }
        }

        public partial class CT_Post
        {
            public int ID_Post { get; set; }

        }
    }
}
