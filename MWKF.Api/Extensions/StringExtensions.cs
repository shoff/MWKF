namespace MWKF.Api.Extensions
{
    using System.Security.Cryptography;
    using System.Text;

    public static class StringExtensions
    {

        /// <summary>
        ///   Creates a one way hash for string passed using the SHA256 hashing algorithm.
        /// </summary>
        /// <param name="s"> The string to hash. </param>
        /// <returns> </returns>
        public static string Sha256Hash(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            SHA256 sha256 = SHA256.Create();

            byte[] dataSha256 = sha256.ComputeHash(Encoding.Default.GetBytes(s));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataSha256.Length; i++)
            {
                sb.AppendFormat("{0:x2}", dataSha256[i]);
            }
            return sb.ToString();
        }
    }
}