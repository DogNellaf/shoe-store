namespace ShoeStore.Helpers
{
    public class Response
    {
        public ResponseType Type { get; set; }
        public string Text { get; set; }
        public int StatusCode { get; set; }
        public Response(string text, int statusCode = 200, ResponseType type = ResponseType.Info)
        {
            Text = text;
            StatusCode = statusCode;
            Type = type;
        }
    }
}
