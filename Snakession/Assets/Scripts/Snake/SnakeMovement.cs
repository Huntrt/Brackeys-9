using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
	public float initialMoveSpeed, moveSpeed;
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
		//Max allow move speed is 25
		moveSpeed = Mathf.Clamp(moveSpeed, 0f, 25f);
		//If has reached 90% of an step toward mouse
		if(Vector2.Distance(rb.position, mousePos) <= moveSpeed/90)
		{
			//Stop moving
			return;
		}
		//Max rotate speed allow is 50
		rotateSpeed = Mathf.Clamp(rotateSpeed, 0, 50);
		//Make object slowly facing toward mouse position
		head.up = Vector2.Lerp(head.up, (mousePos - rb.position).normalized, rotateSpeed * Time.fixedDeltaTime);
		//Get velocity by multiply speed with where object facing
	  	Vector2 velocity = head.up * moveSpeed;
		//Moving the player toward velocity has get
		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
	}
}