namespace Consuela.Entity.ProfileParts
{
    public class Delete
    {
        public int FileAgeThreshold { get; set; }

        public List<PathAndPattern> Paths { get; set; } = new List<PathAndPattern>();

        public object Schedule { get; set; } //TODO: Need to figure out what this looks like
    }
}
