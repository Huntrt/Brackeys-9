using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Hurt : MonoBehaviour
{
    
	[SerializeField] Food foodCore;
	public int damage;

	void OnValidate() {foodCore = GetComponent<Food>();}
	void OnEnable() {foodCore.onConsume += BeginHurt;}
	void OnDisable() {foodCore.onConsume -= BeginHurt;}

	void BeginHurt()
	{
		//Hurt the snake with damage has get
		Snake.i.Hurt(damage);
	}
}