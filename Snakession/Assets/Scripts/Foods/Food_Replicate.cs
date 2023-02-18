using UnityEngine;

public class Food_Replicate : MonoBehaviour
{
	[SerializeField] Food foodCore;
	public int replicateAmount;
	public float replicateChance;
	
	void OnValidate() {foodCore = GetComponent<Food>();}
	void OnEnable() {foodCore.onConsume += BeginReplicate;}
	void OnDisable() {foodCore.onConsume -= BeginReplicate;}

	void BeginReplicate()
	{
		//For the amount allow to try replicate
		for (int i = 0; i < replicateAmount; i++)
		{
			//If random chance allow to replicate
			if(Random.Range(0f,100f) >= replicateChance)
			{
				//Replicate it self from spawner
				FoodsSpawner.i.ChooseFoodPlot(gameObject);
			}
		}
	}
}
