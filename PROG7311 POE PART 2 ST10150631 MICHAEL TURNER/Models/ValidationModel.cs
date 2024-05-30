namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models
{
    public class ValidationModel
    {
        public ValidationModel()
        {
            
        }

        public bool checkIsBlank(string value)
        {
            if (value == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
