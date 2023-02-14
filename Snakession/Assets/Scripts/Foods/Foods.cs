using UnityEngine;

public class Foods : MonoBehaviour
{
    public int feed;
	public Vector2 spawnCoord;

	void OnCollisionEnter2D(Collision2D other) 
	{
		if(other.collider.CompareTag("Player"))
		{
			other.collider.GetComponent<Snake>().Eat(feed);
			Consume();
		}
	}

	protected virtual void Consume()
	{
		//Pluck object where this food got spawn (also destroy)
		Map.i.PluckObject(spawnCoord);
	}
}