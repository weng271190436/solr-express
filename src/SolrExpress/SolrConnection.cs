﻿using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using SolrExpress.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace SolrExpress
{
    /// <summary>
    /// SOLR connection
    /// </summary>
    public class SolrConnection : ISolrConnection
    {
        private readonly SolrExpressOptions _options;

        public SolrConnection(SolrExpressOptions options)
        {
            Checker.IsNull(options);

            this._options = options;
        }

        /// <summary>
        /// Set authentication configurations
        /// </summary>
        /// <param name="url">Uri to configure</param>
        private void SetAuthentication(Url url)
        {
            switch (this._options.Security.AuthenticationType)
            {
                case AuthenticationType.Basic:
                    url.WithBasicAuth(this._options.Security.UserName, this._options.Security.Password);
                    break;
            }
        }

        string ISolrConnection.Get(string handler, List<string> data)
        {
            var url = this._options.HostAddress
                .AppendPathSegment(handler);

            if (data?.Any() ?? false)
            {
                url.SetQueryParams(data);
            }

            this.SetAuthentication(url);

            return url
                .GetStringAsync()
                .Result;
        }

        string ISolrConnection.GetX(string handler, object data)
        {
            var url = this._options.HostAddress
                .AppendPathSegment(handler);

            this.SetAuthentication(url);

#if NETCORE
            return url
                .SendJsonAsync(HttpMethod.Get, data)
                .Result
                .Content
                .ReadAsStringAsync()
                .Result;
#else
            return url
                .SetQueryParam("json", JsonConvert.SerializeObject(data))
                .GetStringAsync()
                .Result;
#endif
        }

        string ISolrConnection.PostJson(string handler, string data)
        {
            var url = this._options.HostAddress
                .AppendPathSegment(handler);

            this.SetAuthentication(url);

            var body = new StringContent(data);
            body.Headers.Remove("Content-Type");
            body.Headers.Add("Content-Type", "application/json");

            return url
                .SendAsync(HttpMethod.Post, body)
                .ReceiveString()
                .Result;
        }
    }
}
