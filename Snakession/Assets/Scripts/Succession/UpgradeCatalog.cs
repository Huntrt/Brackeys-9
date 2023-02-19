using System.Collections.Generic;
using UnityEngine;

public class UpgradeCatalog : MonoBehaviour
{
    public enum Upgrades
	{
		Haste, Stabilize, Steering, BiggerHead, Digest, FreshFood, Fertilizer,
		Vegetarian, Carnivore, Nutrition, Leftover, HarderBody, Agile
	}

	List<Upgrades> upgradeUsed = new List<Upgrades>();	

	Snake snake; void Start() {snake = Snake.i;}

	public void UseUpgrade(UpgradeInfo info) 
	{
		//If able to spend for the given upgrade cost
		if(Snake.i.money.Spend(info.cost))
		{
			//The given upgrade has been use
			upgradeUsed.Add(info.upgrade);
			//Upgrading given upgrade
			Invoke(info.upgrade + "Upgrading", 0);
			//Invoke end upgrade
			Invoke("EndUpgrade", 0);
		}
	}

	
	//Succession is over after end upgrade
	void EndUpgrade() {SuccessionManager.i.EndSuccession();}

	void HasteUpgrading() 
	{
		//? Increase inital movement speed
		snake.movement.InitialMoveSpeed += (10f/100f) * snake.movement.InitialMoveSpeed;
	}
	void StabilizeUpgrading() 
	{
		//? Decrease inital movement speed
		snake.movement.InitialMoveSpeed -= (10f/100f) * snake.movement.InitialMoveSpeed;
	}
	void SteeringUpgrading() 
	{
		//* Increase rotation speed
		snake.movement.rotateSpeed += (5f/100f) * snake.movement.rotateSpeed;
	}
	void BiggerHeadUpgrading() 
	{
		//* Increase initial max health
		snake.initalHealth += 2;
		//? Bigger head
		//Increase size only in the X Y axis
		Vector3 up = new Vector3(snake.body.head.localScale.x, snake.body.head.localScale.y, 0);
		snake.body.head.localScale += ((5f/100f) * up);
	}
	void DigestUpgrading()
	{
		//* Increase food eat
		snake.mod.foodMaxHPAdditive.flex += 5;
	}
	void FreshFoodUpgrading()
	{
		//* Gain food lifetime
		snake.mod.freshBonus.flex += 3;
		//! Lose food spawn rate
		FoodsSpawner.i.spawnRate -= (5f/100f) * FoodsSpawner.i.spawnRate;
	}
	void FertilizerUpgrading()
	{
		//* Gain food spawn rate
		FoodsSpawner.i.spawnRate += (10f/100f) * FoodsSpawner.i.spawnRate;
		//! Lost food lifetime
		snake.mod.freshAdditive.flex -= 2;
	}
	void VegetarianUpgrading()
	{
		//* Gain 5% from vegan food
		snake.mod.foodMaxHPAdditive.vegan += 5;
		//! Lost 2 food from meat
		snake.mod.foodMaxHPBonus.meat -= 4;
	}
	void CarnivoreUpgrading()
	{
		//* Gain 7 food from meat
		snake.mod.foodMaxHPBonus.meat += 7;
		//! Lost 3% food vegan
		snake.mod.foodMaxHPAdditive.vegan -= 4;
	}
	void NutritionUpgrading()
	{
		//* Gain 1 extra food from any source
		snake.mod.foodMaxHPBonus.flex += 1;
	}
	void LeftoverUpgrading() 
	{
		//? Wait for 15 sec
		Invoke("LeftoverDelay", 15f);
	} void LeftoverDelay() {snake.Heal(25);} //* Heal 25
	void HarderBodyUpgrading()
	{
		//* Increase initial health
		snake.initalHealth += 7;
		//? Lost 20% initial movement speed
		snake.movement.InitialMoveSpeed -= 0.2f * snake.movement.InitialMoveSpeed;
	}
	void AgileUpgrading()
	{
		//* Wait for 0.1 sec
		Invoke("AgileDelay", 0.1f);
	} void AgileDelay() {snake.mod.moveSpeedBoost.AddBoost(30, 4, "Agile");} //? Gain 30% movespeed for 4 sec
}