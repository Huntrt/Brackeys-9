using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SnakeHealthEffect : MonoBehaviour
{
	[SerializeField] Snake snake;
	[SerializeField] TextMeshProUGUI healthText;
	[SerializeField] Image healthBar;
	[SerializeField] float flashDuration;
	[SerializeField] Color hurtHeadColor, hurtTailColor;
	[SerializeField] Color healHeadColor, healTailColor;
	[SerializeField] Color defaultHeadColor, defaultTailColor;
	[SerializeField] SpriteRenderer headRenderer;
	LineRenderer bodyRenderer;

	public void RefreshHealthBar()
	{
		//Fill health bar with health currently have 
		healthBar.fillAmount = Mathf.Clamp01((float)snake.health / (float)snake.maxHealth);
		//Display health text as 80/100
		healthText.text = snake.health + "\n----\n" + snake.maxHealth;
	}

	public void FlashingHealthStatus(bool isHurt)
	{
		//Get the body's line renderer
		bodyRenderer = snake.body.line;
		//Clear any flash currently running
		ClearFlashing();
		//Save the default color of head
		defaultHeadColor = bodyRenderer.startColor;
		//Save the default color of tail
		defaultTailColor = bodyRenderer.endColor;
	}

	void ClearFlashing()
	{
		//Set body color back to default
		SetWholeBodyColor(defaultHeadColor, defaultTailColor);
	}

	void SetWholeBodyColor(Color headColor, Color tailColor)
	{
		headRenderer.color = headColor;
		bodyRenderer.startColor = headColor;
		bodyRenderer.endColor = tailColor;
	}
}
