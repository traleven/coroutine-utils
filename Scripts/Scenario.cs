using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Scenarios/Scenario")]
[System.Serializable]
public class Scenario : MonoBehaviour
{
	[System.Serializable]
	public class ScenarioEvent : UnityEvent {}
	
	[SerializeField]
	private ScenarioEvent[] events = new ScenarioEvent[0];
	private Queue<object>	locks = new Queue<object>();
	
	public void Invoke()
	{
		StartCoroutine(RunLoop());
	}
	
	private IEnumerator RunLoop()
	{
		for (int i = 0; i < events.Length; ++i)
		{
			while (locks.Count > 0)
			{
				yield return locks.Dequeue();
			}
			events[i].Invoke();
		}
	}
	protected void AddLock(object lockObject)
	{
		locks.Enqueue(lockObject);
	}
	
	public void LockForSeconds(float seconds)
	{
		AddLock(new WaitForSeconds(seconds));
	}
}
