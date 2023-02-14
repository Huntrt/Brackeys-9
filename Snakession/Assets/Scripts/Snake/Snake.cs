using UnityEngine;

public class Snake : MonoBehaviour
{
    public float maxHealth, health;
	//Every X max health will grow
	public float growthEveryHealth;
	float growing;
	[SerializeField] SnakeBody body;

	public void Eat(int amount)
	{
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
