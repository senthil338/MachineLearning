using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomFiltersln
{
    class BloomFilter
    {
        private BitArray bits;
        private int numHashes;
        private int bloomFilterCount;

        /// <summary>
        /// Construct Bloom Filter
        /// </summary>
        /// <param name="m">Number of items to be stored in the bloom filter</param>
        /// <param name="fp_probability">False Positive probability should be between 0.1 and 0.9 </param>
        public BloomFilter(int m, double fp_probability)
        {
            // Size of bit array to use 
            int size = get_size(m, fp_probability);
            this.numHashes = get_hash_count(size, m);
            this.bits = new BitArray(size);
            bloomFilterCount = size;
        }

        /// <summary>
        /// Size of Bit Array
        /// Formula can be found at : https://en.wikipedia.org/wiki/Bloom_filter
        /// </summary>
        /// <param name="n">number of items</param>
        /// <param name="p">False Positive probability </param>
        /// <returns></returns>
        private int get_size(int n,double p)
        {
            double m = -(n * Math.Log(p)) / (Math.Pow(Math.Log(2),2));
            int result = (int)m;
            //Edge Case : minimum n number of bit array require to utilize the bloom filter features 
            //Corner case : increase the bit array count 5 times more than size of n to get the benefit of bloom filter.
            return result <= n ? n*5:result;

        }

        /// <summary>
        /// Optimum number of hash functions
        /// Formula can be found at : https://en.wikipedia.org/wiki/Bloom_filter
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private int get_hash_count(int m,int n)
        {
            double k = (m / n) * Math.Log(2);
            int result = (int)k;
            //Edge Case : minimum one hash functions require to build bloom data structure ,
            //Corner case : require more than 1 hash function to avoid most false positive
            return result <= 2 ? 3: result;
        }

        /// <summary>
        /// Count number bit array used to store the values
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            int count = 0;
            for (int i = 0; i < bloomFilterCount; i++)
            {
                if (bits[i])
                {
                    count++;
                }
            }
            return count;
        }


        /// <summary>
        /// Add input string hash code to bloom filter bit array
        /// </summary>
        /// <param name="input">input value</param>
        public void Add(string input)
        {
                int[] hashs = input.GenerateKHashCode(numHashes);
                for (int i = 0; i < numHashes; i++)
                {
                    bits[hashs[i] % bloomFilterCount] = true;
                }
        }

        /// <summary>
        /// Check given input present or not in the bloom filter 
        /// </summary>
        /// <param name="input">string value</param>
        /// <returns>return true if present otherwise false</returns>
        public bool Contains(string input)
        {
            int[] hashes = input.GenerateKHashCode(numHashes);
            for (int i = 0; i < numHashes; i++)
            {
                if (bits[hashes[i] % bloomFilterCount] == false) return false;
            }
            return true;
        }

        /// <summary>
        /// Set all bloom filter bit array value to false
        /// </summary>
        public void Clear()
        {
            this.bits = new BitArray(bloomFilterCount);
        }
    }
}
