namespace BusinessCaseStudyService.Models
{
    public class ResponseObject<T>
    {
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
