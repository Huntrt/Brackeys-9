using UnityEngine.UI;
using UnityEngine;
using TMPro;

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

    public int initalHealth, maxHealth, health;
	//Every X max health will grow
	public float growthEveryHealth;
	float growing;
	public SnakeBody body;
	public SnakeTail tail;
	public SnakeMovement movement;
	public SnakeModifiers mod;
	public SnakeMoney money;
	public Image healthBar;
	public TextMeshProUGUI healthText;

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
		UpdateHealthBar();
	}

	public void Hurt(int amount)
	{
		health -= amount;
		if(health <= 0) Destroy(gameObject);
		UpdateHealthBar();
	}

	public void ResetSnake()
	{
		//Move head back to the center
		body.head.transform.position = Vector3.zero;
		//Reseting body part
		body.ResetPart();
		//Initial max health
		maxHealth = initalHealth;
		//Heal to full max health
		Heal(maxHealth);
		//Disable snake movement
		movement.enabled = false;
	}

	public void ReleaseSnake()
	{
		//Enable snake movement
		movement.enabled = true;
	}

	void UpdateHealthBar()
	{
		//Stop if max health are 0
		if(maxHealth == 0) return;
		//Display health bar
		healthBar.fillAmount = Mathf.Clamp01(health/maxHealth);
		//Display health as text
		healthText.text = health + "/" + maxHealth;
	}
}
