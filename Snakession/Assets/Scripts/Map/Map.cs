using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;
using TMPro;

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
	
	public int level;
	[SerializeField] WeightDrop[] mapTypes;
	public Transform currentMap, foodGroup, trapGroup;
	public LayerMask mapLayer;
	[SerializeField] PlotSettings plotSettings;
	public List<PlotData> emptyPlots = new List<PlotData>();
	public List<PlotData> filledPlots = new List<PlotData>();
	public UnityEvent onMapCreate, onMapClear;
	[Header("GUI")]
	[SerializeField] TextMeshProUGUI levelText;


	void Start()
	{
		ClearMap();
		CreateMap();
	}

	public void ClearMap()
	{
		//Destroy all of the food that get group
		for (int c = foodGroup.childCount - 1; c >= 0 ; c--) Destroy(foodGroup.GetChild(c).gameObject);
		//Destroy all of the trap that get group
		for (int c = trapGroup.childCount - 1; c >= 0 ; c--) Destroy(trapGroup.GetChild(c).gameObject);
		//Clear all the plot
		emptyPlots.Clear(); filledPlots.Clear();
		//Destroy the current map if it exist
		if(currentMap != null) Destroy(currentMap.gameObject);
		//Map has been clear
		onMapClear.Invoke();
	}

	public void CreateMap()
	{
		//Create new current weighted map
		currentMap = Instantiate(WeightSystem.Weighting(mapTypes).obj).transform;
		//Generate new plot
		emptyPlots = plotSettings.GeneratePlot();
		//Map has been created
		onMapCreate.Invoke();
		//Display the level counter
		levelText.text = "LEVEL " + level;
	}

	public GameObject PlaceObject(Vector2 coord, GameObject obj)
	{
		//Find the empty plot
		PlotData empty = SearchPlot(coord, emptyPlots);
		//Failed to place if given plot is not empty
		if(empty == null) return null;
		//Create given object at this empty plot position
		empty.obj = Instantiate(obj, empty.position, obj.transform.rotation);
		//Empty plot has been filled
		filledPlots.Add(empty);
		//Remove the plot got fill from empty
		emptyPlots.Remove(empty);
		//Succesfully place given obj
		return empty.obj;
	}

	public bool PluckObject(Vector2 coord)
	{
		//Find the filled plot
		PlotData filled = SearchPlot(coord, filledPlots);
		//Failed to pluck if given plot is not fill
		if(filled == null) return false;
		//Destroy the object in filled plot
		Destroy(filled.obj);
		//Filled plot are now empty
		emptyPlots.Add(filled);
		//Remove the plot got empty from fill
		filledPlots.Remove(filled);
		//Successfully pluck obj at given coordinate
		return true;
	}

	public PlotData SearchPlot(Vector2 coord, List<PlotData> plotList)
	{
		//Go through all the given plot list
		for (int e = 0; e < plotList.Count; e++)
		{
			//Return plot inside given list that has the same coordinate as given
			if(plotList[e].coordinate == coord) return plotList[e];
		}
		//Return nothing if cant find in given plot
		return null;
	}

	// void OnDrawGizmos() 
	// {
	// 	//test Draw an grey small cube on each plot
	// 	for (int p = 0; p < emptyPlots.Count; p++)
	// 	{
	// 		Gizmos.color = new Color(1f,1f,1f,0.1f); //transparent white
	// 		Gizmos.DrawWireCube(emptyPlots[p].position, Vector3.one * 0.2f);
	// 	}
	// }

	[System.Serializable] public class PlotSettings
	{
		[SerializeField] Vector2 origin;
		[SerializeField] float spacing;
		[SerializeField] Vector2Int size;
		
		public List<PlotData> GeneratePlot()
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
			return plots;
		}
	}
}

[Serializable] public class PlotData
{
	public int index = -1;
	public Vector2Int coordinate;
	public Vector2 position;
	public GameObject obj;

	public PlotData(int index, Vector2Int coord, Vector2 pos)
	{
		this.index = index;
		this.coordinate = coord;
		this.position = pos;
	}
}