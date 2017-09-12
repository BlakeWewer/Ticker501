using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ticker501
{
    class Program
    {
        public static List<Stock> db;
        private static double _feePerTrade = 9.99;
        private static double _feePerTransfer = 4.99;
        static void Main(string[] args)
        {
            List<Stock> db = new List<Stock>();

            StreamReader tick = new StreamReader("ticker.txt");
            string cur = tick.ReadLine();
            while(cur != "")
            {
                string[] split = cur.Split('-');
                Stock s = new Stock(split[0], split[1], 0, Convert.ToDouble(split[2].Substring(1)));
                db.Add(s);
                
                cur = tick.ReadLine();
            }
            Console.ReadLine();
        }

    }
}
