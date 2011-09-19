/*
* StockQuotesSharp
* Copyright (C) 2011 Henning Rauch
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cosh.Stock
{
    /// <summary>
    /// A stock history
    /// </summary>
    [Serializable]
    public sealed class StockHistory
    {
        #region data

        /// <summary>
        /// The stock
        /// </summary>
        public readonly Stock Stock;

        /// <summary>
        /// The start date of the history
        /// </summary>
        public readonly DateTime StartDate;

        /// <summary>
        /// The end date of the history
        /// </summary>
        public readonly DateTime EndDate;

        /// <summary>
        /// The quotes within the stock history
        /// </summary>
        public readonly List<StockQuote> Quotes;

        #endregion

        #region constructor

        /// <summary>
        /// Creates a new StockHistory
        /// </summary>
        /// <param name="myStock">The stock</param>
        /// <param name="myStartDate">The starting date</param>
        /// <param name="myEndDate">The end date</param>
        /// <param name="myQuotes">The quotes between start and end</param>
        public StockHistory(Stock myStock, DateTime myStartDate, DateTime myEndDate, IEnumerable<StockQuote> myQuotes)
        {
            Stock = myStock;
            StartDate = myStartDate;
            EndDate = myEndDate;
            Quotes = new List<StockQuote>(myQuotes);
        }

        #endregion
    }
}
