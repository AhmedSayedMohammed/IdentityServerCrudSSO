namespace LuftBorn.Service.Implementation
{
    public class IdentityServerSettings
    {
        public string DiscovertURL { get; set; }
        public string ClientName { get; set; }
        public string ClientPassword { get; set; }
        public bool UseHTTPS { get; set; }
    }
}
