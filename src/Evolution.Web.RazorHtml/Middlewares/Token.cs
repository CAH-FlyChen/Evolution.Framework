namespace JWT.Common
{
    public class Token
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string token_type { get; set; }
        public string user_name { get; set; }
        public string user_code { get; set; }
        public string role_name { get; set; }
    }
}
