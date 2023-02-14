using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
	
	public float spawnRate, spawnChance; float spawnRateTimer;
	[SerializeField] TrapDrop[] traps;
	[System.Serializable] class TrapDrop : WeightDrop
	{
		public float populateChance;
		public int populateLimit;
	}

    void Update()
    {
        SpawningTrap();
    }

	void SpawningTrap()
	{
		//Counting the timer until reached the rate
		spawnRateTimer += Time.deltaTime; if(spawnRateTimer >= spawnRate)
		{
			//Does spawn chanced meet the random number
			bool chanced = spawnChance >= Random.Range(0f, 100f);
			//If allow to spawn by chance
			if(chanced)
			{
				//Get any random empty plot
				int ranPlot = Random.Range(0, Map.i.emptyPlots.Count);
				//Drop the trap at coordinate of random empty plot has get
				DropTrap(Map.i.emptyPlots[ranPlot].coordinate);
			}
			//Reset rate timer
			spawnRateTimer -= spawnRateTimer;
		}
	}

	public void DropTrap(Vector2 coord)
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
		//Place the given trap onto given coordinate at map but stop if unable to
		if(Map.i.PlaceObject(coord, trap.obj) == null) yield break;
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