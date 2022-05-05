namespace Consuela.Entity
{
	/// <summary>
	/// Mirror of the parts needed from the <see cref="FileInfo"/> object without being bound to it.
	/// </summary>
	public class FileInfoEntity
		: IEquatable<FileInfoEntity>
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

		/// <summary>File name only</summary>
		public string Name { get; set; }

		/// <summary>Full path, with file name</summary>
		public string FullName { get; set; }

		/// <summary>Full path, no file name.</summary>
		public string DirectoryName { get; set; }
		
		/// <summary>When the file was created</summary>
		public DateTime CreationTime { get; set; }

		public override bool Equals(object? obj) => Equals(obj as FileInfoEntity);

		public bool Equals(FileInfoEntity? p)
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

			var areEqual =
				Name == p.Name &&
				FullName == p.FullName &&
				DirectoryName == p.DirectoryName;

			return areEqual;
		}

		//Not including CreationTime on purpose because it doesn't add to the equality comparison
		public override int GetHashCode() => (Name, FullName, DirectoryName).GetHashCode();

		public static bool operator ==(FileInfoEntity lhs, FileInfoEntity rhs)
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

		public static bool operator !=(FileInfoEntity lhs, FileInfoEntity rhs) => !(lhs == rhs);
	}
}
