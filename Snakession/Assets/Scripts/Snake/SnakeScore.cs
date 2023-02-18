using UnityEngine;
using TMPro;

public class SnakeScore : MonoBehaviour
{
    public int score;
	[SerializeField] float scoreEveryHealth;
	[SerializeField] float levelPercentIncrease;

	public void GainingScore()
	{
		//The total score will gain
		float gain = Snake.i.maxHealth/scoreEveryHealth;
		//Get increase percented of gained score base on level
		gain += (((float)Map.i.level*levelPercentIncrease)/100) * gain;
		//Gaind score
		score += (int)gain;
		//Display score has converted in format "SCORE (+12)92"
		SuccessionManager.i.scoreConvertText.text = "= SCORE:" + " (+" + (int)gain + ") " + score;
	}
}