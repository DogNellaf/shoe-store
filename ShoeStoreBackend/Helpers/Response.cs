namespace ShoeStoreBackend.Helpers
{
    public class Response
    {
        public string Text { get; set; }
        public int StatusCode { get; set; }
        public Response(string text, int statusCode = 200)
        {
            Text = text;
            StatusCode = statusCode;
        }
    }
}
