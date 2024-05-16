using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReset : MonoBehaviour
{

    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    // TODO: This is actually stupid just delete it (this is for the playtest)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _playerController.Respawn();
        }
    }

}
