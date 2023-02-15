using UnityEngine;

public class Foods : MonoBehaviour
{
    public int feed;
	public float fresh;
	public Vector2 spawnCoord;
	public bool isVegan;

	void OnEnable()
	{
		//Automaticly pluck this food when it no longer fresh
		Invoke("Pluck", fresh);
	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		if(other.collider.CompareTag("Player"))
		{
			//Snake eat this food
			Snake.i.Eat(feed, isVegan);
			//Pluck the food
			Pluck();
		}
	}

	protected virtual void Pluck()
	{
		//Pluck object where this food got spawn (also destroy)
		Map.i.PluckObject(spawnCoord);
	}
}