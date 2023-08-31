using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    public Vector2 inputVec;
    public Scanner scanner;
    public Hand[] hands;

    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    private Animator animator;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    void Start()
    {

    }


    void Update()
    {
        // Old Input System
        //inputVec.x = Input.GetAxisRaw("Horizontal");
        //inputVec.y = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        // Old version
        //Vector2 nextVec = inputVec.normalized * speed * Time.deltaTime;

        Vector2 nextVec = inputVec * speed * Time.deltaTime; 
        rigid.MovePosition(rigid.position + nextVec);

    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    private void OnMove(InputValue inputValue)
    {
        inputVec = inputValue.Get<Vector2>();
    }
}
