using System.ComponentModel.DataAnnotations;

namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models
{
    public class UserModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserModel()
        {

        }
        [Key]
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int Role { get; set; }
    }
}
