using UnityEngine;

public class FoodsSpawner : MonoBehaviour
{
	#region Set this class to singleton
	static FoodsSpawner _i; public static FoodsSpawner i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<FoodsSpawner>();
			}
			return _i;
		}
	}
	#endregion

	public float spawnRate, spawnChance; float spawnRateTimer;
	[SerializeField] WeightDrop[] foods;

    void Update()
    {
		//Only spawn food if current map is not null
        if(Map.i.currentMap != null) {SpawningFood();}
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
				//Weighted an food to choose which plot it will spawn
				ChooseFoodPlot(WeightSystem.Weighting(foods, Map.i.level).obj);
			}
			//Reset rate timer
			spawnRateTimer -= spawnRateTimer;
		}
	}

	public void ChooseFoodPlot(GameObject food)
	{				
		//Get any random empty plot
		int ranPlot = Random.Range(0, Map.i.emptyPlots.Count);
		//Create the food at coordinate of random empty plot has get
		CreatingFood(Map.i.emptyPlots[ranPlot].coordinate, food);
	}

	public void CreatingFood(Vector2 coord, GameObject foodToCreated)
	{
		//Place the food need to create onto map at given coordinates
		GameObject created = Map.i.PlaceObject(coord, foodToCreated);
		//If successfully created food
		if(created != null)
		{
			//Group food just create
			created.transform.SetParent(Map.i.foodGroup);
			//Set the food has been spawn it spawn coordinate
			created.GetComponent<Food>().spawnCoord = coord;
		}
	}
}