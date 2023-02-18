using System.Collections.Generic;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
	#region Set this class to singleton
	static TextPopup _i; public static TextPopup i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<TextPopup>();
			}
			return _i;
		}
	}
	#endregion

    [SerializeField] List<string> popupBatchs = new List<string>();
	[SerializeField] GameObject popupGUI;
	[SerializeField] Transform worldCanvas;

	public void Popuping(string text)
	{
		//Add the given text to current popup batch
		popupBatchs.Add(text);
		//Cancel any batch currently send
		CancelInvoke();
		//Begin send all the popup has batch
		Invoke("SendBatch", 0);
	}

	void SendBatch()
	{
		//Added all the popup text batch to one line of string
		string batch = ""; for (int p = 0; p < popupBatchs.Count; p++) {batch += " " + popupBatchs[p];}
		//Create the popup gui at snake head
		GameObject gui = Instantiate(popupGUI, Snake.i.body.head.position, Quaternion.identity);
		//Parent the gui to world canvas
		gui.transform.SetParent(worldCanvas);
		//Apply the batch string to gui text at object -> background -> text
		gui.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = batch;
		//Clear this batch
		popupBatchs.Clear();
	}
}