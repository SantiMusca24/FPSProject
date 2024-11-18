using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MoveChar : MonoBehaviour
{
    //TP2 LorenzoMarmol(SETTERS AND GETTERS)
    [SerializeField]
    private float _speed = 5;

    [SerializeField]
    public float Speed
    {
        get { return _speed; }
        set
        {
            if (value > 5)
            {
                _speed = 5;
            }
            else
            {
                _speed = value;
            }
        }
    }


    public Rigidbody _rigidbod;
    public GameObject _camera;
    private Animator _anim;
    private Vector3 _dir1 = new();
    private Vector3 _dir2 = new();

    private float right, forward;

    [SerializeField] private Transform _orientation;
    public float groundDrag;
    public float jumpForce;
    public float poweredJumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    public KeyCode jumpkey = KeyCode.Space;
    public KeyCode powerJumpKey = KeyCode.C; // Tecla para activar el doble salto
    private bool powerJumpActive = false;
    public string sceneName;
    public GameObject mask;
    
    //TP2 Santiago Muscatiello (Delegates)
    public delegate void MovementDelegate(); 
    public delegate void JumpDelegate(float force);
    public MovementDelegate movementDelegate = delegate { };
    public JumpDelegate jumpDelegate = delegate { };

    //TP2 Santiago Muscatiello(Diccionario)
    private int points;
    [SerializeField] private EnemyScoreManager lootData;
    [SerializeField] private TMP_Text pointsText;

   
    
    private void Awake()
    {
        movementDelegate += Move;
        jumpDelegate += Jump;
    }
    void Start()
    {
      
        Speed = 5;
        readyToJump = true;
        pointsText.text = "Points: 0";
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("SampleScene");
        }

        right = Input.GetAxisRaw("Horizontal");
        forward = Input.GetAxisRaw("Vertical");
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        if (grounded)
        {
            _rigidbod.drag = groundDrag;

            if (Input.GetKey(jumpkey) && readyToJump)
            {
                readyToJump = false;

                if (powerJumpActive)
                {
                    jumpDelegate?.Invoke(poweredJumpForce); 
                    powerJumpActive = false;
                    Debug.Log("Salto con poder realizado");
                }
                else
                {
                    jumpDelegate?.Invoke(jumpForce); 
                }

                Invoke(nameof(ResetJump), jumpCooldown);
            }


        }
        else
        {
            _rigidbod.drag = 0;
        }
        if (Input.GetKeyDown(powerJumpKey) && grounded)
        {
            powerJumpActive = true;
            Debug.Log("Poder de salto activado");
        }
        if (Gas.maskOn == false && Input.GetKeyDown(KeyCode.M))
        {
            mask.SetActive(true);
            Gas.maskOn = true;

        }
        else if (Gas.maskOn == true && Input.GetKeyDown(KeyCode.M))
        {
            Gas.maskOn = false;
            mask.SetActive(false);
        }
        

    }

    private void FixedUpdate()
    {
        //if (right != 0 && !Physics.Raycast(transform.position, Vector3.right, rayDist, wallLayerMask)

        transform.rotation = _orientation.rotation;

        movementDelegate();
    }


    public void Move()
    {
        _dir1 = (transform.right * right).normalized;
        _dir2 = (transform.forward * forward).normalized;


        _rigidbod.MovePosition(transform.position + (_dir1 + _dir2) * _speed * Time.fixedDeltaTime); // TERCER MODO EXPLICADO, ES EL QUE USA EL PROFE
                                                                                                     //_rigidbod.MovePosition(transform.position + _dir2 * _speed * Time.fixedDeltaTime); // TERCER MODO EXPLICADO, ES EL QUE USA EL PROFE




    }

    private void Jump(float jumpForce)
    {
        _rigidbod.velocity = new Vector3(_rigidbod.velocity.x, 0f, _rigidbod.velocity.z);

        _rigidbod.AddForce(transform.up * jumpForce, ForceMode.Impulse);


    }
    private void ResetJump()
    {
        readyToJump = true;


    }

    //TP2 Santiago Muscatiell(Diccionario)
    public void GetLoot(EnemyScoreManager.LootData lootData)
    {
        points += lootData.points;
        pointsText.text = "Points:" + points;
        Debug.Log("ganaste"+points);

    }
}
