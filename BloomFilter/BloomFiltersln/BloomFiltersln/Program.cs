using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BloomFiltersln
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // fp_probability : false positive probability should be between 0.1 and 0.9
            BloomFilter bloomfilter = new BloomFilter(m: 200, fp_probability: 0.055);
            string[] text = {
                "Australia", "Belgium", "Brazil", "Canada",
                "China", "Denmark", "Germany", "Jamaica",
                "India", "Madagascar" ,"SriLanka", "Kuwait",
                "Afghanistan", "Argentina", "The Bahamas", "Luxembourg",
                "Iceland", "Bhutan", "Hungary","Sweden"
            };
            for (int i = 0; i < text.Length; i++)
            {
                bloomfilter.Add(text[i]);
            }
            int cnt = bloomfilter.Count();
            // return true if value found in the bloom filter otherwise false
            // values are case sensitive
            bool isFound1 = bloomfilter.Contains("Brazil"); 
            bool isFound2 = bloomfilter.Contains("Japan");
            

        }

    }

}
