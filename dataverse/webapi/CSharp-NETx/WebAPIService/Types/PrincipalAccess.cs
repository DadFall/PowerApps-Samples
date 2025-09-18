using Newtonsoft.Json.Linq;

namespace PowerApps.Samples.Types
{
    /// <summary>
        /// Contains access rights information for the security principal (user or team).
        /// </summary>
    public class PrincipalAccess
    {
        /// <summary>
        /// 获取或设置 the access rights of the security principal (user or team).
        /// </summary>
        public AccessRights AccessMask { get; set; }

        /// <summary>
        /// 获取或设置 the security principal (user or team).
        /// </summary>
        public JObject? Principal { get; set; }
    }
}
