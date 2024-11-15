using Microsoft.AspNetCore.Mvc;

namespace ShoeStore.Helpers
{
    public class JsonResponse: JsonResult
    {
        public JsonResponse(string text, ResponseType type = ResponseType.Info): base(new Response(text, (int)type, type)) { }

        public JsonResponse(string text, ResponseType type = ResponseType.Info, int statusCode = 200) : base(new Response(text, statusCode, type)) { }
    }
}
