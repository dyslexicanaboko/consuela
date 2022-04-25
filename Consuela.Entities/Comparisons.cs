namespace Consuela.Entity
{
	internal static class Comparisons
	{
		//This comparison assumes no duplicates exist
		internal static bool AreDistinctListsEqual<T>(this List<T> primary, List<T> secondary)
		{
			if (primary.Count != secondary.Count) return false;

			foreach (var item in primary)
			{
				if (secondary.Contains(item)) continue;

				return false;
			}

			return true;
		}
	}
}
