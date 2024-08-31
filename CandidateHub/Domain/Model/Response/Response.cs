namespace CandidateHub.Domain.Model.Response
{
    public class Response<T>
    {
        public T Body { get; set; }
        public string Message { get; set; }
        public Response(string message, T body)
        {
            Message = message;
            Body = body;
        }
    }
}
