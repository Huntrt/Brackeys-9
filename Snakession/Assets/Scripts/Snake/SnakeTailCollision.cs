using UnityEngine;

public class SnakeTailCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) 
	{
		//Begin eating tail if collide with the snake's head
		if(other.collider.CompareTag("Player")) Snake.i.body.EatingTail();
	}
}
