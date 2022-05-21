namespace Consuela.Entity
{
	internal static class Comparisons
	{
		//TODO: Make a version that can compare lists that are not distinct
		//This comparison assumes no duplicates exist
		internal static bool AreDistinctListsEqual<T>(this IReadOnlyList<T> primary, IReadOnlyList<T> secondary)
			where T : IEquatable<T>
		{
			if(primary == null && secondary == null) return true;
			
			if (primary == null ^ secondary == null) return false;

			if (primary.Count != secondary.Count) return false;

			foreach (var item in primary)
			{
				if (secondary.Contains(item)) continue;

				return false;
			}

			return true;
		}

		//TODO: How to make the warnings stop?
		/// <summary>
		/// Fully compares each key and value. The types must be comparable.
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="primary"></param>
		/// <param name="secondary"></param>
		/// <returns></returns>
		internal static bool AreDictionariesEqual<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> primary, IReadOnlyDictionary<TKey, TValue> secondary)
			where TKey : IEquatable<TKey>
			where TValue : IEquatable<TValue>
		{
			if (primary == null && secondary == null) return true;

			if (primary == null ^ secondary == null) return false;

			if (primary.Count != secondary.Count) return false;

			foreach (var (pKey, pValue) in primary)
			{
				//If the key is not found then it is an instant fail
				if(!secondary.TryGetValue(pKey, out var sValue)) return false;
				
				//If the values are equal then it is a pass
				if (Equals(pValue, sValue)) continue;

				return false;
			}

			return true;
		}

		/// <summary>
		/// Only compares each key. The Key's type must be comparable. Use this when the value cannot be compared.
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="primary"></param>
		/// <param name="secondary"></param>
		/// <returns></returns>
		internal static bool AreDictionarKeysEqual<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> primary, IReadOnlyDictionary<TKey, TValue> secondary)
			where TKey : IEquatable<TKey>
		{
			if (primary == null && secondary == null) return true;

			if (primary == null ^ secondary == null) return false;

			if (primary.Count != secondary.Count) return false;

			foreach (var (pKey, _) in primary)
			{
				//If the key is not found then it is an instant fail
				if (secondary.ContainsKey(pKey)) continue;

				//In some cases TValue cannot be compared, such as System.Exception.

				return false;
			}

			return true;
		}
	}
}
