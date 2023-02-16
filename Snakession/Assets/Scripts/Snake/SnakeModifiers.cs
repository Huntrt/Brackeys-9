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
			//Save amount for vegan
			int vAmount = amount;
			//Increase amount got eat
			amount += (food/100) * amount;
			//Increase amount depends of food eated are vegan or meat
			if(isVegan) vAmount += (vegan/100) * vAmount; else vAmount += (meat/100) * vAmount;
			//Return both amount
			return amount + vAmount;
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