namespace PowerPlatform.Dataverse.CodeSamples
{
    public static class Settings
    {
        /// <summary>
        /// number of records to create for all samples in this solution.
        /// </summary>
        public const int NumberOfRecords = 1000;

        /// <summary>
        /// maximum number of records operations to send with 
        /// CreateMultiple, UpdateMultiple and DeleteMultiple.
        /// </summary>
        public const short BatchSize = 100;
    }
}
