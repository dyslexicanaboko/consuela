using Consuela.Entity.ProfileParts;

namespace Consuela.Entity
{
	public interface IProfile
	{
		Ignore Ignore { get; set; }

		Logging Logging { get; set; }

		Delete Delete { get; set; }
	}
}
