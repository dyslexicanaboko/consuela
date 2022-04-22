using System.Collections.Generic;

namespace Consuela.Lib
{
	public class Profile
	{
		public List<string> WhiteListFiles { get; set; } = new List<string>();
		public List<string> WhiteListDirectories { get; set; } = new List<string>();
		public List<PathAndPattern> SearchPaths { get; set; } = new List<PathAndPattern>();
	}
}
