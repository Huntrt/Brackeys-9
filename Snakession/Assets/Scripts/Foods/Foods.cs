using UnityEngine;
using System;

public class Foods : MonoBehaviour
{
    public int feed;
	public Action onConsume;

	void OnCollisionEnter2D(Collision2D other) 
	{
		if(other.collider.CompareTag("Player"))
		{
			other.collider.GetComponent<Snake>().Eat(feed);
			Consume();
		}
	}

	public void Consume()
	{
		onConsume?.Invoke();
		Destroy(gameObject);
	}
}
