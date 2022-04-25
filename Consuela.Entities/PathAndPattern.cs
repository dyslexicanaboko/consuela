namespace Consuela.Entity
{
	public class PathAndPattern
		: IEquatable<PathAndPattern>
	{
		public PathAndPattern()
		{ 
			//For unit testing only
		}

		public PathAndPattern(string path, string pattern)
		{
			Path = System.IO.Path.GetDirectoryName(path);
			Pattern = pattern;
		}

		/// <summary>The path to search for files and folders to delete using the provided pattern.</summary>
		public string Path { get; set; }

		/// <summary>The wild card file pattern to use for finding files to delete in the provided path.</summary>
		public string Pattern { get; set; }

		public override bool Equals(object? obj) => Equals(obj as PathAndPattern);

		public bool Equals(PathAndPattern? p)
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
				Path == p.Path &&
				Pattern == p.Pattern;

			return areEqual;
		}

		public override int GetHashCode() => (Path, Pattern).GetHashCode();

		public static bool operator ==(PathAndPattern lhs, PathAndPattern rhs)
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

		public static bool operator !=(PathAndPattern lhs, PathAndPattern rhs) => !(lhs == rhs);
	}
}
