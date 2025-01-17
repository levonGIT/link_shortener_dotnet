namespace api.Dtos.User
{
    public class LoginResponseDto
    {
        public string Login { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public int ExiresIn { get; set; }
    }
}
