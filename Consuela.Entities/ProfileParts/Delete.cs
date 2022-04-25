namespace Consuela.Entity.ProfileParts
{
	public class Delete
		: IEquatable<Delete>
	{
		/// <summary>How many days old a file is kept until it is deleted.</summary>
		public int FileAgeThreshold { get; set; }

		//TODO: need to make sure this is distinct
		public List<PathAndPattern> Paths { get; set; } = new List<PathAndPattern>();

		public object Schedule { get; set; } //TODO: Need to figure out what this looks like

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

			var areEqualPaths = Paths.AreDistinctListsEqual(p.Paths);

			var areEqual =
				areEqualPaths &&
				FileAgeThreshold == p.FileAgeThreshold;
				//Schedule == p.Schedule; //This has to be left out until the schedule object is defined

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
