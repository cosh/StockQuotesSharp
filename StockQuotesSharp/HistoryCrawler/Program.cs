/*
* StockQuotesSharp
* Copyright (C) 2011 Henning Rauch
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using cosh.Stock;
using System.Threading;

namespace cosh.Stock
{
    /// <summary>
    /// A crawler that is able to crawl yahoo finance for a number of stocks and serializes them into a filestream
    /// 
    /// The stocks are given by an argument: HistoryCrawler.exe "NYSE.csv"
    /// 
    /// This file has to look like this:
    /// 
    /// Symbol, Name
    /// A,Agilent Technologies
    /// AA,Alcoa Inc.
    /// AAN,Aaron's Inc.
    /// 
    /// </summary>
    class HistoryCrawler
    {
        static void Main(string[] args)
        {
            if (args.Count() != 0)
            {
                string dataDirectory = CrawlerSettings.Default.DataDirectory;
                bool overwrite = CrawlerSettings.Default.OverWrite;

                if (Directory.Exists(dataDirectory))
                {
                    if (overwrite)
                    {
                        Directory.Delete(dataDirectory, true);
                        Directory.CreateDirectory(dataDirectory);
                    }
                }
                else
                {
                    Directory.CreateDirectory(dataDirectory);
                }

                FileStream symbolFile = File.OpenRead(args[0]);

                StreamReader reader = new StreamReader(symbolFile);

                //skip first
                reader.ReadLine();

                String line = reader.ReadLine();

                Random prng = new Random();
                int counter = 0;

                while (line != null)
                {
                    counter++;

                    var stock = new Stock(line);

                    string file = dataDirectory + Path.DirectorySeparatorChar + stock.Name;

                    Console.WriteLine(String.Format("{0}:\tName:{1}, Desc:{2}", counter, stock.Name, stock.Description));

                    if (File.Exists(file))
                    {
                        if (overwrite)
                        {
                            File.Delete(file);
                            PersistStockAndHistory(stock, file);
                        }
                    }
                    else
                    {
                        PersistStockAndHistory(stock, file);
                    }

                    Thread.Sleep(prng.Next(1000, 5000));

                    line = reader.ReadLine();
                }

                Console.WriteLine("done! <hit return>");
                Console.ReadLine();
            }
        }

        private static void PersistStockAndHistory(Stock myStock, String myFileName)
        {
            var stockHistoryFile = File.Create(myFileName);
            try
            {
                StockQuotesSharp.SerializeHistory(myStock, stockHistoryFile, DateTime.MinValue, DateTime.Now);
            }
            catch (Exception e)
            {
                Console.WriteLine(String.Format("Error while crawling history for stock {0} because of \"{1}\".", myStock.Name, e.Message));
            }
            finally
            {
                stockHistoryFile.Flush();
                stockHistoryFile.Close();
            }
        }
    }
}
