using Domain.Ports.Incoming;
using System.Security.Cryptography;
using System.Text;

namespace Application
{
    public class CryptographyGenerator : ICryptographyGenerator
    {
        private static readonly char[] CharacterMatrixForRandomIVStringGeneration =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '_'
        };

        public string GenerateHashSha256(string text, int length)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hash = SHA256.HashData(bytes);

            var hashString = new StringBuilder(hash.Length * 2);
            foreach (var b in hash)
            {
                hashString.Append($"{b:x2}");
            }

            return length > hashString.Length
                ? hashString.ToString()
                : hashString.ToString().Substring(0, length);
        }
        public string GenerateRandomIV(int length)
        {
            char[] iv = new char[length];
            Span<byte> randomBytes = stackalloc byte[length];

            RandomNumberGenerator.Fill(randomBytes);

            for (int i = 0; i < length; i++)
            {
                int idx = randomBytes[i] % CharacterMatrixForRandomIVStringGeneration.Length;
                iv[i] = CharacterMatrixForRandomIVStringGeneration[idx];
            }

            return new string(iv);
        }
    }
}
