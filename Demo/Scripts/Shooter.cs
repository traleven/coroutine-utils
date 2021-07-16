using UnityEngine;

namespace traleven.CoroutineUtils.Demo
{
	public class Shooter : MonoBehaviour
	{
	#pragma warning disable IDE0044, CS0649
		[SerializeField]
		private GameObject projectile;
		[SerializeField]
		private ParticleSystem createFX;
		[SerializeField]
		private ParticleSystem explosionPrefab;
	#pragma warning restore IDE0044, CS0649
		
		public void Shoot()
		{
			StartCoroutine(
				new System.Action(createFX.Play)
				.Then ( new WaitForSeconds( createFX.main.duration ) )
				.Then ( ShootOnce )
				.Then ( new WaitForSeconds( 1.5f ) )
				.Then ( Shoot )
			);
		}
		public void ShootOnce()
		{
			GameObject o = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
			Projectile p = o.GetComponent<Projectile>();
			Rigidbody r = o.GetComponent<Rigidbody>();
			r.AddForce(Random.Range(3f, 5f), Random.Range(-0.3f, 0.3f), 0f, ForceMode.Impulse);
			p.StartCoroutine(
				new WaitForSeconds(3f)
				.Then( () => 
				{
					ParticleSystem explosion = (ParticleSystem)Instantiate(explosionPrefab, p.transform.position, p.transform.rotation);
					StartCoroutine(
						new WaitForSeconds( explosion.main.duration )
						.Then( () => { Destroy(explosion.gameObject); } )
					);
				})
				.Then( () => { Destroy(p.gameObject); } )
			);
		}
	}
}
