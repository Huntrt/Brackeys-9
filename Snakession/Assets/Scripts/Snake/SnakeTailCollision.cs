using UnityEngine;

public class SnakeTailCollision : MonoBehaviour
{
	[SerializeField] ParticleSystem tailAppearEffect;

    void OnCollisionEnter2D(Collision2D other) 
	{
		//Begin eating tail if collide with the snake's head
		if(other.collider.CompareTag("Player")) Snake.i.body.EatingTail();
	}

	void OnEnable()
	{
		tailAppearEffect.Play();
	}
}