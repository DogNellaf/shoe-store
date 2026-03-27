using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoeStore.Helpers
{
    public class JsonResponse: JsonResult
    {
        public JsonResponse(Dictionary<string, object> values, ResponseType type = ResponseType.Info) : base(new Response(values, (int)type, type))
        {
            StatusCode = (int)type;
        }

        public JsonResponse(Dictionary<string, object> values, ResponseType type = ResponseType.Info, int statusCode = 200) : base(new Response(values, statusCode, type))
        {
            StatusCode = statusCode;
        }

        public JsonResponse(string text, ResponseType type = ResponseType.Info): base(new Response(text, (int)type, type))
        {
            StatusCode = (int)type;
        }

        public JsonResponse(string text, ResponseType type = ResponseType.Info, int statusCode = 200) : base(new Response(text, statusCode, type))
        {
            StatusCode = statusCode;
        }
    }
}
