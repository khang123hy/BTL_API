namespace DTO
{
    public partial class Topic
    {


        public int ID_Topic { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Image { get; set; }

    }

    public partial class LIST_Topic
    {
        public List<CT_Topic> list_topic { get; set; }
    }

    public partial class CT_Topic
    {
        public int ID_Topic { get; set; }

    }
}
