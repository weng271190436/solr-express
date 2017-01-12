﻿namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseRandomSortParameter<TDocument> : IRandomSortParameter<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// True to ascendent order, otherwise false
        /// </summary>
        public bool Ascendent { get; private set; }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        public IRandomSortParameter<TDocument> Configure(bool ascendent)
        {
            this.Ascendent = ascendent;

            return this;
        }
    }
}
