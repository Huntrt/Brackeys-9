using UnityEngine;

public class Food_Heal : MonoBehaviour
{
	[SerializeField] Food foodCore;
	public int healAmount;
	
	void OnValidate() {foodCore = GetComponent<Food>();}
	void OnEnable() {foodCore.onConsume += BeginHeal;}
	void OnDisable() {foodCore.onConsume -= BeginHeal;}

	void BeginHeal()
	{
		//Heal snake for the amount has set
		Snake.i.Heal(healAmount);
	}
}