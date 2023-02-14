using System.Collections.Generic;
using UnityEngine;

public class SankeBody : MonoBehaviour
{
	[SerializeField] Snake snake;
	public LineRenderer line;
	[SerializeField] float spacing;
	[SerializeField] Transform head;
	[SerializeField] List<Vector3> bodyPart = new List<Vector3>();
	[SerializeField] List<Vector3> partPos = new List<Vector3>();

	void Start()
	{
		//Add the head's position
		partPos.Add(head.position);
	}

	void LateUpdate()
	{
		//Get distance between the head and the first part
		float dist = Vector2.Distance((Vector3)head.position, partPos[0]);
		//If distance far enough from spacing
		if(dist > spacing)
		{
			//Get the direction from the first part to head
			Vector3 dir = ((Vector3)head.position - partPos[0]).normalized;
			//Insert position from the first toward direction using spacing
			partPos.Insert(0, partPos[0] + dir * spacing);
			//Remove the old first part
			partPos.RemoveAt(partPos.Count - 1);
			//Get leftover distance
			dist -= spacing;
		}
		//Go through all the body part
		for (int p = 0; p < bodyPart.Count; p++)
		{
			//Lerping this part to it next part using progress of distance
			bodyPart[p] = Vector2.Lerp(partPos[p+1], partPos[p], dist/spacing);
		}
		//Add all the part to line position
		line.SetPositions(bodyPart.ToArray());
	}

	public void Grow()
	{
		//Grow another part 
		bodyPart.Add(partPos[partPos.Count-1]);
		partPos.Add(partPos[partPos.Count-1]);
		//Set line position the same as body part count
		line.positionCount = bodyPart.Count;
	}
}