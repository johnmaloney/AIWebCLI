namespace WebCLI.Core.Contracts
{
    public interface IUserContext
    {
        string UserId { get; set; }
        string UserName { get; set; }
        // Add other user-related properties as needed
    }
}
