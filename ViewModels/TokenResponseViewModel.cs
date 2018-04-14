using Newtonsoft.Json;

namespace TestMakerFreeApi.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TokenResponseViewModel
    {
        public TokenResponseViewModel()
        {
        }
        
        public string token { get; set; }
        public int expiration { get; set; }
    }
}