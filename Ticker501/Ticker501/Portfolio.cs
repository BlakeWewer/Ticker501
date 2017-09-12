using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void buyStock(Stock s)
        {
            Stocks.Add(s);
        }

        public void sellStock(Stock s)
        {
            /*
            Console.WriteLine("Please select which portfolio to sell stock from...");
            if (_portfolios[0] != null)
                Console.Write("Enter '0' for Portfolio " + _portfolios[0].Name + "\t");
            if (_portfolios[1] != null)
                Console.Write("Enter '1' for Portfolio " + _portfolios[1].Name + "\t");
            if (_portfolios[2] != null)
                Console.Write("Enter '2' for Portfolio " + _portfolios[2].Name + "\t");
            Console.WriteLine();
            
            Console.Write("Enter Portfolio: ");
            int portfolio = Convert.ToInt32(Console.ReadLine());
            */
            //************************************************************************************************************************************************************************
            Console.WriteLine("You currently have " + s.Stocks + " " + s.Ticker + " stocks bought for " + s.Price + ".  \nHow many would you like to sell for ");
            //************************************************************************************************************************************************************************
        }

        public void portfolioPrintOut()
        {
            Console.WriteLine("\n\nCurrent Portfolio Status:\n");
            foreach(Stock h in _stocks)
            {
                String cur = String.Format("${0,-10}\t- ({0:C2})% {0} {0}", (h.Stocks * h.Price), (h.Stocks * 100 / _tStocks), h.Ticker, h.Company);
                Console.WriteLine(cur);
                cur = "";
            }
        }
    }
}
