using UnityEngine;

public class SnakeModifiers : MonoBehaviour
{
	public FoodMaxHPBonus foodMaxHPBonus; [System.Serializable] public class FoodMaxHPBonus
	{
   		public int flex, vegan, meat;
		public int GetBonus(bool isVegan)
		{
			//Gain bonus flex amount first then get bonus from vegan or meat 
			return flex + ((isVegan) ? vegan : meat);
		}
	}
	
	public FoodMaxHPAdditive foodMaxHPAdditive; [System.Serializable] public class FoodMaxHPAdditive
	{
		public float flex, vegan, meat;
		public int GetAdditive(int amount, bool isVegan)
		{
			//Gain percented amount amount given with flex and even more with vegan or meat
			return (int)(((flex + ((isVegan) ? vegan : meat))/100f) * amount);
		}
	}

	public FreshBonus freshBonus; [System.Serializable] public class FreshBonus
	{
   		public int flex;
		public int GetBonus() {return flex;}
	}

	public FreshAdditive freshAdditive; [System.Serializable] public class FreshAdditive
	{
   		public float flex;
		public float GetAdditive(float amount) {return (flex/100f) * amount;}
	}
}