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


    // Start is called before the first frame update
    void Start()
    {
        _speed = 5;   
    }

    // Update is called once per frame
    void Update()
    {
        right = Input.GetAxisRaw("Horizontal");
        forward = Input.GetAxisRaw("Vertical");


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




}
