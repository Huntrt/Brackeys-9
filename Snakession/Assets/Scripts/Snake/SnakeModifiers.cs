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
		public int food, vegan, meat;
		public int GetIncrease(int amount, bool isVegan)
		{
			//Increase amount got eat
			amount += (food/100) * amount;
			//Increase amount depends of food eated are vegan or meat
			if(isVegan) amount += (vegan/100) * amount; else amount += (meat/100) * amount;
			//Return amount has increase
			return amount;
		}
	}

	public FreshAdditional freshAdditional; [System.Serializable] public class FreshAdditional
	{
   		public int food;
		public int GetAdditional() {return food;}
	}
}