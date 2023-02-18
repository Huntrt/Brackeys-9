using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SnakeInfo : MonoBehaviour
{
    [SerializeField] Snake snake;
	[Header("Health")]
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
