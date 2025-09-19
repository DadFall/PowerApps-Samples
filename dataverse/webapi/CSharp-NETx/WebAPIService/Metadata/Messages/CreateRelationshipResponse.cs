namespace PowerApps.Samples.Metadata.Messages
{
    // 此class must be instantiated by either:
    // - The Service.SendAsync<T> method
    // - The HttpResponseMessage.As<T> extension in Extensions.cs

    /// <summary>
        /// Contains the response to the CreateRelationshipRequest
        /// </summary>
    public sealed class CreateRelationshipResponse : HttpResponseMessage
    {
        /// <summary>
        /// 一个reference to the relationship created.
        /// </summary>
        public EntityReference RelationshipReference => new(Headers.GetValues("OData-EntityId").FirstOrDefault());
    }
}
