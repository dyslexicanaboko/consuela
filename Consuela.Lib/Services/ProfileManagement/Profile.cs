using Consuela.Entity;
using Consuela.Entity.ProfileParts;
using System;

namespace Consuela.Lib.Services.ProfileManagement
{
	/// <summary>
	/// Watches the properties of the <see cref="IProfile"/> that is loaded into memory to determine when to save changes.
	/// </summary>
	public class Profile
		: IProfile, IEquatable<Profile>
	{
		/// <inheritdoc/>
		public Ignore Ignore { get; set; } = new Ignore();

		/// <inheritdoc/>
		public Audit Audit { get; set; } = new Audit();

		/// <inheritdoc/>
		public Delete Delete { get; set; } = new Delete();

		public override bool Equals(object? obj) => Equals(obj as Profile);

		public bool Equals(Profile? p)
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

			var areEqualIgnore = Ignore == p.Ignore;
			var areEqualDelete = Delete == p.Delete;
			var areEqualLogging = Audit == p.Audit;
				 
			var areEqual = areEqualIgnore && areEqualDelete && areEqualLogging;

			return areEqual;
		}

		public override int GetHashCode() => (Ignore, Audit, Delete).GetHashCode();

		public static bool operator ==(Profile lhs, Profile rhs)
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

		public static bool operator !=(Profile lhs, Profile rhs) => !(lhs == rhs);
	}
}
