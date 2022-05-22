namespace Consuela.Entity.ProfileParts
{
	/// <summary>Schedule for when the clean up should happen.</summary>
	public class Schedule
		: IEquatable<Schedule>
	{
		/// <summary>
		/// 05/21/2022 - For now I am only going to support three options which all start on the first of that type.
		/// Every day
		/// Every Sunday of the week
		/// Every first of the month
		/// </summary>
		public ScheduleFrequency Frequency { get; set; } = ScheduleFrequency.Monthly;

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
			return Frequency == p.Frequency;
		}

		public override int GetHashCode() => (Frequency).GetHashCode();

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
