using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
	#region Set this class to singleton
	static GameOver _i; public static GameOver i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<GameOver>();
			}
			return _i;
		}
	}
	#endregion

	[SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI scoreText, levelText, maxHpAccuText, foodEatenText;

	public void GameIsOver()
	{
		//Show the game over panel
		gameOverPanel.SetActive(true);
		//@ Display info as Info                000
		scoreText.text = "<align=left>Score<line-height=0>\n<align=right>"+Snake.i.score.score+"<line-height=1em>";
		levelText.text = "<align=left>Level<line-height=0>\n<align=right>"+Map.i.level+"<line-height=1em>";
		maxHpAccuText.text = "<align=left>Max HP Accumulated<line-height=0>\n<align=right>"+Snake.i.totalMaxHPGained+"<line-height=1em>";
		foodEatenText.text = "<align=left>Food Eaten<line-height=0>\n<align=right>"+Snake.i.foodEaten+"<line-height=1em>";
	}
}