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

namespace HistoryCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() != 0)
            {
                string dataDirectory = CrawlerSettings.Default.DataDirectory;
                bool overwrite = CrawlerSettings.Default.OverWrite;

                if (overwrite)
                {
                    if (Directory.Exists(dataDirectory))
                    {
                        Directory.Delete(dataDirectory, true);
                    }
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

                    Console.WriteLine(String.Format("{0}:\tName:{1}, Desc:{2}", counter, stock.Name, stock.Description));

                    if (File.Exists(stock.Name))
                    {
                        if (overwrite)
                        {
                            File.Delete(stock.Name);
                            PersistStockAndHistory(stock);
                        }
                    }
                    else
                    {
                        PersistStockAndHistory(stock);
                    }

                    Thread.Sleep(prng.Next(1000, 5000));

                    line = reader.ReadLine();
                }

                Console.WriteLine("done! <hit return>");
                Console.ReadLine();
            }
        }

        private static void PersistStockAndHistory(Stock stock)
        {
            var stockHistoryFile = File.Create(stock.Name);
            try
            {
                StockQuotesSharp.PersistHistory(stock, stockHistoryFile, DateTime.MinValue, DateTime.Now);
            }
            finally
            {
                stockHistoryFile.Flush();
                stockHistoryFile.Close();
            }
        }
    }
}
