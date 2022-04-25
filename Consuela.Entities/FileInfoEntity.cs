namespace Consuela.Entity
{
    /// <summary>
    /// Mirror of the parts needed from the <see cref="FileInfo"/> object without being bound to it.
    /// </summary>
    public class FileInfoEntity
    {
        public FileInfoEntity()
        {
            
        }

        public FileInfoEntity(FileInfo fileInfo)
        {
            Name = fileInfo.Name;
            FullName = fileInfo.FullName;
            DirectoryName = fileInfo.DirectoryName;
            CreationTime = fileInfo.CreationTime;
        }

        public string Name { get; set; }
        
        public string FullName { get; set; }

        public string DirectoryName { get; set; }
        
        public DateTime CreationTime { get; set; }
    }
}
