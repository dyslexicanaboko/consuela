namespace Consuela.Entity
{
    public class PathAndPattern
    {
        public PathAndPattern(string path, string pattern)
        {
            Path = System.IO.Path.GetDirectoryName(path);
            Pattern = pattern;
        }

        public string Path { get; set; }

        public string Pattern { get; set; }
    }
}
