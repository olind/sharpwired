using System;
using System.Collections.Generic;
using System.Text;

namespace SharpWired.Utils
{
	/// <summary>
	/// A Generic Pair class.
	/// </summary>
	/// <typeparam name="T">The first type</typeparam>
	/// <typeparam name="S">The second type.</typeparam>
	class Pair<T, S>
	{
		private T mKey;

		/// <summary>
		/// The first object in the pair.
		/// </summary>
		public T Key
		{
			get { return mKey; }
			set { mKey = value; }
		}

		private S mValue;

		/// <summary>
		/// The second object in the pair.
		/// </summary>
		public S Value
		{
			get { return mValue; }
			set { mValue = value; }
		}

		/// <summary>
		/// Set the two in the pair.
		/// </summary>
		/// <param name="pKey"></param>
		/// <param name="sValue"></param>
		public Pair(T pKey, S sValue)
		{
			mKey = pKey;
			mValue = sValue;
		}

		/// <summary>
		/// Empty. Key and Value gets default(T) and default(S) values.
		/// </summary>
		public Pair()
		{
		}
	}
}