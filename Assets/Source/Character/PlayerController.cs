using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "MazeDecorationTilemap") {
            Tilemap map = collision.GetComponentInParent<Tilemap>();
            Vector3Int removePos = new Vector3Int((int)Math.Ceiling(transform.position.x), (int)Math.Ceiling(transform.position.y), 0);
            MazeBlock block = MazeBuilder.GetSingleTon().GetBlockAt(removePos);
            if (block != null && block is MB_DoorSwitch) {
                Debug.Log("Activate");
                ((MB_DoorSwitch)block).Activate();
            }
        }
    }
}
