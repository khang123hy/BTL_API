namespace DTO
{
    public partial class Topic
    {


        public int ID_Topic { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }


    }
}
