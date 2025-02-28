namespace MarchApi.Settings;

public class TokenSettings
{
    public string Issuer { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public int ExpiredToken { get; set; } = 0;
}
