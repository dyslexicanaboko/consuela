namespace Consuela.Entity
{
    public class AuditRow
    {
        public string FileType { get; set; }
        
        public DateTime? CreationTime { get; set; }
        
        public string Path { get; set; }
        
        public string? Filename { get; set; }
    }
}
