using UnityEngine;

public class Trap_Spike : MonoBehaviour
{
	public int damage;
	Snake snake;
	bool collidedWithSnake;

	void OnEnable()
	{
		snake = Snake.i;
	}

    void OnTriggerEnter2D(Collider2D other) 
	{
		//If this the first eneter of spike collide with player 
		if(other.CompareTag("Player") && !collidedWithSnake)
		{
			//Hurt the player
			snake.Hurt(damage);
			collidedWithSnake = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) 
	{
		//If exit player collider 
		if(other.CompareTag("Player"))
		{
			collidedWithSnake = false;
		}
		
	}
}