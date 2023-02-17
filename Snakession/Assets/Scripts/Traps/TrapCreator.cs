using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TrapCreator : MonoBehaviour
{
	[SerializeField] Placer placer; [System.Serializable] public class Placer
	{
		public float quantity, chance; 
		public TrapDrop[] placeTraps;
	}
	[SerializeField] Spawner spawner; [System.Serializable] public class Spawner
	{
		public float rate, chance; 
		[HideInInspector] public float rateTimer;
		public TrapDrop[] spawnTraps;
	}

	[System.Serializable] public class TrapDrop : WeightDrop
	{
		public float populateChance;
		public int populateLimit;
	}

    void Update()
    {
		//Only spawn trap if map exist
        if(Map.i.currentMap != null) SpawningTrap();
    }

	public void PlacingTrap()
	{
		//Go through all the quantity need to place
		for (int q = 0; q < placer.quantity; q++)
		{
			//Create placer's traps with placer's chance
			CreateTrap(placer.placeTraps, placer.chance);
		}
	}

	void SpawningTrap()
	{
		//Counting the timer until reached the rate
		spawner.rateTimer += Time.deltaTime; if(spawner.rateTimer >= spawner.rate)
		{
			//Create spawner's traps with spawner's chance
			CreateTrap(spawner.spawnTraps, spawner.chance);
			//Reset rate timer
			spawner.rateTimer -= spawner.rateTimer;
		}
	}

	void CreateTrap(TrapDrop[] traps, float chance)
	{
		//Does given chanced meet the random number
		bool chanced = chance >= Random.Range(0f, 100f);
		//If allow to create by chance
		if(chanced)
		{
			//Get any random empty plot
			int ranPlot = Random.Range(0, Map.i.emptyPlots.Count);
			//Drop the given trap list at coordinate of random empty plot has get
			DropTrap(Map.i.emptyPlots[ranPlot].coordinate, traps);
		}
	}

	public void DropTrap(Vector2 coord, TrapDrop[] traps)
	{
		//Weight the traps
		WeightDrop weighted = WeightSystem.Weighting((WeightDrop[])traps);
		//The trap will be drop
		TrapDrop trap = null;
		//Go through all the traps could drop
		for (int t = 0; t < traps.Length; t++)
		{
			//Get the trap that has the same object as weighted trap
			if(weighted.obj == traps[t].obj) trap = traps[t];
		}
		//Begin populated weighted trap at given coord
		StartCoroutine(PopulateTrap(coord, trap, 0));
	}

	IEnumerator PopulateTrap(Vector2 coord, TrapDrop trap, int populatedCount)
	{
		yield return null;
		//Stop if has reached populated limit
		if(populatedCount > trap.populateLimit) yield break;
		//Place the given trap onto given coordinate at map 
		GameObject placed = Map.i.PlaceObject(coord, trap.obj);
		//Stop if unable to place trap
		if(placed == null) yield break;
		//Group the trap just got place
		placed.transform.SetParent(Map.i.trapGroup);
		//Create an an list of 4 avail direction to populate
		List<int> aDir = new List<int>(){0,1,2,3};
		//Go through all available direction in revert
		for (int d = aDir.Count - 1; d >= 0 ; d--)
		{
			//Randomly choose any direction left to populate
			int choose = UnityEngine.Random.Range(0, aDir.Count);
			//If able to populate again
			if(trap.populateChance > Random.Range(0f,100f))
			{
				//Coordinate to be populate
				Vector2 popCoord = coord;
				//@ Increase populate coordinate base on direction choose
				switch(choose)
				{
					case 0: popCoord += Vector2.up; break;
					case 1: popCoord += Vector2.down; break;
					case 2: popCoord += Vector2.left; break;
					case 3: popCoord += Vector2.right; break;
				}
				//Increase populate counter
				populatedCount++;
				//Populate this trap again at coordinate has get with it count has been increase
				StartCoroutine(PopulateTrap(popCoord, trap, populatedCount));
			}
			//The direction got choose no longer available
			aDir.Remove(choose);
		}
	}
}