using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MineScript : MonoBehaviour
{
    [SerializeField] private GameObject _playerObj;
    [SerializeField] private GameObject _explosionn;
    [SerializeField] private float _viewRadius;
    [SerializeField] static public bool _activated;
    [SerializeField] private AudioSource _boooom;
    [SerializeField] private AudioSource _beep;
    [SerializeField] private float _distancePlayer;
    [SerializeField] private float _randomX;
    [SerializeField] private float _randomZ;
    [SerializeField] private Material _material;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private MeshRenderer _mesh2;
    // Start is called before the first frame update
    void Start()
    {
        // EMPIEZA DESACTIVADA, ACTIVA CORUTINA PARA SPAWNEAR EN UN LUGAR ALEATORIO
        _activated = false;
        StartCoroutine(appear());
    }

    // Update is called once per frame
    void Update()
    {
        _distancePlayer = Vector2.Distance(transform.position, _playerObj.transform.position);

        if (_distancePlayer < _viewRadius)
        {
            if (!_activated) explodeee();
        }
    }

    public void explodeee()
    {
        if (!_activated)
        {
            StartCoroutine(skibidi());
        }
        IEnumerator skibidi()
        {
            Debug.Log("going off");
            _activated = true;
            _beep.Play();
            _material.color = new Color(1f, 0f, 0f, 1f);
            yield return new WaitForSeconds(0.5f);
            _material.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.5f);
            _beep.Play();
            _material.color = new Color(1f, 0f, 0f, 1f);
            yield return new WaitForSeconds(0.5f);
            _material.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.5f);
            
            Instantiate(_explosionn, transform.position, Quaternion.identity);
            _particle.Play();
            _boooom.Play();
            _mesh.enabled = false;
            _mesh2.enabled = false;

            if (_distancePlayer < _viewRadius)
            {
                Salud.health = Salud.health - 50;
            }
            yield return new WaitForSeconds(0.5f);
            transform.position = new Vector3(70, 0.3f, 70);
            _mesh.enabled = true;
            _mesh2.enabled = true;
            _activated = false;
            yield return new WaitForSeconds(5);
            _randomX = UnityEngine.Random.Range(0, 21);
            _randomZ = UnityEngine.Random.Range(-7, 7);
            transform.position = new Vector3(_randomX, 0.3f, _randomZ);

        }
    }


    IEnumerator appear()
    {
        yield return new WaitForSeconds(5);
        _randomX = UnityEngine.Random.Range(0, 21);
        _randomZ = UnityEngine.Random.Range(-7, 7);
        transform.position = new Vector3(_randomX, 0.3f, _randomZ);

    }
}
