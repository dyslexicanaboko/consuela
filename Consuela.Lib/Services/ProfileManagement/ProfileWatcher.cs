using Consuela.Entity;
using Consuela.Entity.ProfileParts;
using System;

namespace Consuela.Lib.Services.ProfileManagement
{
	/// <summary>
	/// Watches the properties of the <see cref="IProfile"/> that is loaded into memory to determine when to save changes.
	/// </summary>
	public class ProfileWatcher
		: IProfile, IEquatable<ProfileWatcher>
	{
		public delegate void SaveHandler(object sender, EventArgs e);
		
		public event SaveHandler Save;

		/// <inheritdoc/>
		public Ignore Ignore { get; set; } = new Ignore();

		/// <inheritdoc/>
		public Logging Logging { get; set; } = new Logging();

		/// <inheritdoc/>
		public Delete Delete { get; set; } = new Delete();

		private void RaiseSaveEvent() => Save?.Invoke(this, new EventArgs());

		public override bool Equals(object? obj) => Equals(obj as ProfileWatcher);

		public bool Equals(ProfileWatcher? p)
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
			var areEqualLogging = Logging == p.Logging;
				 
			var areEqual = areEqualIgnore && areEqualDelete && areEqualLogging;

			return areEqual;
		}

		public override int GetHashCode() => (Ignore, Logging, Delete).GetHashCode();

		public static bool operator ==(ProfileWatcher lhs, ProfileWatcher rhs)
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

		public static bool operator !=(ProfileWatcher lhs, ProfileWatcher rhs) => !(lhs == rhs);
	}
}
