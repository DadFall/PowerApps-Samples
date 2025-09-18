namespace PowerApps.Samples.Metadata.Messages
{
    // 此class must be instantiated by either:
    // - The Service.SendAsync<T> method
    // - The HttpResponseMessage.As<T> extension in Extensions.cs

    /// <summary>
        /// Contains the response to the CreateEntityRequest
        /// </summary>
    public sealed class CreateEntityResponse : HttpResponseMessage
    {
        /// <summary>
        /// 一个reference to the table created
        /// </summary>
        public EntityReference TableReference => new(uri: Headers.GetValues("OData-EntityId").FirstOrDefault());
    }
}
