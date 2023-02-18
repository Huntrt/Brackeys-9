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
			//Invoke end upgrade
			Invoke("EndUpgrade", 0);
		}
		//If out of money
		else
		{
			//temp: Print out of money message
			print("You are out of money");
		}
	}

	
	//Succession is over after end upgrade
	void EndUpgrade() {SuccessionManager.i.EndSuccession();}

	void HasteUpgrading() 
	{
		//? Increase inital movement speed
		snake.movement.initialMoveSpeed += (10f/100f) * snake.movement.initialMoveSpeed;
	}
	void StabilizeUpgrading() 
	{
		//? Decrease inital movement speed
		snake.movement.initialMoveSpeed -= (10f/100f) * snake.movement.initialMoveSpeed;
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
}