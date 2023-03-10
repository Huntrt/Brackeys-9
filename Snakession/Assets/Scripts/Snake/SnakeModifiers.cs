using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SnakeModifiers : MonoBehaviour
{
	public FoodMaxHPBonus foodMaxHPBonus; [System.Serializable] public class FoodMaxHPBonus
	{
   		public int flex, vegan, meat;
		public int GetBonus(bool isVegan)
		{
			//Gain bonus flex amount first then get bonus from vegan or meat 
			return flex + ((isVegan) ? vegan : meat);
		}
	}
	
	public FoodMaxHPAdditive foodMaxHPAdditive; [System.Serializable] public class FoodMaxHPAdditive
	{
		public float flex, vegan, meat;
		public int GetAdditive(int amount, bool isVegan)
		{
			//Gain percented amount amount given with flex and even more with vegan or meat
			return (int)(((flex + ((isVegan) ? vegan : meat))/100f) * amount);
		}
	}

	public FreshBonus freshBonus; [System.Serializable] public class FreshBonus
	{
   		public int flex;
		public int GetBonus() {return flex;}
	}

	public FreshAdditive freshAdditive; [System.Serializable] public class FreshAdditive
	{
   		public float flex;
		public float GetAdditive(float amount) {return (flex/100f) * amount;}
	}

	public MoveSpeedBoost moveSpeedBoost; [System.Serializable] public class MoveSpeedBoost
	{
		[SerializeField] SnakeMovement movement;
		public List<BoostData> boosts = new List<BoostData>();
		[System.Serializable] public class BoostData : BuffData
		{
			public float additive;

			public BoostData(string source, float duration, float additive) : base(source, duration)
			{
				this.source = source;
				this.duration = duration;
				this.additive = additive;
			}
		}

		public void AddBoost(float additive, float duration, string source)
		{
			//If this speed boost has long duration then popup regarless of duplicate
			if(duration > 1) SpeedPopup(additive);
			//Go through all the boost buff to check if the source given are already boost
			for (int b = 0; b < boosts.Count; b++) if(boosts[b].source == source)
			{
				//Renew this source buff timer if it already boosting
				boosts[b].timer -= boosts[b].timer; return;
			}
			//Add an new booste buff with given source and duration
			boosts.Add(new BoostData(source, duration, additive));
			//Only popup when new boost wince duration are too small
			if(duration <= 1) SpeedPopup(additive);
		}

		public void Boosting()
		{
			//The total additive of all boost
			float totalAdditive = 0;
			//Go through all the active boost
			for (int b = 0; b < boosts.Count; b++)
			{
				//Save this boost
				BoostData boost = boosts[b];
				//If this boost still going
				if(boost.BuffGoing())
				{
					//Save the additive of this buff
					totalAdditive += boost.additive;
				}
				//Remove this boost if it ended
				else boosts.RemoveAt(b);
			}
			//Apply initial move speed that has are additive
			movement.moveSpeed = movement.InitialMoveSpeed + ((1*(totalAdditive/100f)) * movement.InitialMoveSpeed);
		}

		void SpeedPopup(float additive)
		{
			//Add an plus sign if additive is increase
			string additivePopup = (additive > 0) ? "+" + additive : additive.ToString();
			//Popup the speed additive "+20% Speed"(Blue)
			TextPopup.i.Popuping("<#1fa5ff>" + additivePopup + "% " + "Speed</color>");
		}
	}

	[System.Serializable] public class BuffData 
	{
		public string source;
		public float duration;
		public float timer;

		public BuffData(string source, float duration)
		{
			this.source = source;
			this.duration = duration;
		}

		public bool BuffGoing()
		{
			//Counting timer 
			timer += Time.deltaTime;
			//If timer reached duration
			if(timer >= duration)
			{
				//Buff has ended
				return false;
			}
			else
			{
				//Buff still going
				return true;
			}
		}
	}

	void Update()
	{
		moveSpeedBoost.Boosting();
	}
}