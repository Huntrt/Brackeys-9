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
	public int eatenEachSuccession;
	public SnakeBody body;
	public SnakeMovement movement;
	public SnakeModifiers mod;
	public SnakeMoney money;
	public SnakeScore score;
	public Image healthBar;
	public TextMeshProUGUI healthText;

	public void Eat(int amount, bool isVegan)
	{
		//This sucessor has eat one more time
		eatenEachSuccession++;
		//Begin increase the additive amount gain from mod 
		amount += mod.foodMaxHPAdditive.GetAdditive(amount, isVegan);
		//Begin increase the bonus amount gain from mod
		amount += mod.foodMaxHPBonus.GetBonus(isVegan);
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
		//STtart an new eat count for this sucessor
		eatenEachSuccession = 0;
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
		//Clear all the speed booste
		mod.moveSpeedBoost.boosts.Clear();
		//Enable snake movement
		movement.enabled = true;
	}

	void UpdateHealthBar()
	{
		//Stop if max health are 0
		if(maxHealth == 0) return;
		//Display health bar
		healthBar.fillAmount = Mathf.Clamp01((float)health/(float)maxHealth);
		//Display health as text
		healthText.text = health + "/" + maxHealth;
	}
}
