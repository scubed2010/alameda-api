namespace Alameda.Business.DTOs.Responses
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T ResponseObject { get; set; }
    }
}
