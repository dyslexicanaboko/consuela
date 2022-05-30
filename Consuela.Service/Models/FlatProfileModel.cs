using Consuela.Entity;

namespace Consuela.Service.Models
{
    public class FlatProfileModel
    {
        public List<string> IgnoreFiles { get; set; }

        public List<string> IgnoreDirectories { get; set; }

        public bool AuditDisable { get; set; }

        public string AuditPath { get; set; }

        public int AuditRetentionDays { get; set; }

        public int DeleteFileAgeThreshold { get; set; }

        public ScheduleFrequency DeleteFrequency { get; set; }

        private List<PathAndPattern> DeletePaths { get; set; }
    }
}
