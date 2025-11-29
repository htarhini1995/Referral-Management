public sealed class UserLoginRequest
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string MobilePhoneNumber { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
}
