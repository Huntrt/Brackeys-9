using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SnakeInfo : MonoBehaviour
{

	public Health health; [System.Serializable] public class Health
	{
    	[SerializeField] Snake snake;
		[SerializeField] TextMeshProUGUI healthText;
		[SerializeField] Image healthBar;
	
		public void RefreshHealthBar()
		{
			//Fill health bar with health currently have 
			healthBar.fillAmount = Mathf.Clamp01((float)snake.health / (float)snake.maxHealth);
			//Display health text as 80/100
			healthText.text = snake.health + "\n----\n" + snake.maxHealth;
		}
	}

	public Movement movement; [System.Serializable] public class Movement
	{
		[SerializeField] Snake snake;
		[SerializeField] TextMeshProUGUI speedText;

		public void RefreshSpeedText()
		{
			//Display snake movement speed
			speedText.text = snake.movement.moveSpeed.ToString();
		}
	}

	void Update()
	{
		movement.RefreshSpeedText();
	}
}
