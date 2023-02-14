using UnityEngine;

public class Snake : MonoBehaviour
{
    public float maxHealth, health;

	public void Eat(int amount)
	{
		maxHealth += amount;
		Heal(amount);
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
