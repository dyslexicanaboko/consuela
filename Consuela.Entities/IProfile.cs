using Consuela.Entity.ProfileParts;

namespace Consuela.Entity
{
	public interface IProfile
	{
		/// <summary>Files and folders to ignore while clean up is happening.</summary>
		Ignore Ignore { get; set; }

		/// <summary>Logging options used to audit clean up operations.</summary>
		Audit Audit { get; set; }

		/// <summary>Files and folders to delete durring clean up. Additional configuration surrounding clean up.</summary>
		Delete Delete { get; set; }
	}
}
