using System.ComponentModel.DataAnnotations;

namespace BusinessCaseStudyService.Models
{
    public class TxnModel
    {

        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Account length must be between 10 characters")]
        public string DebitAccount { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Account length must be between 10 characters")]
        public string CreditAccount { get; set; }
        [Required]
        public string RefAmount { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Narration cannot be more than 50 characters")]
        public string Narration { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Narration cannot be more than 50 characters")]
        public string TranId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Narration cannot be more than 50 characters")]
        public string PostAmount { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Narration cannot be more than 50 characters")]
        public string RefNo { get; set; }
        //[Required]
        //[StringLength(50, MinimumLength = 1, ErrorMessage = "Narration cannot be more than 50 characters")]
        public string Reason { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Narration cannot be more than 50 characters")]
        public string CreditBranch { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Narration cannot be more than 50 characters")]
        public string DebitBranch { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "currency codes must be 2 0r 3 characters")]
        public string CurrencyCode { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "currency codes must be 2 0r 3 characters")]
        public string CountryCode { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "currency codes must be 2 0r 3 characters")]
        public string ResponseCode { get; set; }
        public string RateFrom { get; set; }
        public string RateTo { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "tnx type must be 2 0r 3 characters")]
        public string TxnType { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name cannot be more than 50 characters")]
        public string Depositor { get; set; }
        public string DepositorMobile { get; set; }
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Address cannot be more than 50 characters")]
        public string DepositorAddress { get; set; }
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Address cannot be more than 50 characters")]
        public string ResponseMessage { get; set; }
        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Process code can only be 00 or 01")]
        public string ProcessCode { get; set; }
        public string TxnDate { get; set; }
        public string PostDate { get; set; }
        public string Charge { get; set; }
        public string Vat { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name cannot be more than 50 characters")]
        public string BeneficiaryName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Bank name cannot be more than 50 characters")]
        public string DestBank { get; set; }
        public string Status { get; set; }
        public string RevStatus { get; set; }
    }
}
