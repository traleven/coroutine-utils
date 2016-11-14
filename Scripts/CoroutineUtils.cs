using UnityEngine;
using System.Collections;
using System;
using JetBrains.Annotations;

public static class CoroutineUtils
{
	#region Then
	#region IEnumerator
	public	static	IEnumerator Then			( [NotNull] this IEnumerator predecessor, [NotNull] IEnumerator successor )
	{
		while( predecessor.MoveNext(  ) )
		{
			yield return predecessor.Current;
		}
		while( successor.MoveNext(  ) )
		{
			yield return successor.Current;
		}
	}
	public	static	IEnumerator Then			( [NotNull] this IEnumerator predecessor, [NotNull] Action successor )
	{
		while( predecessor.MoveNext(  ) )
		{
			yield return predecessor.Current;
		}
		successor.Invoke(  );
	}
	public	static	IEnumerator	Then			( [NotNull] this IEnumerator predecessor, [CanBeNull] YieldInstruction successor )
	{
		while( predecessor.MoveNext(  ) )
		{
			yield return predecessor.Current;
		}
		yield return successor;
	}
	#endregion
	
	#region Action
	public	static	IEnumerator Then			( [NotNull] this Action predecessor, [NotNull] IEnumerator successor )
	{
		predecessor.Invoke(  );
		while( successor.MoveNext(  ) )
		{
			yield return successor.Current;
		}
	}
	public	static	Action		Then			( [NotNull] this Action	predecessor, [NotNull] Action successor )
	{
		return delegate
		{
			predecessor.Invoke();
			successor.Invoke();
		};
	}
	public	static	IEnumerator	Then			( [NotNull] this Action predecessor, [CanBeNull] YieldInstruction successor )
	{
		predecessor.Invoke();
		yield return successor;
	}
	#endregion
	
	#region YieldInstruction
	public	static	IEnumerator	Then			( [CanBeNull] this YieldInstruction predecessor, [NotNull] IEnumerator successor )
	{
		yield return predecessor;
		while ( successor.MoveNext(  ) )
		{
			yield return successor.Current;
		}
	}
	public	static	IEnumerator	Then			( [CanBeNull] this YieldInstruction predecessor, [NotNull] Action successor )
	{
		yield return predecessor;
		successor.Invoke();
	}
	public	static	IEnumerator	Then			( [CanBeNull] this YieldInstruction predecessor, [CanBeNull] YieldInstruction successor )
	{
		yield return predecessor;
		yield return successor;
	}
	#endregion
	#endregion
	
	#region ThenCoroutine
	public	static	IEnumerator	ThenCoroutine	( [NotNull] this IEnumerator predecessor, [NotNull] MonoBehaviour owner, [NotNull] IEnumerator coroutine )
	{
		while ( predecessor.MoveNext() )
		{
			yield return predecessor.Current;
		}
		yield return owner.StartCoroutine(coroutine);
	}
	public	static	IEnumerator	ThenCoroutine	( [CanBeNull] this YieldInstruction predecessor, [NotNull] MonoBehaviour owner, [NotNull] IEnumerator coroutine )
	{
		yield return predecessor;
		yield return owner.StartCoroutine(coroutine);
	}
	#endregion
}
