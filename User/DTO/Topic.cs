namespace DTO
{
    public partial class Topic
    {


        public int ID_Topic { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }

        public Topic(int iD_Topic, string title, string? description, DateTime? createdDate)
        {
            ID_Topic = iD_Topic;
            Title = title;
            Description = description;
            CreatedDate = createdDate;
        }

    }
}
