namespace FleetManagement.EF.Storage
{
    public class DocumentContentResult
    {
        public byte[] Content { get; set; } = default!;
        public string FileName { get; set; } = default!;
        public string ContentType { get; set; } = default!;
    }
}