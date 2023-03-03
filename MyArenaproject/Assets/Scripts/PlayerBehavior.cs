using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f;

    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;
    public GameObject bullet;
    public float bulletSpeed = 100f;

    private float vInput;
    private float hInput;
    private Rigidbody _rb;
    private CapsuleCollider _col;
    private GameBehavior _gameManager;
    public delegate void JumpingEvent();

    // 2
    public event JumpingEvent playerJump;

    void Start()
    {
        // 3
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;


        /* 4
        this.transform.Translate(Vector3.forward * vInput * 
        Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
    }
    // 1
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 3
            GameObject newBullet = Instantiate(bullet,
               this.transform.position + new Vector3(1, 0, 0),
                  this.transform.rotation) as GameObject;

            // 4
            Rigidbody bulletRB =
                newBullet.GetComponent<Rigidbody>();

            // 5
            bulletRB.velocity = this.transform.forward *
                                           bulletSpeed;
            playerJump();
        }
    
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * jumpVelocity,
                ForceMode.Impulse);
        }
        // 2
        Vector3 rotation = Vector3.up * hInput;

        // 3
        Quaternion angleRot = Quaternion.Euler(rotation *
            Time.fixedDeltaTime);

        // 4
        _rb.MovePosition(this.transform.position +
            this.transform.forward * vInput * Time.fixedDeltaTime);

        // 5
        _rb.MoveRotation(_rb.rotation * angleRot);



        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 3
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }

    }
    private bool IsGrounded()
    {
        // 7
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x,
            _col.bounds.min.y, _col.bounds.center.z);

        // 8
        bool grounded = Physics.CheckCapsule(_col.bounds.center,
           capsuleBottom, distanceToGround, groundLayer,
              QueryTriggerInteraction.Ignore);

        // 9
        return grounded;

    }
    void OnCollisionEnter(Collision collision)
    {
        // 4
        if (collision.gameObject.name == "Enemy")
        {
            // 5
            _gameManager.HP -= 1;
        }
    }
}