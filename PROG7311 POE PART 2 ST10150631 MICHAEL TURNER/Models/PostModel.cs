namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models
{
    public class PostModel
    {
        public PostModel()
        {
            
        }
        public int ID { get; set; }
        public string Content { get; set; }
        public string Topic { get; set; }
        public DateTime PostDate { get; set; }
        public string Creator { get; set; }
        public byte[] PostImage { get; set; }
    }
}
