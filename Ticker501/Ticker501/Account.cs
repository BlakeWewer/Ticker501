using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticker501
{
    class Account
    {
        private Portfolio[] _portfolios;
        private double _balance, _gains, _losses;
        private static double _feePerTrade = 9.99;
        private static double _feePerTransfer = 4.99;

        /**
         * Dummy constructor for an Account object
         * Sets the Array of portfolios to a new Array of Portfolios
         * Sets the balance, gains, and losses doubles to 0
         */
        public Account()
        {
            _portfolios = new Portfolio[3];
            _balance = 0;
            _gains = 0;
            _losses = 0;
        }

        /**
         * Constructor for an Account object that takes in an Array of Portfolios
         * Sets the Array of portfolios to passed Array
         * Sets the balance, gains, and losses doubles to 0
         */
        public Account(Portfolio[] p)
        {
            _portfolios = p;
            _balance = 0;
            _gains = 0;
            _losses = 0;
        }

        /**
         * Constructor for an Account object that takes in a current balance
         * Sets the Array of portfolios to a new Array of Portfolios
         * Sets the balance to the passed double
         * Sets gains, and losses doubles to 0
         */
        public Account(double balance)
        {
            _portfolios = null;
            _balance = balance;
            _gains = 0;
            _losses = 0;
        }

        /**
         * Getter and Setter Method for an Account's Portfolios 
         * 
         */
        public Portfolio[] Portfolios
        {
            get
            {
                return _portfolios;
            }
            set
            {
                _portfolios = value;
            }
        }

        /**
         * Getter and Setter Method for an Account's balance 
         * 
         */
        public double Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                _balance = value;
            }
        }

        /**
         * Getter and Setter Method for an Account's gains
         * 
         */
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

        /**
         * Getter and Setter Method for an Account's losses 
         * 
         */
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

        void addPortfolio(Portfolio p)
        {
            Portfolio[] cur = new Portfolio[3];
            this.Portfolios.CopyTo(cur, 0);
            int i = 0;
            while(cur[i] != null)
            {
                i++;
                if(i > 2)
                {
                    Console.WriteLine("Cannot add another portfolio, maximum of 3.");
                    break;
                }
            }
            cur[i] = p;
        }

        void deletePortfolio(Portfolio p)
        {

        }

        void buyStock(string ticker)
        {
            bool processed = false;
            while(!processed)
            {
                int portfolio = -1, max = 0;
                Console.WriteLine("Please select which portfolio to add this stock to...");
                if (_portfolios[0] != null)
                {
                    Console.Write("Enter '0' for Portfolio " + _portfolios[0].Name + "\t");
                    max = 1;
                } 
                if (_portfolios[1] != null)
                {
                    Console.Write("Enter '1' for Portfolio " + _portfolios[1].Name + "\t");
                    max = 2;
                }
                if (_portfolios[2] != null)
                {
                    Console.Write("Enter '2' for Portfolio " + _portfolios[2].Name + "\t");
                    max = 3;
                }
                Console.WriteLine();

                Console.Write("Enter Portfolio: ");
                try
                {
                    portfolio = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Value must be a valid integer from the list above.");
                }
                //************************************************************************************************************************************************************************
                //************************************************************************************************************************************************************************
                if (portfolio > 0 && portfolio < max)
                {
                    _portfolios[portfolio].buyStock(s);
                    processed = true;
                }  
                else
                {
                    throw new Exception("Integer must be within the valid range from above.");
                }
                //************************************************************************************************************************************************************************
                //************************************************************************************************************************************************************************
            }
        }

        void sellStock(string ticker)
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

        void addFunds(double amount)
        {
            Balance += (amount - _feePerTransfer);
            Losses += _feePerTransfer;
        }

        void withdrawFunds(double amount)
        {
            int max = 0;
            if (_portfolios[0] != null)
                max = 1;
            if (_portfolios[1] != null)
                max = 2;
            if (_portfolios[2] != null)
                max = 3;

            while (amount + _feePerTransfer > Balance)
            {
                Console.WriteLine("Withdrawal amount is greater than cash value in available balance.  \nWhich stocks would you like to sell in order to fulfill the withdraw transaction?");
                for(int i = 0; i < max; i++)
                {
                    Console.WriteLine("Portfolio " + i + ":");
                    _portfolios[i].portfolioPrintOut();
                }
                Console.Write("Enter portfolio number from the list above: ");
                int port = Convert.ToInt32(Console.ReadLine());

                Portfolio cur = _portfolios[port];
                cur.portfolioPrintOut();
                Console.Write("Enter the Ticker for the stock you wish to sell from " + cur.Name + ": ");
                cur.sellStock(Console.ReadLine());

                //*******************************************************
                //*******************************************************
                //*******************************************************
                //*******************************************************
                //*******************************************************
                //*******************************************************
                //*******************************************************
                //*******************************************************
                //*******************************************************
                //*******************************************************
                Balance -= (amount + _feePerTransfer);
                Losses += _feePerTransfer;
            }
        }
    }
}
