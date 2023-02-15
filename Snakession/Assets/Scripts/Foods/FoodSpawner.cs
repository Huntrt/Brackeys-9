using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
	#region Set this class to singleton
	static FoodSpawner _i; public static FoodSpawner i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<FoodSpawner>();
			}
			return _i;
		}
	}
	#endregion

	public float spawnRate, spawnChance; float spawnRateTimer;
	[SerializeField] WeightDrop[] foods;

    void Update()
    {
        SpawningFood();
    }

	void SpawningFood()
	{
		//Counting the timer until reached the rate
		spawnRateTimer += Time.deltaTime; if(spawnRateTimer >= spawnRate)
		{
			//Does spawn chance meet the random number
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
		//Spawn object at given coord on the map with the foods has been weighted
		GameObject spawn = Map.i.PlaceObject(coord, WeightSystem.Weighting(foods).obj);
		//If successfully spawn food
		if(spawn != null)
		{
			//Set the object has been spawn's spawn coordinate
			spawn.GetComponent<Foods>().spawnCoord = coord;
		}
	}
}