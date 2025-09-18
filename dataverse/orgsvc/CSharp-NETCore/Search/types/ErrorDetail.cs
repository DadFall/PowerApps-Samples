using System.Runtime.Serialization;

namespace PowerApps.Samples.Search.Types;

/// <summary>
        /// error detail returned as part of response.
        /// </summary>
public sealed class ErrorDetail
{
    /// <summary>
        /// 获取或设置 the error code.
        /// </summary>
    [DataMember(Name = "code")]
    public string Code { get; set; }

    /// <summary>
        /// 获取或设置 the error message.
        /// </summary>
    [DataMember(Name = "message")]
    public string Message { get; set; }

    /// <summary>
        /// 获取或设置 additional error information.
        /// </summary>
    [DataMember(Name = "propertybag")]
    public Dictionary<string, object> PropertyBag { get; set; }
}