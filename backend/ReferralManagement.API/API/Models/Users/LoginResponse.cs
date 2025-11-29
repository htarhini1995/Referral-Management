public sealed class LoginResponse
{
    public string Token { get; set; } = "";
    public DateTime ExpiresAtUtc { get; set; }
    public long UserId { get; set; }
    public String? UserName { get; set; }
}   