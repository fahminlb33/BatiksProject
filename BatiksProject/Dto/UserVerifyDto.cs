namespace BatiksProject.Dto
{
    public class UserVerifyDto
    {
        public string Username { get; }

        public UserVerifyDto(string username)
        {
            Username = username;
        }
    }
}
