using UnityEngine;
using TMPro;

public class SuccessionManager : MonoBehaviour
{
    #region Set this class to singleton
	static SuccessionManager _i; public static SuccessionManager i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<SuccessionManager>();
			}
			return _i;
		}
	}
	#endregion

	[SerializeField] GameObject successionPanel;
	[SerializeField] UpgradeManager upgradeManager;
	public TextMeshProUGUI maxHPAcquiredText, scoreConvertText, moneyConvertText;

	
	public void BeginSuccession()
	{
		//Earn money from snake max health
		Snake.i.money.EarningMaxHealth();
		//Gain score from snake max health
		Snake.i.score.GainingScore();
		//Display the amount of max health has acquired
		maxHPAcquiredText.text = "MAX HP ACQUIRED: " + Snake.i.maxHealth;
		//Clear the map
		Map.i.ClearMap();
		//Refresh the upgrade
		upgradeManager.RefreshUpgrade();
		//Active the panel
		successionPanel.SetActive(true);
	}

	public void EndSuccession()
	{
		//Start an new level
		Map.i.level++;
		//Create an new map
		Map.i.CreateMap();
		//Hide the panel
		successionPanel.SetActive(false);
	}
}