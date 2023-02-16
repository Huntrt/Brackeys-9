using UnityEngine;
using TMPro;

public class SnakeScore : MonoBehaviour
{
    public int score;
	[SerializeField] float scoreEveryHealth;
	[SerializeField] float levelPercentIncrease;
	[SerializeField] TextMeshProUGUI scoreText;

	public void GainingScore()
	{
		//The total score will gain
		float gain = 0;
		//Save snake max hp
		float maxHP = Snake.i.maxHealth;
		//While max hp could still gain
		while (maxHP >= scoreEveryHealth)
		{
			//will gain one score
			gain++;
			//Gained score from maxhp
			maxHP -= scoreEveryHealth;
		}
		//Get increase percented of gained score base on level
		gain += (((float)Map.i.level*levelPercentIncrease)/100) * gain;
		//Gaind score
		score += (int)gain;
		//Display score text
		scoreText.text = "SCORE: " + score;
	}
}