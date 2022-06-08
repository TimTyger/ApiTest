using System.ComponentModel.DataAnnotations;

namespace BusinessCaseStudyService.Models
{
    public class TransactionRequest
    {
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Account length must be between 10 characters")]
        public string DebitAccount { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Account length must be between 10 characters")]
        public string CreditAccount { get; set; }
        [Required]
        public string Amount { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Narration cannot be more than 50 characters")]
        public string Narration { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "currency codes must be 2 0r 3 characters")]
        public string CurrencyCode { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name cannot be more than 50 characters")]
        public string Depositor { get; set; }
        public string DepositorMobile { get; set; }
        public string DepositorAddress { get; set; }
        public string ProcessCode { get; set; }
        public string Charge { get; set; }
        public string Vat { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Benefiiciary Name cannot be more than 50 characters")]
        public string BeneficiaryName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Destination Bank cannot be more than 50 characters")]
        public string DestBank { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "currency codes must be 2 0r 3 characters")]
        public string CountryCode { get; set; }
    }
}
