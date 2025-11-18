namespace ATOZAPIs.JWT
{
    public class clsJWT
    {

        public string? Issuer { get; set; } = null;

        public string? Audience { get; set; } = null;

        public int? DurationInMinutes { get; set; } = null;

        public string? Key { get; set; } = null;

    }
}
