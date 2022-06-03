namespace Consuela.Entity.ProfileParts
{
	public class Delete
		: IEquatable<Delete>
	{
		/// <summary>How many days old a file is kept until it is deleted.</summary>
		public int FileAgeThreshold { get; set; }

		private List<PathAndPattern> _paths = new List<PathAndPattern>();

		public IReadOnlyList<PathAndPattern> Paths => _paths;

		public Schedule Schedule { get; set; } = new Schedule();

		public void AddPath(PathAndPattern path)
		{
			if(_paths.Contains(path)) return;

			_paths.Add(path);
		}

		public void AddPath(IEnumerable<PathAndPattern> paths)
		{
			foreach (var path in paths)
			{
				AddPath(path);
			}
		}

		public override bool Equals(object? obj) => Equals(obj as Delete);

		public bool Equals(Delete? p)
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

			var areEqualPaths = _paths.AreDistinctListsEqual(p.Paths);

			var areEqual =
				areEqualPaths &&
				FileAgeThreshold == p.FileAgeThreshold &&
				Schedule == p.Schedule;

			return areEqual;
		}

		public override int GetHashCode() => (FileAgeThreshold, Schedule, Paths).GetHashCode();

		public static bool operator ==(Delete lhs, Delete rhs)
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

		public static bool operator !=(Delete lhs, Delete rhs) => !(lhs == rhs);
	}
}
