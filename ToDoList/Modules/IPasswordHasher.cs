namespace ToDoList.Modules
{
    /// <summary>
    /// Module to create a hash from a password and validate
    /// a given password with the given hash.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Create a hash string from the given password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        string HashFromPassword(string password);

        /// <summary>
        /// Valdiate the given password if it matches the
        /// given hash.
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool CompareHashAndPassword(string hash, string password);
    }
}
