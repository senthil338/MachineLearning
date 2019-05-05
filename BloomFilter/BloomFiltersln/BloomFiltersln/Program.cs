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
            //m number of items to be constructed 
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
            

            string[] find = {
                "Australia", "Bahrain","Belgium", "Colombia","Brazil", "Canada",
                "China", "Denmark", "Germany", "Jamaica",
                "India", "Madagascar" ,"SriLanka", "Kuwait",
                "Afghanistan", "Argentina", "The Bahamas", "Costa Rica","Luxembourg",
                "Iceland", "Bhutan", "Hungary","Sweden","Japan"
            };

            for(int i=0;i< find.Length; i++)
            {
                if(bloomfilter.Contains(find[i]))
                {
                    Console.WriteLine("Bloom Filter contain {0}", find[i]);
                }
                else
                {
                    Console.WriteLine("Bloom Fiter not contain {0}", find[i]);
                }
            }

            Console.ReadLine();
            

        }

    }

}
