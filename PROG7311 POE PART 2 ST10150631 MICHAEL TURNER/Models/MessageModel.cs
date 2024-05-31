namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public string Sender { get; set; }
        public MessageModel()
        {
            
        }
    }
}
