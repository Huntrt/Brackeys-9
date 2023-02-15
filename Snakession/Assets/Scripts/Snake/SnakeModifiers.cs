using UnityEngine;

public class SnakeModifiers : MonoBehaviour
{
    public int foodAdditional, veganAdditional, meatAdditional;
    public int foodIncrease, veganIncrease, meatIncrease;

	public int EatAdditional(int amount, bool isVegan)
	{
		//Gain additional amount from modifiers
		amount += foodAdditional;
		//Gain additional amount depends of food eated are vegan or meat
		if(isVegan) amount += veganAdditional; else amount += meatAdditional;
		//Return amount has added
		return amount;
	}

	public int EatIncrease(int amount, bool isVegan)
	{
		//Increase amount from modifiers
		amount += (foodIncrease/100) * amount;
		//Increase amount depends of food eated are vegan or meat
		if(isVegan) amount += (veganIncrease/100) * amount; else amount += (meatIncrease/100) * amount;
		//Return amount has increase
		return amount;
	}
}