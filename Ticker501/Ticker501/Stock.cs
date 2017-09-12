using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticker501
{
    class Stock
    {
        private string _ticker, _company, _name;
        private int _stocks;
        private double _price;

        public Stock()
        {
            _name = null;
            _ticker = null;
            _company = null;
            _stocks = 0;
            _price = 0;
        }

        public Stock(string name, string ticker, string company, int stocks, double price)
        {
            _name = name;
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
    }
}
