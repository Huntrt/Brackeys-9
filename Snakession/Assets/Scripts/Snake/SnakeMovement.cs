using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
	public float moveSpeed;
	public float rotateSpeed;
	[SerializeField] Transform head;
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
		if(Vector2.Distance(rb.position, mousePos) <= moveSpeed/80)
		{
			//Stop moving
			return;
		}
		//Make object slowly facing toward mouse position
		head.up = Vector3.Slerp(head.up, (mousePos - rb.position), rotateSpeed * Time.deltaTime);
		//Get velocity by multiply speed with where object facing
	  	Vector2 velocity = head.up * moveSpeed;
		//Moving the player toward velocity has get
		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
	}
}