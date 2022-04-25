namespace Consuela.Entity.ProfileParts
{
	public class Ignore
		 : IEquatable<Ignore>
	{
		//TODO: Need to prevent duplicates from being added
		public List<string> Files { get; set; } = new List<string>();

		public List<string> Directories { get; set; } = new List<string>();

		public override bool Equals(object? obj) => Equals(obj as Ignore);

		public bool Equals(Ignore p)
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
			var areEqualFiles = AreDistinctListsEqual(Files, p.Files);

			var areEqualDirectories = AreDistinctListsEqual(Directories, p.Directories);

			var areEqual = areEqualFiles && areEqualDirectories;

			return areEqual;
		}

		//This comparison assumes no duplicates exist
		private bool AreDistinctListsEqual<T>(List<T> primary, List<T> secondary)
		{
			if (primary.Count != secondary.Count) return false;

			foreach (var item in primary)
			{
				if (secondary.Contains(item)) continue;

				return false;
			}

			return true;
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
