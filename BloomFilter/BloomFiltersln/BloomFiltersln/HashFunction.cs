using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BloomFiltersln
{
    /// <summary>
    /// Hash Functions
    /// </summary>
  
    public static class HashFunction
    {

        /// <summary>
        /// Two Toy Hash function
        /// </summary>
        /// <param name="input">string as input</param>
        /// <returns>Hash code</returns>
        private static int two_toy_hash_code(this string input,int n)
        {
           
            unchecked
            {
                int ReturnValue = n;
                for (int i = 0; i < input.Length; i++)
                {
                    ReturnValue += (int)((int)input[i] * i);
                    ReturnValue = (int)(ReturnValue % 64);
                }

                return Math.Abs(ReturnValue);
            }
        }


        /// <summary>
        /// Two Toy Hash function
        /// </summary>
        /// <param name="input">string as input</param>
        /// <returns>Hash code</returns>
        private static int two_toy_custom_hash_code(this string input,int n)
        {
            unchecked
            {
                int ReturnValue = n;
                for (int i = 0; i < input.Length; i++)
                {
                    ReturnValue += ((int)input[i]);
                    ReturnValue = ReturnValue % 32;
                }
                return Math.Abs(ReturnValue);
            }
        }
        /// <summary>
        /// MDH5 Hash function
        /// </summary>
        /// <param name="input">string as input</param>
        /// <returns>Hash code</returns>
        private static int md5_hash_code(this string input,int n)
        {
            unchecked
            {
                MD5 md5Hasher = MD5.Create();
                var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
                var ivalue = BitConverter.ToInt32(hashed, 0);
                return Math.Abs(ivalue + n);
            }

        }

        /// <summary>
        /// MDH5 Custom Hash function
        /// </summary>
        /// <param name="input">string as input</param>
        /// <returns>Hash code</returns>
        private static int sha256_hash_code(this string input,int n)
        {
            unchecked
            {
                SHA256 sha256Hasher = SHA256.Create();
                var hashed = sha256Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
                var ivalue = (BitConverter.ToInt32(hashed, 0));
                return Math.Abs(ivalue+n);
            }

        }

        /// <summary>
        /// Custom Hash function
        /// </summary>
        /// <param name="input">string as input</param>
        /// <param name="n">nth number of Hash function</param>
        /// <returns>Hash code</returns>
        private static int custom_hash_code(this string input,int n)
        {
            unchecked
            {
                int total = n;
                char[] c;
                c = input.ToCharArray();

                // Summing up all the ASCII values  
                // of each alphabet in the string 
                for (int k = 0; k <= c.GetUpperBound(0); k++)
                    total += (int)c[k];

                return total;
            }
        }
        /// <summary>
        /// Dynamically generate K number of Hash key for a given string
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="k">Number of hash code</param>
        /// <returns>list of hash code</returns>
        public static int[] GenerateKHashCode(this string s, int k)
        {
            int[] hashes = new int[k];
            hashes[0] = Math.Abs(s.GetHashCode());
            Random R = new Random(hashes[0]);
            for (int i = 1; i < k; i++)
            {
                switch (k % i) // to call random hash code function
                {
                    case 0:
                        hashes[i] = R.Next() + i % k; 
                        break;
                    case 1:
                        hashes[i] = two_toy_custom_hash_code(s, i);
                        break;
                    case 2:
                        hashes[i] = md5_hash_code(s,i);
                        break;
                    case 3:
                        hashes[i] = two_toy_hash_code(s,i);
                        break;
                    case 4:
                        hashes[i] = custom_hash_code(s,i);
                        break;
                    case 5:
                        hashes[i] = sha256_hash_code(s,i);
                        break;
                    default:
                        hashes[i] = Math.Abs(Tuple.Create(R.Next(), i % k).GetHashCode());  
                        break;
                }

            }
            return hashes;
        }

    }
}

