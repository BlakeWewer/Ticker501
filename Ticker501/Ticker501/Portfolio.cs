using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ticker501
{
    class Portfolio
    {
        private List<Stock> _stocks;
        private int _tStocks;
        private string _name;
        private double _gains, _losses;
        private static double _feePerTrade = 9.99;
        private static double _feePerTransfer = 4.99;

        public Portfolio()
        {
            _stocks = new List<Stock>();
            _tStocks = 0;
            _name = null;
            _gains = 0;
            _losses = 0;
        }

        public Portfolio(List<Stock> s, string name)
        {
            _stocks = s;
            _tStocks = 0;
            _name = name;
            _gains = 0;
            _losses = 0;
            foreach(Stock h in s)
            {
                _tStocks += h.Stocks;
            }
        }

        public List<Stock> Stocks
        {
            get
            {
                return _stocks;
            }
            set
            {
                _stocks = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public double Gains
        {
            get
            {
                return _gains;
            }
            set
            {
                _gains = value;
            }
        }

        public double Losses
        {
            get
            {
                return _losses;
            }
            set
            {
                _losses = value;
            }
        }

        public void buyStock(string ticker, double curBalance)
        {
            List<Stock> db = new List<Stock>();
            Stock toAdd = new Stock();
            StreamReader tick = new StreamReader("ticker.txt");
            string cur = tick.ReadLine();
            while (cur != "")
            {
                string[] split = cur.Split('-');
                Stock s = new Stock(split[0], split[1], 50, Convert.ToDouble(split[2].Substring(1)));
                db.Add(s);

                cur = tick.ReadLine();
            }

            foreach(Stock h in db)
            {
                if(h.Ticker.Equals(ticker))
                {
                    Console.WriteLine("Current Price for " + ticker + ":");
                    Console.WriteLine(h.ToString());
                    toAdd.Company = h.Company;
                    toAdd.Ticker = h.Ticker;
                    toAdd.Price = h.Price;
                }
            }
            Console.Write("Would you like to purchase a specific number of stocks [1] or enter a dollar amount to purchase stock [2] ?: ");
            int type = Convert.ToInt32(Console.ReadLine());
            if(type == 1)
            {
                bool complete = false;
                int numStocks = 0;
                while (!complete)
                {
                    Console.Write("Enter the number of stocks you wish to purchase: ");
                    try
                    {
                        numStocks = Convert.ToInt32(Console.ReadLine());
                    }catch(Exception e)
                    {
                        Console.WriteLine("Please enter a valid integer value.");
                        continue;
                    }
                    toAdd.Stocks = numStocks;
                    complete = true;
                }
                if(numStocks * toAdd.Price + _feePerTrade < curBalance)
                {
                    _stocks.Add(toAdd);
                }else
                {
                    Console.WriteLine("Insufficient Funds to buy this quantity of stock.");
                }
                

            }
        }

        public void sellStock(string ticker)
        {
            int index = -1;
            for(int i = 0; i < this.Stocks.Count; i++)
            {
                if(ticker.Equals(Stocks[i].Ticker))
                {
                    index = i;
                    break;
                }
            }

            Stock s = Stocks[index];


            //Get Current Value of the stock
            //************************************************************************************************************************************************************************
            Console.WriteLine("You currently have " + s.Stocks + " " + s.Ticker + " stocks bought for " + s.Price + ".  \nHow many would you like to sell for ");
            //************************************************************************************************************************************************************************
        }

        public void portfolioPrintOut()
        {
            Console.WriteLine("\n\nCurrent Portfolio \'" + this.Name + "\' Status:\n");
            foreach(Stock h in _stocks)
            {
                String cur = String.Format("${0,-10}\t- ({0:C2})% {0} {0}", (h.Stocks * h.Price), (h.Stocks * 100 / _tStocks), h.Ticker, h.Company);
                Console.WriteLine(cur);
                cur = "";
            }
        }
    }
}
