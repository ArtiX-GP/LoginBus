using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Rigidbody2D rigidBody;

    private Animator animator;

    private Vector2 velocity;

    private bool isWatchingLeft;

    //private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;
        animator.SetBool(Animator.StringToHash("IsRunning"), velocity.magnitude > 0);
    }

    void FixedUpdate() {
        rigidBody.MovePosition(rigidBody.position + velocity * Time.deltaTime);
        if (velocity.x != 0) {
            if ((velocity.x < 0 && !isWatchingLeft) || (velocity.x > 0 && isWatchingLeft)) {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            isWatchingLeft = velocity.x < 0;
        }
    }
}
