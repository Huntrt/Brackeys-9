using UnityEngine;

public class SnakeModifiers : MonoBehaviour
{
	public FoodAdditional foodAdditional; [System.Serializable] public class FoodAdditional
	{
   		public int food, vegan, meat;
		public int GetAdditional( bool isVegan)
		{
			//Gain additional amount 
			int additional = food;
			//Gain more additional amount depends of food eated are vegan or meat
			if(isVegan) additional += vegan; else additional += meat;
			//Return additional amount
			return additional;
		}
	}
	
	public FoodIncrease foodIncrease; [System.Serializable] public class FoodIncrease
	{
		public float food, vegan, meat;

		public int GetIncrease(int amount, bool isVegan)
		{
			//Get the amount got increase
			float Increased = (food/100f) * amount;
			//This increase use for vegan/meat
			float vIncreased = 0;
			//Get increase of given amount depends of food eated are vegan or meat
			if(isVegan) vIncreased += (vegan/100f) * amount; else vIncreased += (meat/100f) * amount;
			//Return both increase as int
			return (int)(Increased + vIncreased);
		}
	}

	public FreshAdditional freshAdditional; [System.Serializable] public class FreshAdditional
	{
   		public int food;
		public int GetAdditional() {return food;}
	}

	public FreshIncrease freshIncrease; [System.Serializable] public class FreshIncrease
	{
   		public float food;
		public float GetIncrease(float amount) {return (food/100f) * amount;}
	}
}