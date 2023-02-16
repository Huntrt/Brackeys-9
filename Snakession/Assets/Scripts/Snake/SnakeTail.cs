using UnityEngine;

public class SnakeTail : MonoBehaviour
{
	[SerializeField] Snake snake;
	public float maxHealthRequired;
	[SerializeField] Transform tail;

	void LateUpdate()
	{
		//If snake max health haven't meet requirement
		if(snake.maxHealth < maxHealthRequired)
		{
			//Deactive the tail
			tail.gameObject.SetActive(false);
		}
		else
		{
			//Active the tail
			tail.gameObject.SetActive(true);
			//Make the tail follow the last body part
			tail.position = snake.body.bodyPart[snake.body.bodyPart.Count-1];
		}
	}

	public void EatingTail()
	{
		SuccessionManager.i.BeginSuccession();
	}
}
