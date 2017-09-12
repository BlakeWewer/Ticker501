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
        private string _name;
        private double _gains, _losses;

        public Portfolio()
        {
            _stocks = new List<Stock>();
            _name = null;
            _gains = 0;
            _losses = 0;
        }

        public Portfolio(List<Stock> s, string name)
        {
            _stocks = s;
            _name = name;
            _gains = 0;
            _losses = 0;
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

        
    }
}
