﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ticker501
{
    class Account
    {
        private Portfolio[] _portfolios;
        private double _balance, _stocks, _gains, _losses;
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
            _portfolios = new Portfolio[3];
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
                double sum = 0;
                foreach(Portfolio h in _portfolios)
                {
                    sum += h.Gains;
                }
                _gains = sum;
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
                double sum = 0;
                foreach (Portfolio h in _portfolios)
                {
                    sum += h.Losses;
                }
                _losses = sum;
                return _losses;
            }
            set
            {
                _losses = value;
            }
        }

        public double Stocks
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

        public void addPortfolio(Portfolio p)
        {
            Portfolio[] cur = new Portfolio[3];
            bool add = true;
            this.Portfolios.CopyTo(cur, 0);
            int i = 0;
            while(cur[i] != null)
            {
                i++;
                if(i > 2)
                {
                    Console.WriteLine("Cannot add another portfolio, maximum of 3.");
                    add = false;
                    break;
                }
            }
            if(add)
                cur[i] = p;
            else
            {
                Console.WriteLine("Could not add portfolio.");
            }
        }

        public void deletePortfolio()
        {
            Portfolio cur = new Portfolio();
            bool found = false;
            int portfolio = -1;
            while (!found)
            {
                int max = 0;
                Console.WriteLine("Please select which portfolio to delete...");
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
                if (portfolio > 0 && portfolio < max)
                {
                    cur = _portfolios[portfolio];
                    found = true;
                }
                else
                {
                    throw new Exception("Integer must be within the valid range from above.");
                }
            }
            foreach(Stock h in cur.Stocks)
            {
                _gains += cur.Gains;
                _losses += cur.Losses;
                double sellReturn = cur.sellStock(h.Ticker);
                _stocks -= sellReturn;
                _balance += sellReturn;
            }
            _portfolios[portfolio] = null;

            for (int i = 0; i < _portfolios.Count() - 1; i++)
            {
                _portfolios[i] = _portfolios[i + 1];
            }
        }

        public void buyStock(string ticker)
        {
            Portfolio cur = new Portfolio();
            bool processed = false;
            while (!processed)
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
                if (portfolio > 0 && portfolio < max)
                {
                    cur = _portfolios[portfolio];
                    processed = true;
                }
                else
                {
                    throw new Exception("Integer must be within the valid range from above.");
                }
            }

            cur.buyStock(ticker, _balance);

            double transactionAmount = cur.Stocks[cur.Stocks.Count - 1].Price * cur.Stocks[cur.Stocks.Count - 1].Stocks;
            _balance -= transactionAmount + _feePerTrade;
            _stocks += transactionAmount;
            _losses += _feePerTrade;
        }

        public void sellStock(string ticker)
        {
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

            Portfolio cur = _portfolios[portfolio];
            double sellReturn = cur.sellStock(ticker);
            _stocks -= sellReturn;
            _balance += sellReturn;

        }

        public void addFunds(double amount)
        {
            _balance += (amount - _feePerTransfer);
            _losses += _feePerTransfer;
        }

        public void withdrawFunds(double amount)
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
                Balance -= (amount + _feePerTransfer);
                Losses += _feePerTransfer;
            }
        }

        public void portfolioBalancePrintOut(Portfolio p)
        {
            String cur = String.Format("{0:C2}",(p.stocksSum() * 100 / _stocks));
            Console.WriteLine("\n\n" + p.Name + " Portfolio:\n");
            Console.WriteLine("Total Investment: $" + p.stocksSum());
            Console.WriteLine("Percentage of Account: " + cur + "%");
            Console.WriteLine();

            Console.WriteLine("Stock Breakdown:");
            p.portfolioPrintOut();
        }
    }
}
