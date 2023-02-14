using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class WeightDrop
{
	public GameObject obj;
	public float weight;
	public float requirement;
}

public static class WeightSystem
{
    public static WeightDrop Weighting(WeightDrop[] drops, float required = 0)
	{
		//Get the total sum of all drops's weight
		float sum = 0; for (int d = 0; d < drops.Length; d++)
		{
			//Skip if havent meet requirement of this drop
			if(drops[d].requirement > required) continue;
			//Added this drop weight to the sum
			sum += drops[d].weight;
		}
		//Randomize sum
		sum = Random.Range(0, sum);
		//Go through all the drops
		for (int d = 0; d < drops.Length; d++)
		{
			//Skip if havent meet requirement of this drop
			if(drops[d].requirement > required) continue;
			//If this drop weight take all the sum
			if(sum - drops[d].weight <= 0)
			{
				//Return this drop
				return drops[d];
			}
			//Sum now lost this drop weight if havent drop
			else sum -= drops[d].weight;
		}
		//? This should never fail to return any drop
		return null;
	}
}