using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using AUSKF.Api.Exceptions;

namespace AUSKF.Api.Extensions
{
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

        /// <summary>
        ///   Ensures the slashes before.
        /// </summary>
        /// <param name="s"> The s. </param>
        /// <returns> </returns>
        public static string EnsureSlashesBefore(this string s)
        {
            if (s == null)
            {
                return null;
            }

            if (s.StartsWith("\\"))
            {
                return s;
            }
            return "\\" + s;
        }

        /// <summary>
        ///   Finds the config path.
        /// </summary>
        /// <param name="fileName"> The name of the file.(i.e. notes.txt, index.html etc.) </param>
        /// <param name="prependPath"> if set to <c>true</c> [prepend path]. </param>
        /// <returns> </returns>
        public static string FindPath(this string fileName, bool prependPath)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ParameterNullException("fileName");
            }

            string configName = fileName.EnsureSlashesBefore();
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string path = string.Empty;

            // first see if a file exists in the given path
            // this 
            if (File.Exists(fileName))
            {
                // yes we really do want to do an assignment here, and not just
                // return the string.
                if (prependPath)
                {
                    FileInfo fi = new FileInfo(fileName);
                    path = fi.FullName;
                }
                else
                {
                    path = fileName;
                }
            }
            // try all the usual suspects.
            else if (File.Exists(basePath + configName))
            {
                path = basePath + configName;
            }
            // try adding \\bin
            else if (File.Exists(basePath + "\\bin" + configName))
            {
                path = basePath + "\\bin" + configName;
            }
            // try adding \\bin\\Debug
            else if (File.Exists(basePath + "\\bin\\Debug" + configName))
            {
                path = basePath + "\\bin\\Debug" + configName;
            }
            // try release
            else if (File.Exists(basePath + "\\bin\\Release" + configName))
            {
                path = basePath + "\\bin\\Release" + configName;
            }
            // try App_Data
            else if (File.Exists(basePath + "\\App_Data" + configName))
            {
                path = basePath + "\\App_Data" + configName;
            }
            else if (File.Exists(basePath + "\\bin\\App_Data" + configName))
            {
                path = basePath + "\\bin\\App_Data" + configName;
            }
            else if (File.Exists(basePath + "\\bin\\Debug\\App_Data" + configName))
            {
                path = basePath + "\\bin\\Debug\\App_Data" + configName;
            }
            else if (File.Exists(basePath + "\\bin\\Release\\App_Data" + configName))
            {
                path = basePath + "\\bin\\Release\\App_Data" + configName;
            }
            else if (File.Exists(configName))
            {
                path = configName;
            }
            return path;
        }

        /// <summary>
        ///   Finds the full file path.
        /// </summary>
        /// <param name="fileName"> The name of the file.(i.e. notes.txt, index.html etc.) </param>
        /// <returns> </returns>
        public static string FindPath(this string fileName)
        {
            return fileName.FindPath(false);
        }
    }
}