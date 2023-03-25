namespace Hungry_Api.AuthModels
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string? AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
