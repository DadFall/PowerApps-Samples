namespace PowerApps.Samples.Messages
{
    // 此class must be instantiated by either:
    // - The Service.SendAsync<T> method
    // - The HttpResponseMessage.As<T> extension in Extensions.cs

    /// <summary>
        /// Contains the response from the UpsertRequest
        /// </summary>
    public sealed class UpsertResponse : HttpResponseMessage
    {
        /// <summary>
        /// 一个reference to the record.
        /// </summary>
        public EntityReference? EntityReference => new EntityReference(
                        uri: Headers.GetValues("OData-EntityId").FirstOrDefault());
    }
}
