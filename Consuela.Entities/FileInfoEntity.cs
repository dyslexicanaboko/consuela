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

		/// <summary>
		/// Clone an existing <see cref="FileInfoEntity"/> object.
		/// </summary>
		/// <param name="fileInfoEntity">Object to clone.</param>
		public FileInfoEntity(FileInfoEntity fileInfoEntity)
		{
			Name = fileInfoEntity.Name;
			FullName = fileInfoEntity.FullName;
			DirectoryName = fileInfoEntity.DirectoryName;
			CreationTime = fileInfoEntity.CreationTime;
		}

		/// <summary>
		/// Converting a full file path into the separate components of this class using the <see cref="Path"/> class's methods for parsing.
		/// </summary>
		/// <param name="fullFilePath">Full file path</param>
		/// <param name="creationTime">When the file was supposedly created</param>
		public FileInfoEntity(string fullFilePath, DateTime creationTime)
		{
			Name = Path.GetFileName(fullFilePath);
			FullName = fullFilePath;
			DirectoryName = Path.GetDirectoryName(fullFilePath);
			CreationTime = creationTime;
		}

		/// <summary>
		/// Extracting only what this class requires from an existing <see cref="FileInfo"/> object.
		/// </summary>
		/// <param name="fileInfo">Target file info object.</param>
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

		public FileInfoEntity Clone() => new FileInfoEntity(this);
	}
}
