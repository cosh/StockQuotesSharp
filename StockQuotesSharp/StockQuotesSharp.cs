/*
* StockQuotesSharp
* Copyright (C) 2011 Henning Rauch
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Web;

namespace cosh.Stock
{
    /// <summary>
    /// A stock quotes API
    /// </summary>
    public static class StockQuotesSharp
    {
        #region public methods

        /// <summary>
        /// Get historic stock quotes
        /// </summary>
        /// <param name="myStockSymbol">The stock symbol (like AAPL)</param>
        /// <param name="myFromDate">The date where the historic data should start</param>
        /// <param name="myToDate">The date where the history data should end</param>
        /// <returns>A lazy enumerable of stock quotes</returns>
        public static IEnumerable<StockQuote> GetHistoricQuotes(String myStockSymbol, DateTime myFromDate, DateTime myToDate)
        {
            #region initial checks

            if (String.IsNullOrWhiteSpace(myStockSymbol))
            {
                throw new ArgumentOutOfRangeException("myStockSymbol", "A stock symbol cannot be empty or null");
            }

            if (myFromDate > myToDate)
	        {
                throw new ArgumentOutOfRangeException("myFromDate", "The from-date should always be older than the to-date");
	        }

            #endregion

            var myFromMonth = myFromDate.Month - 1;
            var myToMonth = myToDate.Month - 1;
            var webrequest = HttpWebRequest.Create(
                new Uri("http://ichart.finance.yahoo.com/table.csv?s=" + HttpUtility.UrlEncode(myStockSymbol) + "&d=" + myToMonth + "&e=" + myToDate.Day + "&f=" + myToDate.Year + "&g=d&a=" + myFromMonth + "&b=" + myFromDate.Day + "&c=" + myFromDate.Year + "&ignore=.cvs"));

            var response = GetResponse(myStockSymbol, webrequest);

            var reader = new StreamReader(response.GetResponseStream());
            
            //skip the first line
            reader.ReadLine();

            string str = reader.ReadLine();
                       
            while (str != null)
            {
                yield return new StockQuote(str);
                str = reader.ReadLine();
            }

            response.Close();

            yield break;
        }

        /// <summary>
        /// Serializes the history into a stream
        /// </summary>
        /// <param name="stock">The interesting stock</param>
        /// <param name="myStream">The stream in which the result should be written to</param>
        /// <param name="myFromDate">The date where the historic data should start</param>
        /// <param name="myToDate">The date where the history data should end</param>
        public static void SerializeHistory(Stock stock, Stream myStream, DateTime myFromDate, DateTime myToDate)
        {
            var stockhistory = new StockHistory(stock, myFromDate, myToDate, GetHistoricQuotes(stock.Name, myFromDate, myToDate));
            var gzip = new GZipStream(myStream, CompressionMode.Compress);

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(gzip, stockhistory);
        }

        /// <summary>
        /// Deserializes the history from a stream
        /// </summary>
        /// <param name="myStream">The stream that contains the stock history</param>
        /// <returns>A StockHistory object</returns>
        public static StockHistory SerializeHistory(Stream myStream)
        {
            var gzip = new GZipStream(myStream, CompressionMode.Decompress);

            BinaryFormatter formatter = new BinaryFormatter();
            return (StockHistory)formatter.Deserialize(gzip);
        }

        #endregion

        #region private helper

        /// <summary>
        /// Creates a web response corresponding to a stock symbol and a webrequest
        /// </summary>
        /// <param name="myStockSymbol">The stock symbol that should be requestet</param>
        /// <param name="webrequest">The webrequest that has been sent</param>
        /// <returns>A http web response</returns>
        private static HttpWebResponse GetResponse(String myStockSymbol, WebRequest webrequest)
        {
            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)webrequest.GetResponse();
            }
            catch (WebException e)
            {
                response.Close();

                switch (((HttpWebResponse)e.Response).StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        throw new ArgumentException("myStockSymbol", myStockSymbol + " is not a valid stock symbol");

                    default:
                        throw e;
                }
            }
            return response;
        }

        #endregion
    }
}
