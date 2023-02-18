using UnityEngine;

public class Food : MonoBehaviour
{
	public bool isVegan;
    public int feed;
	public float fresh;
	[SerializeField] float randomRotation;
	[HideInInspector] public Vector2 spawnCoord;
	public System.Action onConsume;

	void OnEnable()
	{
		//Randomize the rotation when this food got spawn
		transform.localRotation = Quaternion.Euler(0,0, Random.Range(0, randomRotation));
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
			onConsume?.Invoke();
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