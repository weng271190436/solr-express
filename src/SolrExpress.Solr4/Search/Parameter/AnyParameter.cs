﻿using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class AnyParameter<TDocument> : BaseAnyParameter<TDocument>, ISearchParameterExecute<List<string>>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the creation of the parameter
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            container.Add($"{this.Name}={this.Value}");
        }
    }
}
