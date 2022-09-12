namespace Account.Service;

    public interface IPasswordHash
    {
        string GenerateSalt(int nSalt);
        string HashPassword(string password, string salt, int nIterations, int nHash);
    }
