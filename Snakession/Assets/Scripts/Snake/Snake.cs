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
	[Header("Health GUI")]
	[SerializeField] int containerEveryHealth;
	[SerializeField] Transform healthBar;
	[SerializeField] GameObject healthContainer;
	Image[] containerProgress;

	public void Eat(int amount, bool isVegan)
	{
		//This sucessor has eat one more time
		eatenEachSuccession++;
		//Begin increase the additive amount gain from mod 
		amount += mod.foodMaxHPAdditive.GetAdditive(amount, isVegan);
		//Begin increase the bonus amount gain from mod
		amount += mod.foodMaxHPBonus.GetBonus(isVegan);
		//Increase max health than heal with given amount
		maxHealth += amount; Heal(amount);
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
		RenewHealthBar();
	}

	public void Heal(int amount)
	{
		health += amount;
		health = Mathf.Clamp(health, 0, maxHealth);
		RefreshHealthContainer();
	}

	public void Hurt(int amount)
	{
		health -= amount;
		if(health <= 0) Destroy(gameObject);
		RefreshHealthContainer();
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
		//Update the health bar
		RenewHealthBar();
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

	void RenewHealthBar()
	{
		//Stop if max health are 0
		if(maxHealth == 0) return;
		//Get how many coontainer needed for max hp
		int containerCount = maxHealth/containerEveryHealth;
		//Destroy all the container of current health bar
		for (int b = 0; b < healthBar.childCount; b++) Destroy(healthBar.GetChild(b).gameObject);
		//Prepare array for all the container
		containerProgress = new Image[containerCount];
		//Go through all the container needed
		for (int c = 0; c < containerCount; c++)
		{
			//Create an new health container
			GameObject container = Instantiate(healthContainer);
			//Parent this health container to bar
			container.transform.SetParent(healthBar);
			//Get the image from this container children to be progress
			containerProgress[c] = container.transform.GetChild(0).GetComponent<Image>();
		}
		RefreshHealthContainer();
	}

	void RefreshHealthContainer()
	{
		//Go through each of container progress
		for (int c = 0; c < containerProgress.Length; c++)
		{
			//Get the this container progress
			Image container = containerProgress[c];
			//Get progress of by mulitple this container order with how health each container have
			float progress = c * containerEveryHealth;
			//Fill this health container with it progress base on current health
			container.fillAmount = Mathf.Clamp01((health - progress) / progress);
		}
	}
}