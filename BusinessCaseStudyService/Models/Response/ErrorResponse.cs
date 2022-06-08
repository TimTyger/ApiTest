using System;

namespace BusinessCaseStudyService.Models
{
    public class ErrorResponse
    {
        public bool Status { get; set; } = false;
        public string Message { get; set; } = "unknown error";
        public string ResponseCode { get; set; } = "400";
        public string ProcessId { get; set; } = Guid.NewGuid().ToString();

    }
}
