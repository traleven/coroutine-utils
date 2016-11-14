using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Scenarios/Demo FX Scenario")]
[System.Serializable]
public class FxScenario : Scenario
{
	public void LockWhileParticleSystemIsPlaying(ParticleSystem ps)
	{
		//AddLock(new WaitWhile(() => ps.isPlaying));
	}
}
