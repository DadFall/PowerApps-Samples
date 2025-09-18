namespace PowerPlatform.Dataverse.CodeSamples
{
    public static class Settings
    {
        /// <summary>
        /// number of records to create
        /// </summary>
        public const int NumberOfRecords = 1000;

        /// <summary>
        /// maximum number of records to create with $batch
        /// </summary>
        public const short BatchSize = 100; //Must not exceed 1000
    }
}
