using System.Text;
using System.Text.Json;

namespace PowerApps.Samples.Messages
{
    /// <summary>
        /// Contains the data to set a column value
        /// </summary>
    /// <typeparam name="T">The type of the column value to set</typeparam>
    public sealed class SetColumnValueRequest<T> : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the SetColumnValueRequest
        /// </summary>
        /// <param name="entityReference">一个reference to the record that has the column.</param>
        /// <param name="propertyName">名称 of the column</param>
        /// <param name="value">值 to set</param>
        public SetColumnValueRequest(EntityReference entityReference, string propertyName, T value)
        {
            Method = HttpMethod.Put;
            RequestUri = new Uri(
                uriString: $"{entityReference.Path}/{propertyName}", 
                uriKind: UriKind.Relative);
            Content = new StringContent(
                content: $"{{\"value\": {JsonSerializer.Serialize(value)}}}", 
                encoding: Encoding.UTF8, 
                mediaType: "application/json");
        }
    }
}