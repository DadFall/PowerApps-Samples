using System.Xml.Linq;
using System.Net;
using Newtonsoft.Json.Linq;

namespace PowerApps.Samples.Messages
{
    /// <summary>
        /// Contains the data to execute a query using FetchXml
        /// </summary>
    public sealed class FetchXmlRequest : HttpRequestMessage
    {

        /// <summary>
        /// 初始化 the FetchXmlRequest
        /// </summary>
        /// <param name="entitySetName">名称 of the entity set.</param>
        /// <param name="fetchXml">document containing the fetchXml</param>
        /// <param name="includeAnnotations">Whether annotations should be included in the 响应.</param>
        public FetchXmlRequest(string entitySetName, XDocument fetchXml, bool includeAnnotations = false)
        {
            Method = HttpMethod.Get;
            RequestUri = new Uri(
                uriString: $"{entitySetName}?fetchXml={WebUtility.UrlEncode(fetchXml.ToString())}&$count=true",
                uriKind: UriKind.Relative);
            if (includeAnnotations)
            {
                Headers.Add("Prefer", "odata.include-annotations=\"*\"");
            }
        }
    }
}
