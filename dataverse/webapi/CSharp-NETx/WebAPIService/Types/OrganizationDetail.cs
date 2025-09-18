namespace PowerApps.Samples.Types
{
    public class OrganizationDetail
    {
        /// <summary>
        /// global unique identifier of the organization.
        /// </summary>
        public Guid OrganizationId { get; set; }
        /// <summary>
        /// friendly name of the organization.
        /// </summary>
        public string FriendlyName { get; set; }
        /// <summary>
        /// version of the organization.
        /// </summary>
        public string OrganizationVersion { get; set; }

        // In Default environment the value has 'Default-' appended to to a guid value.
        public string EnvironmentId { get; set; }

        public Guid DatacenterId { get; set; }

        public string Geo { get; set; }

        public string TenantId { get; set; }
        /// <summary>
        /// organization name used in the URL for the organization web service.
        /// </summary>
        public string UrlName { get; set; }
        /// <summary>
        /// unique name of the organization.
        /// </summary>
        public string UniqueName { get; set; }
        /// <summary>
        /// Collection that identifies the service type and address for each endpoint of the organization.
        /// </summary>
        public EndpointCollection Endpoints { get; set; }
        /// <summary>
        /// state of the organization.
        /// </summary>
        public OrganizationState State { get; set; }
    }
}
