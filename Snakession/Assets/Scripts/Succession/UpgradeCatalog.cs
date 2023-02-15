using UnityEngine;

public class UpgradeCatalog : MonoBehaviour
{
    public enum Upgrades
	{
		Haste, Stabilize, Steering, BiggerHead, Digest, FreshFood, Fertilizer
	}

	Snake snake; void Start() {snake = Snake.i;}

	//USe upgrade relied on string to invoke correct function
	public void UseUpgrade(Upgrades upgrade) {Invoke(upgrade + "Upgrading", 0);}

	void HasteUpgrading() 
	{
		//? Increase movement speed
		snake.movement.moveSpeed += (10/100) * snake.movement.moveSpeed;
	}
	void StabilizeUpgrading() 
	{
		//? Decrease movement speed
		snake.movement.moveSpeed -= (10/100) * snake.movement.moveSpeed;
	}
	void SteeringUpgrading() 
	{
		//* Increase rotation speed
		snake.movement.rotateSpeed += (5/100) * snake.movement.rotateSpeed;
	}
	void BiggerHeadUpgrading() 
	{
		//* Increase initial max health
		snake.maxHealth += 2;
		//? Bigger head
		snake.body.head.localScale += (5/100) * snake.body.head.localScale;
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
		FoodSpawner.i.spawnRate -= (5/100) * FoodSpawner.i.spawnRate;
	}
	void FertilizerUpgrading()
	{
		//* Gain food spawn rate
		FoodSpawner.i.spawnRate += (10/100) * FoodSpawner.i.spawnRate;
		//! Lost food lifetime
		snake.mod.freshIncrease.food -= 2;
	}
}