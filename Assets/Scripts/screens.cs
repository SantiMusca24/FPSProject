using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class screens : MonoBehaviour
{

    [SerializeField] private GameObject _menu, _controls;

    // Start is called before the first frame update
    void Start()
    {
        if (_controls != null)
        {
            _menu.SetActive(true);
            _controls.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Tutorial()
    {
        _menu.SetActive(false);
        _controls.SetActive(true);
    }
    public void Back()
    {
        _menu.SetActive(true);
        _controls.SetActive(false);
    }
    public void Retry()
    {
        SceneManager.LoadScene("Nivel 1");
    }

    public void ExitGame()
    {

#if UNITY_EDITOR
        
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

}
