using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    private Rigidbody rigidBody;
    public Vector2 jumpForce = new Vector2(0, 450);

    public float maxSpeed = 3.0f;
    public float speed = 50.0f;
    private float xMove;
    private bool shouldJump;

    private bool onGround;
    private float yPrevious;

    private bool collidingWall;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        shouldJump = false;
        xMove = 0.0f;
        onGround = false;
        yPrevious = Mathf.Floor(transform.position.y);
        collidingWall = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //플레이어를 왼쪽과 오늘쪽으로 이동시킨다.
        Movement();

        // 카메라를 플레이어의 위치 중앙에 설정한다.
        // 카메라의 원래 깊이를 유지한다.
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
	}

    // 땅에 닿아있지 않은데 무언가에 부딛혔다면 벽이나 천장이다.
    private void OnCollisionStay(Collision collision)
    {
        if(!onGround)
        {
            collidingWall = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collidingWall = false;
    }

    private void Update()
    {
        // 플레이어가 땅에 있는지 체크한다. 
        CheckGrounded();

        // 점프 버튼을 누르면 플레이어를 점프시킨다.
        Jumping();
    }

    void Movement()
    {
        xMove = Input.GetAxis("Horizontal");

        if(collidingWall && !onGround)
        {
            xMove = 0;
        }

        if(xMove != 0)
        {
            float xSpeed = Mathf.Abs(xMove * rigidBody.velocity.x);
            if(xSpeed < maxSpeed)
            {
                Vector3 movementForce = new Vector3(1, 0, 0);
                movementForce *= xMove * speed;

                RaycastHit hit;
                if(!rigidBody.SweepTest(movementForce, out hit, 0.05f))
                    rigidBody.AddForce(movementForce);
            }

            if(Mathf.Abs(rigidBody.velocity.x) > maxSpeed)
            {
                Vector2 newVelocity;
                newVelocity.x = Mathf.Sign(rigidBody.velocity.x) * maxSpeed;
                newVelocity.y = rigidBody.velocity.y;
                rigidBody.velocity = newVelocity;
            }

        }
        else
        {
            // 움직이고 있지 않다면 약간 느려진다.  
            Vector2 newVelocity = rigidBody.velocity;

            newVelocity.x *= 0.9f;
            rigidBody.velocity = newVelocity;
        }
    }

    void Jumping()
    {
        if(Input.GetButtonDown("Jump"))
        {
            shouldJump = true;
        }

        if(shouldJump && onGround)
        {
            rigidBody.AddForce(jumpForce);
            shouldJump = false;
        }
    }

    void OnDrawGizmos()
    {
        if(rigidBody != null)
            Debug.DrawLine(transform.position, transform.position + rigidBody.velocity, Color.red);
    }

    void CheckGrounded()
    {
        // 플레이어가 중심으로부터 
        // 맨 아래보다 조금 더 밑에 지점에서 충돌하는지 체크
        float distance = (GetComponent<CapsuleCollider>().height / 2 * this.transform.localScale.y) + 01f;
        Vector3 floorDirection = transform.TransformDirection(-Vector3.up);
        Vector3 origin = transform.position;

        if(!onGround)
        {
            // 바로 아래 무언가 있는지 체크한다.
            if(Physics.Raycast(origin, floorDirection, distance))
            {
                onGround = true;
            }
        }
        // 현재 땅에 있다면 떨어지고 있는 중인가? 점프중인가?
        else if(Mathf.Floor(transform.position.y) != yPrevious)
        {
            onGround = false;
        }

        // 현재 위치는 이전 프레임의 다음 프레임이다.
        yPrevious = Mathf.Floor(transform.position.y);

    }
}
