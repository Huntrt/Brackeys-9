using UnityEngine;

public class SnakeMoney : MonoBehaviour
{
    public int money;
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
		//Divide max health with each max health to earn
		float earn = Snake.i.maxHealth/earnEveryHealth;
		Earn((int)earn);
		//Display money has converted in format "MONEY (+12)92"
		SuccessionManager.i.moneyConvertText.text = "MONEY:" + " (+" + earn + ") " + money;
	}

	void UpdateMoneyText()
	{
		//Display money has converted in format "MONEY (+12)92"
		SuccessionManager.i.moneyConvertText.text = "MONEY:" + money;
	}
}