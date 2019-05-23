using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Start()
    {
        Cursor.visible   = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) {
            PlayerPrefs.SetInt("difficulty", 1);
            SceneManager.LoadScene("Main");
        }

        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) {
            PlayerPrefs.SetInt("difficulty", 2);
            SceneManager.LoadScene("Main");
        }

        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) {
            PlayerPrefs.SetInt("difficulty", 3);
            SceneManager.LoadScene("Main");
        }

        if (Input.GetButton("Cancel")) {
            Application.Quit();
        }
    }
}
