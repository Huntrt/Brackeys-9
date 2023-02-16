using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
	[SerializeField] float spacing;
	public LineRenderer line;
	public Transform head;
	public EdgeCollider2D[] bodyColliders;
	[SerializeField] public List<Vector2> bodyPart = new List<Vector2>();
	List<Vector2> partPos = new List<Vector2>();

	public void ResetPart()
	{
		//Clear all the body part
		bodyPart.Clear(); partPos.Clear();
		//Remove all line position
		line.positionCount = 0;
		//Add the head's position
		partPos.Add(head.position);
	}

	void LateUpdate()
	{
		//Only draw body if part pos for head exist
		if(partPos.Count > 0) DrawBody();
	}

	public void DrawBody()
	{
		//Get distance between the head and the first part
		float dist = Vector2.Distance((Vector2)head.position, partPos[0]);
		//While distance still bigger than spacing
		while (dist > spacing)
		{
			//Get the direction from the first part to head
			Vector2 dir = ((Vector2)head.position - partPos[0]).normalized;
			//Insert position from the first toward direction using spacing
			partPos.Insert(0, partPos[0] + dir * spacing);
			//Remove the old first part
			partPos.RemoveAt(partPos.Count - 1);
			//Get leftover distance
			dist -= spacing;
		}
		//Array for body part's 3d position
		Vector3[] bodyPart3d = new Vector3[bodyPart.Count];
		//Set the frist body part to be head's position
		if(bodyPart.Count > 0) {bodyPart[0] = head.position; bodyPart3d[0] = head.position;}
		//Go through all the body part
		for (int b = 1; b < bodyPart.Count; b++)
		{
			//Lerping this part to it previous part using progress of distance
			bodyPart[b] = Vector2.Lerp(partPos[b], partPos[b-1], dist/spacing);
			//Save this body part 3d position
			bodyPart3d[b] = bodyPart[b];
		}
		//Add all the part position to line
		line.SetPositions(bodyPart3d);
		//Set points for both trigger and collision collider
		bodyColliders[0].SetPoints(bodyPart);
		bodyColliders[1].SetPoints(bodyPart);
	}

	public void Grow()
	{
		//Grow another part 
		bodyPart.Add(partPos[partPos.Count-1]);
		partPos.Add(partPos[partPos.Count-1]);
		//Set line position the same as body part count
		line.positionCount = bodyPart.Count;
		//Draw body instantly after grow
		DrawBody();
	}
}