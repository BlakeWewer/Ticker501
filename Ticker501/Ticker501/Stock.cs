using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ticker501
{
    class Stock
    {
        private string _ticker, _company;
        private int _stocks;
        private double _price;
        private static double _feePerTrade = 9.99;
        private static double _feePerTransfer = 4.99;

        public Stock()
        {
            _ticker = null;
            _company = null;
            _stocks = 0;
            _price = 0;
        }

        public Stock(string ticker, string company, int stocks, double price)
        {
            _ticker = ticker;
            _company = company;
            _stocks = stocks;
            _price = price;
        }

        public string Ticker
        {
            get
            {
                return _ticker;
            }

            set
            {
                _ticker = value;
            }
        }

        public string Company
        {
            get
            {
                return _company;
            }

            set
            {
                _company = value;
            }
        }

        public int Stocks
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

        public double Price
        {
            get
            {
                return _price;
            }

            set
            {
                _price = value;
            }
        }

        public override string ToString()
        {
            return (_ticker + " - " + _company + " - Price: " + _price + " - Stocks: " + _stocks);
        }
    }
}
