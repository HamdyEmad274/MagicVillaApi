using static MagicVillaUtility.SD;

namespace MagicVillaWeb.Models
{
    public class APIRequest
    {
        public ApiType APIType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
