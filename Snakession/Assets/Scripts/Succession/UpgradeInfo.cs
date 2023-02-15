using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade Info", menuName = "Upgrade Info", order = 3)]
public class UpgradeInfo : ScriptableObject
{
	public UpgradeCatalog.Upgrades upgrade;
	public int cost;
	public Sprite icon;
	[TextArea(5,20)] public string description;

	void OnValidate() 
	{
		//Set 'SO' name as upgrade is assign
		name = upgrade.ToString();
	}
}