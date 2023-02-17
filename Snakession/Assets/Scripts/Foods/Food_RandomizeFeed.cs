using UnityEngine;

public class Food_RandomizeFeed : MonoBehaviour
{
	[SerializeField] Food foodCore;
	[SerializeField] int minFeed, maxFeed;

	void OnValidate() {foodCore = GetComponent<Food>();}
	void OnEnable() {foodCore.onConsume += BeginRandomize;}
	void OnDisable() {foodCore.onConsume -= BeginRandomize;}

	void BeginRandomize()
	{
		//Randmize the min and max amount could feed
		Snake.i.Eat(Random.Range(minFeed, maxFeed+1), foodCore.isVegan);
	}
}
