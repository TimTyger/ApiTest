using System.ComponentModel.DataAnnotations;

namespace BusinessCaseStudyService.Models
{
    public class StatusReq
    {
        [Required]
        [StringLength(20, MinimumLength = 20, ErrorMessage = "TransactionRef must be 20 characters")]
        public string TransactionRefId { get; set; }
    }
}
