using UnityEngine;

public class Snake : MonoBehaviour
{
	#region Set this class to singleton
	static Snake _i; public static Snake i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<Snake>();
			}
			return _i;
		}
	}
	#endregion

    public float maxHealth, health;
	//Every X max health will grow
	public float growthEveryHealth;
	float growing;
	public SnakeBody body;
	public SnakeTail tail;
	public SnakeMovement movement;
	public SnakeModifiers mod;

	public void Eat(int amount, bool isVegan)
	{
		//Begin increase the amount gain from good
		amount += mod.foodIncrease.GetIncrease(amount, isVegan);
		//Begin gain additional amount from mod
		amount += mod.foodAdditional.GetAdditional(isVegan);
		//Grow with the amount has given
		growing += amount;
		//While has meet requirement to grow
		while(growing >= growthEveryHealth)
		{
			//Grow body part
			body.Grow();
			//Decrease grow with requirement
			growing -= growthEveryHealth;
		}
		//Increase max health than heal with given amount
		maxHealth += amount; Heal(amount);
	}

	public void Heal(int amount)
	{
		health += amount;
		health = Mathf.Clamp(health, 0, maxHealth);
	}

	public void Hurt(int amount)
	{
		health -= amount;
		if(health <= 0) Destroy(gameObject);
	}
}
