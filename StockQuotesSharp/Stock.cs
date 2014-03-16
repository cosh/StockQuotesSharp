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
    /// A stock
    /// </summary>
    [Serializable]
    public sealed class Stock
    {
        #region data

        /// <summary>
        /// The name of the stock
        /// </summary>
        public readonly String Name;

        /// <summary>
        /// The description of the stock
        /// </summary>
        public readonly String Description;

        #endregion

        #region constructor

        public Stock(string str)
        {
			var splittedString = str.Split(';');

            Name = splittedString[0];
            Description = splittedString[1];
        }

        #endregion

    }
}
