using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
	[SerializeField] Snake snake;
	[SerializeField] float spacing;
	public LineRenderer line;
	public Transform head;
	public EdgeCollider2D[] bodyColliders;
	[HideInInspector] public List<Vector2> bodyPart = new List<Vector2>();
	List<Vector2> partPos = new List<Vector2>();

	void Start()
	{
		//Add the head's position
		partPos.Add(head.position);
	}

	void FixedUpdate()
	{
		DrawBody();
	}

	//bug: body will struggle to follow head with fast speed;
	void DrawBody()
	{
		//Get distance between the head and the first part
		float dist = Vector2.Distance((Vector2)head.position, partPos[0]);
		//If distance far enough from spacing
		if(dist > spacing)
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
		//Go through all the body part
		for (int b = 0; b < bodyPart.Count; b++)
		{
			//Lerping this part to it next part using progress of distance
			bodyPart[b] = Vector2.Lerp(partPos[b+1], partPos[b], dist/spacing);
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