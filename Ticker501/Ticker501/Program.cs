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
            db = new List<Stock>();

            StreamReader tick = new StreamReader("ticker.txt");
            string cur = tick.ReadLine();
            while(cur != "")
            {
                string[] split = cur.Split('-');
                Stock s = new Stock(split[0], split[1], 0, Convert.ToDouble(split[2].Substring(1)));
                db.Add(s);
                
                cur = tick.ReadLine();
            }

            Console.WriteLine("Ticker501 Application");
            Console.WriteLine()






            Console.ReadLine();
        }

        public static void updateStockPrices()
        {
            StreamWriter s = new StreamWriter("ticker.txt");
            Random r = new Random();
            Console.WriteLine("\n\n\nEnter L for low volatility (1% - 4%),");
            Console.WriteLine("M for medium volatility (2% - 8%),");
            Console.WriteLine("or H for high volatility (3% - 15%)");
            Console.Write("Selection: ");
            string input = Console.ReadLine();

            int first = -1, second = -1;
            if(input.Equals("L"))
            {
                foreach(Stock h in db)
                {
                    first = r.Next(0, 2);
                    second = r.Next(1, 5);
                    if(first == 0)
                    {
                        h.Price = h.Price - (h.Price * second / 100);
                    }else if(first == 1)
                    {
                        h.Price = h.Price + (h.Price * second / 100);
                    }
                }
            }else if(input.Equals("M"))
            {
                foreach (Stock h in db)
                {
                    first = r.Next(0, 2);
                    second = r.Next(2, 9);
                    if (first == 0)
                    {
                        h.Price = h.Price - (h.Price * second / 100);
                    }
                    else if (first == 1)
                    {
                        h.Price = h.Price + (h.Price * second / 100);
                    }
                }
            }
            else if (input.Equals("H"))
            {
                foreach (Stock h in db)
                {
                    first = r.Next(0, 2);
                    second = r.Next(3, 16);
                    if (first == 0)
                    {
                        h.Price = h.Price - (h.Price * second / 100);
                    }
                    else if (first == 1)
                    {
                        h.Price = h.Price + (h.Price * second / 100);
                    }
                }
            }
            foreach(Stock h in db)
            {
                s.WriteLine(h.Ticker + "-" + h.Company + "-$" + h.Price);
            }
        }

    }
}
