using UnityEngine;

public class Trap_Explosion : MonoBehaviour
{
	public int damage;
	public float radius;
	[SerializeField] ParticleSystem[] explosionEffects;

	public void Explode()
	{
		//Go through all the explosion effect
		for (int e = 0; e < explosionEffects.Length; e++)
		{
			//Play this effect
			explosionEffects[e].Play();
			//Unparent this explosion effect
			explosionEffects[e].transform.SetParent(null);
		}
		//Cast an circle in radius to check if explode hit anyting
		RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero);
		//Go through all the hit
		if(hits.Length > 0) for (int h = 0; h < hits.Length; h++)
		{
			//If hit the snake
			if(hits[h].collider.CompareTag("Player"))
			{
				//Hurt the snake
				Snake.i.Hurt(damage);
			}
		}
		//Destroy the trap
		Destroy(gameObject);
	}
}
