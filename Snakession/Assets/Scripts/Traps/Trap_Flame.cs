using UnityEngine;

public class Trap_Flame : MonoBehaviour
{
	public int damage;
	public float tickRate; float tickRateTimer;

	void OnTriggerStay2D(Collider2D other) 
	{
		//If the player stay on the trap
		if(other.CompareTag("Player"))
		{
			//Timing tick rate
			tickRateTimer += Time.deltaTime;
			//If has count to tick rate
			if(tickRateTimer >= tickRate)
			{
				//Deal damage to snake
				Snake.i.Hurt(damage);
				//Reset tick rate
				tickRateTimer -= tickRateTimer;
			}
		}
	}
}