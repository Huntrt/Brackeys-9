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
	public int foodEaten, totalMaxHPGained;
	public SnakeBody body;
	public SnakeMovement movement;
	public SnakeModifiers mod;
	public SnakeMoney money;
	public SnakeScore score;
	public SnakeInfo info;
	public SnakeHealthEffect healthEffect;
	[SerializeField] ParticleSystem releasedEffect;

	public void Eat(int amount, bool isVegan)
	{
		//Begin increase the additive amount gain from mod 
		amount += mod.foodMaxHPAdditive.GetAdditive(amount, isVegan);
		//Begin increase the bonus amount gain from mod
		amount += mod.foodMaxHPBonus.GetBonus(isVegan);
		//This snake has eat one more time
		foodEaten++;
		//Increase the amount has gained for max health
		totalMaxHPGained += amount;
		//Popup the amount has grow "^23"(Yellow)
		TextPopup.i.Popuping("<#ffff1f>^"+ amount +"</color>");
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
	}

	public void Heal(int amount)
	{
		//Gain health with amount given
		health += amount;
		//Clamp health to max health
		health = Mathf.Clamp(health, 0, maxHealth);
		healthEffect.RefreshHealthBar();
		healthEffect.FlashingHealthStatus(false);
		//Popup the amount has head "+23"(Green)
		TextPopup.i.Popuping("<#35ff1f>+"+ amount +"</color>");
	}

	public void Hurt(int amount)
	{
		//Lose health with amount given
		health -= amount;
		//If out of health
		if(health <= 0)
		{	
			//Game is over 
			GameOver.i.GameIsOver();
			//Destroy the player
			Destroy(gameObject);
		}
		healthEffect.RefreshHealthBar();
		healthEffect.FlashingHealthStatus(true);
		//Popup the amount has hurt "-23"(Red)
		TextPopup.i.Popuping("<#ff1f3d>-"+ amount +"</color>");
	}

	public void ResetSnake()
	{
		//STtart an new eat count for this sucessor
		foodEaten = 0;
		//Disable snake movement
		movement.enabled = false;
	}

	public void ReleaseSnake()
	{
		//Move head back to the center
		body.head.transform.position = Vector3.zero;
		//Initial max health
		maxHealth = initalHealth;
		//Heal to full max health
		Heal(maxHealth);
		//Reseting body part
		body.ResetPart();
		//Clear all the speed booste
		mod.moveSpeedBoost.boosts.Clear();
		//Enable snake movement
		movement.enabled = true;
		//Play released effect
		releasedEffect.Play();
	}
}