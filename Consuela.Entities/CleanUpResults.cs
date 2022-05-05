namespace Consuela.Entity
{
	public class CleanUpResults
		: IEquatable<CleanUpResults>
	{
		public List<FileInfoEntity> FilesDeleted { get; set; } = new List<FileInfoEntity>();

		public List<FileInfoEntity> FilesIgnored { get; set; } = new List<FileInfoEntity>();
		
		public Dictionary<FileInfoEntity, Exception> FileDeleteErrors { get; set; } = new Dictionary<FileInfoEntity, Exception>();

		public List<string> DirectoriesDeleted { get; set; } = new List<string>();
		
		public Dictionary<string, Exception> DirectoryDeleteErrors { get; set; } = new Dictionary<string, Exception>();

		public override bool Equals(object? obj) => Equals(obj as CleanUpResults);

		public bool Equals(CleanUpResults? p)
		{
			if (p is null)
			{
				return false;
			}

			// Optimization for a common success case.
			if (ReferenceEquals(this, p))
			{
				return true;
			}

			// If run-time types are not exactly the same, return false.
			if (GetType() != p.GetType())
			{
				return false;
			}

			//Perform bubble search loops
			var areEqualFilesDeleted = FilesDeleted.AreDistinctListsEqual(p.FilesDeleted);

			var areEqualFilesIgnored = FilesIgnored.AreDistinctListsEqual(p.FilesIgnored);
			
			var areEqualDirectoriesDeleted = DirectoriesDeleted.AreDistinctListsEqual(p.DirectoriesDeleted);

			var areEqualFileDeleteErrors = FileDeleteErrors.AreDictionarKeysEqual(p.FileDeleteErrors);
			
			var areEqualDirectoryDeleteErrors = DirectoryDeleteErrors.AreDictionarKeysEqual(p.DirectoryDeleteErrors);

			var areEqual = 
				areEqualFilesDeleted && 
				areEqualFilesIgnored &&
				areEqualDirectoriesDeleted &&
				areEqualFileDeleteErrors &&
				areEqualDirectoryDeleteErrors;

			return areEqual;
		}

		public override int GetHashCode() => (
			FilesDeleted, 
			FilesIgnored, 
			FileDeleteErrors, 
			DirectoriesDeleted, 
			DirectoryDeleteErrors).GetHashCode();

		public static bool operator ==(CleanUpResults lhs, CleanUpResults rhs)
		{
			if (lhs is null)
			{
				if (rhs is null)
				{
					return true;
				}

				// Only the left side is null.
				return false;
			}

			// Equals handles case of null on right side.
			return lhs.Equals(rhs);
		}

		public static bool operator !=(CleanUpResults lhs, CleanUpResults rhs) => !(lhs == rhs);
	}
}
