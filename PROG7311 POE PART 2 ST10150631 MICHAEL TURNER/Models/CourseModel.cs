namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models
{
    public class CourseModel
    {
        public int CourseID { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public byte[] CourseImage { get; set; }
        public byte[] CourseVideo { get; set; }

        public CourseModel()
        {
            
        }
    }
}
