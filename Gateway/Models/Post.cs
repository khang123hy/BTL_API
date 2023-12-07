namespace Models
{
    public partial class Post
    {


        public int ID_Post { get; set; }
        public int? ID_User { get; set; }
        public int? ID_Topic { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public byte[]? Image { get; set; }
        public DateTime? CreatedDate { get; set; }

        public Post()
        {

        }
        public Post(int iD_Post, int? iD_User, int? iD_Topic, string title, string content, byte[]? image, DateTime? createdDate)
        {
            ID_Post = iD_Post;
            ID_User = iD_User;
            ID_Topic = iD_Topic;
            Title = title;
            Content = content;
            Image = image;
            CreatedDate = createdDate;
        }
    }
}
