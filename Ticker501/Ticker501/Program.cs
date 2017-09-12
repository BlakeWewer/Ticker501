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
        public static Account account;
        private static double _feePerTrade = 9.99;
        private static double _feePerTransfer = 4.99;
        static void Main(string[] args)
        {
            db = new List<Stock>();
            account = new Account();
            StreamReader tick = new StreamReader("ticker.txt");
            string cur = tick.ReadLine();
            while(cur != "")
            {
                string[] split = cur.Split('-');
                Stock s = new Stock(split[0], split[1], 0, Convert.ToDouble(split[2].Substring(1)));
                db.Add(s);
                
                cur = tick.ReadLine();
            }
            tick.Close();
            mainMenu();

            Console.ReadLine();
        }

        public static void updateStockPrices()
        {
            StreamWriter s = new StreamWriter("Ticker.txt");
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
            s.Close();
        }

        public static void mainMenu()
        {
            Console.Clear();
            int selection = -1;
            Console.WriteLine("\n\n\n\n\nTicker501 Application");
            Console.WriteLine();
            Console.WriteLine("1 - Account Funds");
            Console.WriteLine("2 - Account Balance");
            Console.WriteLine("3 - Create a new Portfolio");
            Console.WriteLine("4 - Delete a Portfolio");
            Console.WriteLine("5 - Portfolio Balance");
            Console.WriteLine("6 - Buy and Sell Stock");
            Console.WriteLine("7 - Simulate Stock Price Change");
            while(!(selection < 8 && selection > 0))
            {
                Console.Write("\nSelect a menu option by typing in the corresponding number: ");
                try
                {
                    selection = Convert.ToInt32(Console.ReadLine());
                }catch(Exception e)
                {
                    Console.WriteLine("Not a valid input, please select one of the menu options.");
                }
            }

            if (selection == 1)
                accountFundsMenu();
            if (selection == 2)
                accountBalanceMenu();
            if (selection == 3)
                addPortfolioMenu();
            if (selection == 4)
                deletePortfolioMenu();
            if (selection == 5)
                portfolioBalanceMenu();
            if (selection == 6)
                mangaeStockMenu();
            if (selection == 7)
                simulatorMenu();
        }

        public static void accountFundsMenu()
        {
            int selection = -1;
            Console.Clear();
            Console.WriteLine("1 - Add Funds");
            Console.WriteLine("2 - Withdraw Funds");
            Console.Write("\nPlease select and option from the menu above: ");
            while (!(selection < 2 && selection > 0))
            {
                Console.Write("\nSelect a menu option by typing in the corresponding number: ");
                try
                {
                    selection = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Not a valid input, please select one of the menu options.");
                }
            }

            if (selection == 1)
            {
                bool complete = false;
                while(!complete)
                {
                    Console.Write("Enter amount you would like to add (Do NOT include symbols or commas): ");
                    try
                    {
                        account.addFunds(Convert.ToDouble(Console.ReadLine()));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter a valid number.\n");
                        continue;
                    }
                    complete = true;
                }
                mainMenu();
            }else if (selection == 2)
            {
                bool complete = false;
                while (!complete)
                {
                    Console.Write("Enter amount you would like to withdraw (Do NOT include symbols or commas): ");
                    try
                    {
                        account.withdrawFunds(Convert.ToDouble(Console.ReadLine()));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter a valid number.\n");
                        continue;
                    }
                    complete = true;
                }
                mainMenu();
            }
        }

        public static void accountBalanceMenu()
        {
            int selection = -1;
            Console.Clear();
            Console.WriteLine("1 - View Positions Balance");
            Console.WriteLine("2 - View Gains/Loss Report");
            Console.Write("\nPlease select and option from the menu above: ");
            while (!(selection < 2 && selection > 0))
            {
                Console.Write("\nSelect a menu option by typing in the corresponding number: ");
                try
                {
                    selection = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Not a valid input, please select one of the menu options.");
                }
            }

            if(selection == 1)
            {
                foreach(Portfolio h in account.Portfolios)
                {
                    account.portfolioBalancePrintOut(h);
                }
            }else if(selection == 2)
            {
                //**************************************************************************************************************************************************************
                //**************************************************************************************************************************************************************
                //**************************************************************************************************************************************************************
                //**************************************************************************************************************************************************************
            }
            mainMenu();
        }

        public static void addPortfolioMenu()
        {
            Console.Clear();
            Portfolio p = new Portfolio();
            Console.Write("Enter Portfolio Name: ");
            string name = Console.ReadLine();
            account.addPortfolio(new Portfolio(new List<Stock>(), name));

            mainMenu();
        }

        public static void deletePortfolioMenu()
        {
            Console.Clear();
            account.deletePortfolio();
            mainMenu();
        }

        public static void portfolioBalanceMenu()
        {
            Console.Clear();
            Console.WriteLine("Please select which portfolio to sell stock from...");
            if (account.Portfolios[0] != null)
                Console.Write("Enter '0' for Portfolio " + account.Portfolios[0].Name + "\t");
            if (account.Portfolios[1] != null)
                Console.Write("Enter '1' for Portfolio " + account.Portfolios[1].Name + "\t");
            if (account.Portfolios[2] != null)
                Console.Write("Enter '2' for Portfolio " + account.Portfolios[2].Name + "\t");
            Console.WriteLine();

            Console.Write("Enter Corresponding Portfolio: ");
            int portfolio = Convert.ToInt32(Console.ReadLine());

            Portfolio p = account.Portfolios[portfolio];
            account.portfolioBalancePrintOut(p);

            //**************************************************************************************************************************************************************
            //**************************************************************************************************************************************************************
            //**************************************************************************************************************************************************************
            //**************************************************************************************************************************************************************

            mainMenu();
        }

        public static void mangaeStockMenu()
        {
            int selection = -1;
            Console.Clear();
            Console.WriteLine("1 - Buy Stock");
            Console.WriteLine("2 - Sell Stock");
            Console.Write("\nPlease select and option from the menu above: ");
            while (!(selection < 2 && selection > 0))
            {
                Console.Write("\nSelect a menu option by typing in the corresponding number: ");
                try
                {
                    selection = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Not a valid input, please select one of the menu options.");
                }
            }

            if(selection == 1)
            {
                Console.Write("Enter the ticker value for the stock you are wanting to buy: ");
                string ticker = Console.ReadLine();
                account.buyStock(ticker);
            }else if(selection == 2)
            {
                Console.Write("Enter the ticker value for the stock you are wanting to sell: ");
                string ticker = Console.ReadLine();
                account.sellStock(ticker);
            }
            mainMenu();
        }

        public static void simulatorMenu()
        {
            Console.Clear();
            updateStockPrices();
            mainMenu();
        }
    }
}
