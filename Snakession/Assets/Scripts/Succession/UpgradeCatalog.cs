using UnityEngine;

public class UpgradeCatalog : MonoBehaviour
{
    public enum Upgrades
	{
		Haste, Stabilize, Steering, BiggerHead, Digest, FreshFood, Fertilizer
	}

	Snake snake; void Start() {snake = Snake.i;}

	public void UseUpgrade(UpgradeInfo info) 
	{
		//If able to spend for the given upgrade cost
		if(Snake.i.money.Spend(info.cost))
		{
			//Upgrading given upgrade
			Invoke(info.upgrade + "Upgrading", 0);
			//Succession is over
			SuccessionManager.i.EndSuccession();
		}
		//If out of money
		else
		{
			//temp: Print out of money message
			print("You are out of money");
		}
	}

	void HasteUpgrading() 
	{
		//? Increase movement speed
		snake.movement.moveSpeed += (10f/100f) * snake.movement.moveSpeed;
	}
	void StabilizeUpgrading() 
	{
		//? Decrease movement speed
		snake.movement.moveSpeed -= (10f/100f) * snake.movement.moveSpeed;
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
		snake.mod.foodIncrease.food += 5;
	}
	void FreshFoodUpgrading()
	{
		//* Gain food lifetime
		snake.mod.freshAdditional.food += 3;
		//! Lose food spawn rate
		FoodSpawner.i.spawnRate -= (5f/100f) * FoodSpawner.i.spawnRate;
	}
	void FertilizerUpgrading()
	{
		//* Gain food spawn rate
		FoodSpawner.i.spawnRate += (10f/100f) * FoodSpawner.i.spawnRate;
		//! Lost food lifetime
		snake.mod.freshIncrease.food -= 2;
	}
}