using UnityEngine;

public class Food_Boost : MonoBehaviour
{
	[SerializeField] Food foodCore;
	[SerializeField] string sourceName;
	public float boostAdditive;
	public float boostDuration;

	void OnValidate() {foodCore = GetComponent<Food>();}
	void OnEnable() {foodCore.onConsume += BeginBoost;}
	void OnDisable() {foodCore.onConsume -= BeginBoost;}

	void BeginBoost()
	{
		//Add an new speed boost with set value
		Snake.i.mod.moveSpeedBoost.AddBoost(boostAdditive, boostDuration, sourceName);
	}
}
