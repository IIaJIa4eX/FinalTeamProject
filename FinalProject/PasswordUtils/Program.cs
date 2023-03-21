using FinalProject.Utils;

namespace PasswordUtils
{
    public class Program
    {
        static void Main(string[] args)
        {
            var result = PasswordUtils.CreatePasswordHash("12345");
            Console.WriteLine(result.passwordSalt);
            Console.WriteLine(result.passwordHash);
            Console.ReadKey(true);
        }
    }
}