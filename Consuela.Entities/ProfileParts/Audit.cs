namespace Consuela.Entity.ProfileParts
{
	public class Audit
		: IEquatable<Audit>
	{
		/// <summary>Set to false by default, Disable logging.</summary>
		public bool Disable { get; set; }

		/// <summary>The path to where rolling log files are stored. By default stored where the executable is located.</summary>
		public string Path { get; set; }

		/// <summary>The number of days to keep the rolling log files.</summary>
		public int RetentionDays { get; set; }

		public override bool Equals(object? obj) => Equals(obj as Audit);

		public bool Equals(Audit? p)
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
				Disable == p.Disable &&
				Path == p.Path &&
				RetentionDays == p.RetentionDays;

			return areEqual;
		}

		public override int GetHashCode() => (Disable, Path, RetentionDays).GetHashCode();

		public static bool operator ==(Audit lhs, Audit rhs)
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

		public static bool operator !=(Audit lhs, Audit rhs) => !(lhs == rhs);
	}
}
