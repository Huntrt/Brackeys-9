using UnityEngine;

public class Foods : MonoBehaviour
{
    public int feed;
	public Vector2 spawnCoord;
	public bool isVegan;

	void OnCollisionEnter2D(Collision2D other) 
	{
		if(other.collider.CompareTag("Player"))
		{
			Snake.i.Eat(feed);
			Consumed();
		}
	}

	protected virtual void Consumed()
	{
		//Pluck object where this food got spawn (also destroy)
		Map.i.PluckObject(spawnCoord);
	}
}