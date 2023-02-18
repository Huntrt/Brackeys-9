using UnityEngine;

public class Trap_SpeedAlter : MonoBehaviour
{
	public string sourceName;
    public float speedAlterAdditive;
	public float decayDuration;

	void OnTriggerStay2D(Collider2D other) 
	{
		//If player stay in the trap
		if(other.CompareTag("Player"))
		{
			//Apply speed alter that slowly decay to move speed boost
			Snake.i.mod.moveSpeedBoost.AddBoost(speedAlterAdditive, decayDuration, sourceName);
		}
	}
}
