using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
	[SerializeField] UpgradeCatalog catalog;
	[SerializeField] UpgradeInfo[] upgradeInfos;

    [SerializeField] PanelGUI[] panels;
	[System.Serializable] class PanelGUI
	{
		public TextMeshProUGUI nameText, descText, costText;
		public Image iconImg;
		public Button upgradeButton;
	}

	void Start()
	{
		//Go through all the panel
		for (int p = 0; p < panels.Length; p++)
		{
			//Weighting to get upgrade info
			UpgradeInfo info = Weighting();
			//@ Apply info has weighted to this panel
			panels[p].nameText.text = info.name;
			panels[p].descText.text = info.description;
			panels[p].costText.text = "$" + info.cost;
			//Reset button onclick then add weighted upgrade onto it
			panels[p].upgradeButton.onClick.RemoveAllListeners();
			panels[p].upgradeButton.onClick.AddListener(() => {catalog.UseUpgrade(info.upgrade);});
		}
	}

    public UpgradeInfo Weighting()
	{
		//Get the total sum of all drops's weight
		float sum = 0; for (int d = 0; d < upgradeInfos.Length; d++)
		{
			//Added this drop weight to the sum
			sum += upgradeInfos[d].weight;
		}
		//Randomize sum
		sum = Random.Range(0, sum);
		//Go through all the drops
		for (int d = 0; d < upgradeInfos.Length; d++)
		{
			//If this drop weight take all the sum
			if(sum - upgradeInfos[d].weight <= 0)
			{
				//Return this drop
				return upgradeInfos[d];
			}
			//Sum now lost this drop weight if havent drop
			else sum -= upgradeInfos[d].weight;
		}
		//? This should never fail to return any drop
		return null;
	}
}