using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
	public float spawnRate, spawnChance; float spawnRateTimer;
	[SerializeField] WeightDrop[] foods;

    void Update()
    {
        SpawningFood();
    }

	void SpawningFood()
	{
		spawnRateTimer += Time.deltaTime;
		if(spawnRateTimer >= spawnRate)
		{
			//Does spawn chanced meet the random number
			bool chanced = spawnChance >= Random.Range(0f, 100f);
			//If allow to spawn by chance
			if(chanced)
			{
				//Get any random empty plot
				int ranPlot = Random.Range(0, Map.i.emptyPlots.Count);
				//Drop the food at coordinate of random empty plot has get
				DropFood(Map.i.emptyPlots[ranPlot].coordinate);
			}
			//Reset rate timer
			spawnRateTimer -= spawnRateTimer;
		}
	}

	public void DropFood(Vector2 coord)
	{
		//Place object at given coord with the foods has been weight
		Map.i.PlaceObject(coord, WeightSystem.Weighting(foods).obj);
	}
}