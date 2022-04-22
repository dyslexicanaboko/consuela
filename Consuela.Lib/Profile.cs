using System.Collections.Generic;

namespace Consuela.Lib
{
	public class Profile
	{
		public List<string> IgnoreListFiles { get; set; } = new List<string>();
		public List<string> IgnoreListDirectories { get; set; } = new List<string>();
		public List<PathAndPattern> SearchPaths { get; set; } = new List<PathAndPattern>();
	}
}
