using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SnakeInfo : MonoBehaviour
{
	public Movement movement; [System.Serializable] public class Movement
	{
		[SerializeField] Snake snake;
		[SerializeField] TextMeshProUGUI speedText;

		public void RefreshSpeedText()
		{
			//Display snake movement speed
			speedText.text = "SPEED: " + (int)snake.movement.moveSpeed;
		}
	}
	
	void Update()
	{
		movement.RefreshSpeedText();
	}

	void ClearHealthFlashing()
	{

	}
}
