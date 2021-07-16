using UnityEngine;
using UnityEngine.UI;

namespace traleven.CoroutineUtils.Demo
{
	[AddComponentMenu("Scenarios/Demo FX Scenario")]
	[System.Serializable]
	public class FxScenario : Scenario
	{
		public void LockWhileParticleSystemIsPlaying(ParticleSystem ps)
		{
			//AddLock(new WaitWhile(() => ps.isPlaying));
		}
	}
}
