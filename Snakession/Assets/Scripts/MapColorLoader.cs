using UnityEngine;

public class MapColorLoader : MonoBehaviour
{
	[SerializeField] MapColorData colorData;
	[SerializeField] SpriteRenderer[] wallRenderers;
	[SerializeField] SpriteRenderer[] groundRenderers;

    void OnEnable()
	{
		//Set camera color as back ground color
		Camera.main.backgroundColor = colorData.background;
		//Color all the walls renderer
		for (int w = 0; w < wallRenderers.Length; w++) wallRenderers[w].color = colorData.walls;
		//Color all the grounds renderer
		for (int g = 0; g < groundRenderers.Length; g++) groundRenderers[g].color = colorData.ground;
	}
}
