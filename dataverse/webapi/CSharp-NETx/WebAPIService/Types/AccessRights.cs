using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PowerApps.Samples.Types
{
    /// <summary>
        /// Contains the possible access rights for a user.
        /// </summary>
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    [Flags]
    public enum AccessRights
    {
        /// <summary>
        /// 没有access.
        /// </summary>
        None = 0,
        /// <summary>
        /// right to read the specified type of record.
        /// </summary>
        ReadAccess = 1,
        /// <summary>
        /// right to update the specified record.
        /// </summary>
        WriteAccess = 2,
        /// <summary>
        /// right to append the specified record to another object.
        /// </summary>
        AppendAccess = 4,
        /// <summary>
        /// right to append another record to the specified object.
        /// </summary>
        AppendToAccess = 16,
        /// <summary>
        /// right to create a record.
        /// </summary>
        CreateAccess = 32,
        /// <summary>
        /// right to delete the specified record.
        /// </summary>
        DeleteAccess = 65536,
        /// <summary>
        /// right to share the specified record.
        /// </summary>
        ShareAccess = 262144,
        /// <summary>
        /// right to assign the specified record to another user or team.
        /// </summary>
        AssignAccess = 524288
    }
}
