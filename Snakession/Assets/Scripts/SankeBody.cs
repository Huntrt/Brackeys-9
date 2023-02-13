using System.Collections.Generic;
using UnityEngine;

public class SankeBody : MonoBehaviour
{
	public LineRenderer line;
	public Transform head;
	public int length;
	public float width;
	public float segmentSpeed;
	public float minimumSpacing;
	public Vector3[] segmentPos; Vector3[] segmentVel;

    void Start()
    {
		line.positionCount = length;
        segmentPos = new Vector3[length];
        segmentVel = new Vector3[length];
    }
	
    void Update()
    {
		//Segment 0 start at the head
        segmentPos[0] = head.position;
		//Go through all the segment after head
		for (int s = 1; s < segmentPos.Length; s++)
		{
			//Slowly move this segment toward previous segment in head direction with speed has set
			segmentPos[s] = Vector3.SmoothDamp(segmentPos[s], segmentPos[s-1] - head.right * minimumSpacing, ref segmentVel[s], segmentSpeed);
		}
		//Add all segment to line position
		line.SetPositions(segmentPos);
    }
}