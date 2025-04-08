using System.ComponentModel.DataAnnotations;

namespace Portal.Web.Areas.Dashboard.Models
{
    public class UpdateStudentModel
    {
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
    }
}
