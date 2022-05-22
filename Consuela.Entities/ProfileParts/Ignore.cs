namespace Consuela.Entity.ProfileParts
{
	public class Ignore
		 : IEquatable<Ignore>
	{
		private List<string> _files = new List<string>();

		/// <summary>The files to ignore during clean up</summary>
		public IReadOnlyList<string> Files => _files;

		private List<string> _directories = new List<string>();

		/// <summary>The directories to ignore during clean up</summary>
		public IReadOnlyList<string> Directories => _directories;

		public void AddFile(string file)
		{
			if (_files.Contains(file)) return;

			_files.Add(file);
		}

		public void AddDirectory(string directory)
		{
			if (_directories.Contains(directory)) return;

			_directories.Add(directory);
		}

		public override bool Equals(object? obj) => Equals(obj as Ignore);

		public bool Equals(Ignore? p)
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
			var areEqualFiles = Files.AreDistinctListsEqual(p.Files);

			var areEqualDirectories = Directories.AreDistinctListsEqual(p.Directories);

			var areEqual = areEqualFiles && areEqualDirectories;

			return areEqual;
		}

		public override int GetHashCode() => (Files, Directories).GetHashCode();

		public static bool operator ==(Ignore lhs, Ignore rhs)
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

		public static bool operator !=(Ignore lhs, Ignore rhs) => !(lhs == rhs);
	}
}
