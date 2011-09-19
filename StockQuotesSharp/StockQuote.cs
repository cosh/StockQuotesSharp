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
    /// A stock quote
    /// </summary>
    public sealed class StockQuote
    {
        #region data

        /// <summary>
        /// The date of the stock quote
        /// </summary>
        public readonly DateTime Date;

        /// <summary>
        /// The opening quote
        /// </summary>
        public readonly Double Open;

        /// <summary>
        /// The highest quote
        /// </summary>
        public readonly Double High;

        /// <summary>
        /// The lowest quote
        /// </summary>
        public readonly Double Low;

        /// <summary>
        /// The close quote
        /// </summary>
        public readonly Double Close;

        /// <summary>
        /// The volume of the quote
        /// </summary>
        public readonly UInt64 Volume;

        /// <summary>
        /// The adjusted closing prices
        /// </summary>
        public readonly Double AdjustedClosingPrices;

        #endregion

        #region constructor

        public StockQuote(string str)
        {
            var splittedString = str.Split(',');

            Date = Convert.ToDateTime(splittedString[0]);
            Open = Convert.ToDouble(splittedString[1]);
            High = Convert.ToDouble(splittedString[2]);
            Low = Convert.ToDouble(splittedString[3]);
            Close = Convert.ToDouble(splittedString[4]);
            Volume = Convert.ToUInt64(splittedString[5]);
            AdjustedClosingPrices = Convert.ToDouble(splittedString[6]);
        }

        #endregion

    }
}
