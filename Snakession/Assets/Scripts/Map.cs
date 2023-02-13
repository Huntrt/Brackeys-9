using System.Collections.Generic;
using UnityEngine;
using System;

public class Map : MonoBehaviour
{
	#region Set this class to singleton
	static Map _i; public static Map i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<Map>();
			}
			return _i;
		}
	}
	#endregion
	
	public PlotData[] plots;
	public LayerMask mapLayer;

	void Start()
	{
		//Generate plot from settings
		plots = plotSettings.GeneratePlot();
	}

	void OnDrawGizmos() 
	{
		//% Draw an grey small cube on each plot
		for (int p = 0; p < plots.Length; p++)
		{
			Gizmos.color = Color.gray;
			Gizmos.DrawCube(plots[p].position, Vector3.one * 0.2f);
		}
	}

	[SerializeField] PlotSettings plotSettings; [System.Serializable] public class PlotSettings
	{
		[SerializeField] Vector2 origin;
		[SerializeField] float spacing;
		[SerializeField] Vector2Int size;
		
		public PlotData[] GeneratePlot()
		{
			//List of plots gonne be return
			List<PlotData> plots = new List<PlotData>();
			//Go from the negative to the final x axis
			for (int x = (int)-size.x; x < size.x+1; x++) 
			//Go from the negative to the final y axis
			for (int y = (int)-size.y; y < size.y+1; y++)
			{
				//Use this x,y as coordinate
				Vector2Int coord = new Vector2Int(x,y);
				//Multiply coordinate at origin with spacing for position
				Vector2 pos = (origin + coord) * spacing;
				//Dont create this plot if it does not hit any part of the map
				if(!Physics2D.Raycast(pos, Vector2.zero, 0 , Map.i.mapLayer)) continue;
				//Create plot at coordinate and position then add into list
				plots.Add(new PlotData(plots.Count, coord, pos));
			}
			//Return the plot list as array
			return plots.ToArray();
		}
	}
}

[Serializable] public class PlotData
{
	public int index = -1;
	public Vector2Int coordinate;
	public Vector2 position;
	public bool empty = true;

	public PlotData(int index, Vector2Int coord, Vector2 pos)
	{
		this.index = index;
		this.coordinate = coord;
		this.position = pos;
	}
}