namespace Consuela.Entity.ProfileParts
{
	/// <summary>Schedule for when the clean up should happen.</summary>
	public class Schedule
		: IEquatable<Schedule>
	{
		//TODO: Give this class meaning
		//I don't know how this class will work yet. Place holder.
		//Model it after windows task manager for now unless there is a better idea. Could use Hangfire.io.
		public int TheNumberSeven { get; set; } = 7;

		public override bool Equals(object? obj) => Equals(obj as Schedule);

		public bool Equals(Schedule? p)
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

			//This is just dummy code for now.
			return TheNumberSeven == p.TheNumberSeven;
		}

		public override int GetHashCode() => (TheNumberSeven).GetHashCode();

		public static bool operator ==(Schedule lhs, Schedule rhs)
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

		public static bool operator !=(Schedule lhs, Schedule rhs) => !(lhs == rhs);
	}
}
