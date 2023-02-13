using UnityEngine;

public class FollowMouse : MonoBehaviour
{
	public float speed;
	[SerializeField] Rigidbody2D rb;
	Vector2 mousePos;
	Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
		//Get mouse position
       	mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

	void FixedUpdate()
	{
		//If has reached 0.1 of an step toward mouse
		if(Vector2.Distance(rb.position, mousePos) <= speed/100)
		{
			//Stop moving
			return;
		}
		//Apply speed to normalized direction from this object to mouse for velocity
	  	Vector2 velocity = (mousePos - (Vector2)rb.position).normalized * speed;
		//Moving the player toward mouse direction
		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
		
	}
}