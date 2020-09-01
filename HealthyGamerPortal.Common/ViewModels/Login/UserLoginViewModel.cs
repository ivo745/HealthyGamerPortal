namespace HealthyGamerPortal.Common.ViewModels.Login
{
    public class BasicLoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class EncryptedBasicLoginModel
    {
        public EncryptedMessage Email { get; set; }

        public EncryptedMessage Password { get; set; }
    }

    public class EncryptedMessage
    {
        public string Text { get; set; }

        public int Length { get; set; }
    }

    public class BasicAuthenticationResult
    {
        public string Name { get; set; }
        public string[] Roles { get; set; }
    }
}