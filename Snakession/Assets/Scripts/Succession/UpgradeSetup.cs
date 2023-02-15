using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UpgradeSetup : MonoBehaviour
{
	[SerializeField] UpgradeCatalog catalog;
	[SerializeField] UpgradeWeight[] upgradeDrops;
	[System.Serializable] public class UpgradeWeight
	{
		public UpgradeInfo info;
		public float weight;
	}

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
			UpgradeInfo info = Weighting().info;
			//@ Apply info has weighted to this panel
			panels[p].nameText.text = info.name;
			panels[p].descText.text = info.description;
			panels[p].costText.text = "$" + info.cost;
			//Reset button onclick then add weighted upgrade onto it
			panels[p].upgradeButton.onClick.RemoveAllListeners();
			panels[p].upgradeButton.onClick.AddListener(() => {catalog.UseUpgrade(info.upgrade);});
		}
	}

    public UpgradeWeight Weighting()
	{
		//Get the total sum of all drops's weight
		float sum = 0; for (int d = 0; d < upgradeDrops.Length; d++)
		{
			//Added this drop weight to the sum
			sum += upgradeDrops[d].weight;
		}
		//Randomize sum
		sum = Random.Range(0, sum);
		//Go through all the drops
		for (int d = 0; d < upgradeDrops.Length; d++)
		{
			//If this drop weight take all the sum
			if(sum - upgradeDrops[d].weight <= 0)
			{
				//Return this drop
				return upgradeDrops[d];
			}
			//Sum now lost this drop weight if havent drop
			else sum -= upgradeDrops[d].weight;
		}
		//? This should never fail to return any drop
		return null;
	}
}