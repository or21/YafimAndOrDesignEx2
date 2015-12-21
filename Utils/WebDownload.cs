//-----------------------------------------------------------------------
// <copyright file="WebDownload.cs" company="A16_Ex01">
// Yafim Vodkov 308973882 Or Brand id 302521034
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Net;

namespace Utils
{
    /// <summary>
    /// Extends WebClient class. Add more timeout to the web request.
    /// </summary>
    public class WebDownload : WebClient
    {
        /// <summary>
        /// Gets or sets timeout
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// Initializes a new instance of the WebDownload class.
        /// </summary>
        public WebDownload() : this(60000)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WebDownload class.
        /// Add more time
        /// </summary>
        /// <param name="i_Timeout">Http timeout</param>
        public WebDownload(int i_Timeout)
        {
            this.Timeout = i_Timeout;
        }

        /// <summary>
        /// Extends more time
        /// </summary>
        /// <param name="i_Address">Http path</param>
        /// <returns>the request</returns>
        protected override WebRequest GetWebRequest(Uri i_Address)
        {
            WebRequest request = base.GetWebRequest(i_Address);
            if (request != null)
            {
                request.Timeout = this.Timeout;
            }

            return request;
        }
    }
}