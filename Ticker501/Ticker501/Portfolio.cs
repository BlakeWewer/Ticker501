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

        public int TStocks
        {
            get
            {
                return _tStocks;
            }

            set
            {
                _tStocks = value;
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
            tick.Close();
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
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter a valid integer value.");
                        continue;
                    }
                    toAdd.Stocks = numStocks;

                    if (numStocks * toAdd.Price + _feePerTrade < curBalance)
                    {
                        _stocks.Add(toAdd);
                        complete = true;
                    }
                    else
                    {
                        Console.WriteLine("Insufficient Funds to buy this quantity of stock.");
                    }
                }
            }else if(type == 2)
            {
                bool complete = false;
                double amount = 0;
                while (!complete)
                {
                    Console.Write("Enter the dollar amount to purchase stock: ");
                    try
                    {
                        amount = Convert.ToDouble(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter a valid integer value.");
                        continue;
                    }
                    toAdd.Stocks = (int)(amount / toAdd.Price);

                    if (amount + _feePerTrade < curBalance)
                    {
                        _stocks.Add(toAdd);
                        complete = true;
                    }
                    else
                    {
                        Console.WriteLine("Insufficient Funds to buy this quantity of stock.");
                    }
                }
            }
        }

        public double sellStock(string ticker)
        {
            List<Stock> db = new List<Stock>();
            Stock toAdd = new Stock();
            StreamReader tick = new StreamReader("ticker.txt");
            string curS = tick.ReadLine();
            while (curS != "")
            {
                string[] split = curS.Split('-');
                Stock s = new Stock(split[0], split[1], 50, Convert.ToDouble(split[2].Substring(1)));
                db.Add(s);

                curS = tick.ReadLine();
            }
            tick.Close();
            int index = -1;
            for(int i = 0; i < this.Stocks.Count; i++)
            {
                if(ticker.Equals(Stocks[i].Ticker))
                {
                    index = i;
                    break;
                }
            }
            Stock cur = Stocks[index];
            double sellPrice = 0;
            foreach (Stock h in db)
            {
                if (h.Ticker.Equals(ticker))
                {
                    Console.WriteLine("Selling Price for " + ticker + ":");
                    Console.WriteLine(h.Price);
                    sellPrice = h.Price;
                }
                break;
            }
            Console.WriteLine("You currently have " + cur.Stocks + " " + cur.Ticker + " stocks bought for " + cur.Price + ".  \nHow many would you like to sell for " + sellPrice + "?: ");
            int nStocks = Convert.ToInt32(Console.ReadLine());
            while(nStocks > cur.Stocks)
            {
                Console.WriteLine("You don't have that many stocks to sell.  Please enter a valid number.");
                Console.WriteLine("You currently have " + cur.Stocks + " " + cur.Ticker + " stocks bought for " + cur.Price + ".  \nHow many would you like to sell for " + sellPrice + "?: ");
                nStocks = Convert.ToInt32(Console.ReadLine());
            }
            double gain = (nStocks * sellPrice - nStocks * cur.Price) - 2 * _feePerTrade;
            double value = nStocks * sellPrice - _feePerTrade;
            cur.Stocks -= nStocks;
            if(cur.Stocks == 0)
            {
                Stocks[index] = null;
                for(int i = 0; i < Stocks.Count - 1; i++)
                {
                    Stocks[i] = Stocks[i + 1];
                }
            }
            _gains += gain;
            _losses += _feePerTrade;
            return value;
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

            Console.WriteLine("Gains: " + _gains);
            Console.WriteLine("Losses: " + _losses);
        }

        public double stocksSum()
        {
            double sum = 0;
            foreach(Stock h in _stocks)
            {
                sum += (h.Stocks * h.Price);
            }
            return sum;
        }
    }
}
