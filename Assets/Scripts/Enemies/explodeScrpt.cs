using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class explodeScrpt : MonoBehaviour
{
    [SerializeField] private bool _active;

    // Start is called before the first frame update
    void Start()
    {
         _active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != new Vector3(-40, 0, 0) && !_active)
        {
            StartCoroutine(dieee());
        }
    }
    IEnumerator dieee()
    {
        _active = true;
        yield return new WaitForSeconds(0.5f);
        _active = false;
        Destroy(gameObject);
    }
}