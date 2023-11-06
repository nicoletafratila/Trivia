namespace EventBus.Contracts.Events
{
    public record EmailMetadata
    {
        public List<string>? ToAddresses { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? AttachmentPath { get; set; }

        public EmailMetadata()
        { }

        public EmailMetadata(List<string>? toAddresses, string? subject, string? body = "", string? attachmentPath = "")
        {
            ToAddresses = toAddresses;
            Subject = subject;
            Body = body;
            AttachmentPath = attachmentPath;
        }
    }
}