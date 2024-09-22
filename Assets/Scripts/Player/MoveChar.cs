using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChar : MonoBehaviour
{


    [SerializeField] private float _speed = 5;

    public Rigidbody _rigidbod;
    public GameObject _camera;
    private Animator _anim;
    private Vector3 _dir1 = new();
    private Vector3 _dir2 = new();

    private float right, forward;

    [SerializeField] private Transform _orientation;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    public KeyCode jumpkey = KeyCode.Space;
    // Start is called before the first frame update
    void Start()
    {
        _speed = 5;
        readyToJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        right = Input.GetAxisRaw("Horizontal");
        forward = Input.GetAxisRaw("Vertical");
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        if (grounded) 
        {
            _rigidbod.drag = groundDrag;
        }
        else
        {
            _rigidbod.drag = 0;
        }

        if (Input.GetKey(jumpkey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Debug.Log("salto");
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void FixedUpdate()
    {
        //if (right != 0 && !Physics.Raycast(transform.position, Vector3.right, rayDist, wallLayerMask)
        Move();
        transform.rotation = _orientation.rotation;


    }


    public void Move()
    {
        _dir1 = (transform.right * right).normalized;
        _dir2 = (transform.forward * forward).normalized;


        _rigidbod.MovePosition(transform.position + (_dir1 + _dir2) * _speed * Time.fixedDeltaTime); // TERCER MODO EXPLICADO, ES EL QUE USA EL PROFE
        //_rigidbod.MovePosition(transform.position + _dir2 * _speed * Time.fixedDeltaTime); // TERCER MODO EXPLICADO, ES EL QUE USA EL PROFE




    }

    private void Jump()
    {
        _rigidbod.velocity = new Vector3(_rigidbod.velocity.x,0f,_rigidbod.velocity.z);

        _rigidbod.AddForce(transform.up * jumpForce, ForceMode.Impulse);


    }
    private void ResetJump()
    {
        readyToJump = true;


    }
}
