namespace Flush_It_API.Utilities
{
    public interface IBCryptHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);

    }
}
