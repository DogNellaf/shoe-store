namespace ShoeStore.Helpers
{
    public class Response
    {
        public ResponseType Type { get; set; }
        public Dictionary<string, object> Values { get; set; } = null!;
        public int StatusCode { get; set; } 

        public Response()
        {

        }

        public Response(string text, int statusCode = 200, ResponseType type = ResponseType.Info)
        {
            Values = new Dictionary<string, object>() { { "result", text } };
            StatusCode = statusCode;
            Type = type;
        }

        public Response(Dictionary<string, object> values, int statusCode = 200, ResponseType type = ResponseType.Info)
        {
            Values = values;
            StatusCode = statusCode;
            Type = type;
        }
    }
}
