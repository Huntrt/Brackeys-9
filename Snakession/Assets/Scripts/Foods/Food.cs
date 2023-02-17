using UnityEngine;

public class Food : MonoBehaviour
{
	public bool isVegan;
    public int feed;
	public float fresh;
	[HideInInspector] public Vector2 spawnCoord;

	void OnEnable()
	{
		//Increase additive fresh food time
		fresh += Snake.i.mod.freshAdditive.GetAdditive(fresh);
		//Increase bonus fresh food time
		fresh += Snake.i.mod.freshBonus.GetBonus();
		//Automaticly pluck this food when it no longer fresh
		Invoke("Pluck", fresh);
	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		if(other.collider.CompareTag("Player"))
		{
			//Snake eat this food
			Snake.i.Eat(feed, isVegan);
			//Food has been consumed
			Consumed();
			//Pluck the food
			Pluck();
		}
	}

	protected virtual void Consumed() {}

	protected virtual void Pluck()
	{
		//Pluck object where this food got spawn (also destroy)
		Map.i.PluckObject(spawnCoord);
	}
}