using UnityEngine;
using TMPro;

public class SnakeMoney : MonoBehaviour
{
    public int money;
	public TextMeshProUGUI moneyText;
	public float earnEveryHealth;

	public bool Spend(int price)
	{
		if(money - price > 0)
		{
			money -= price;
			UpdateMoneyText();
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public void Earn(int amount)
	{
		money += amount;
		UpdateMoneyText();
	}

	public void EarningMaxHealth()
	{
		//Get max health from snake
		float maxHP = Snake.i.maxHealth;
		//If haven't earn all the max health
		while (maxHP >= earnEveryHealth)
		{
			//Earn 1 money
			Earn(1);
			//Lose 1 earn
			maxHP -= earnEveryHealth;
		}
	}

	void UpdateMoneyText()
	{
		moneyText.text = "$" + money;
	}
}