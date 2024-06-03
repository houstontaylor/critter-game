using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicoEyes : MonoBehaviour
{

    [SerializeField] private Transform _eyeParent;
    [SerializeField] private Transform _eyeObject;

    [HideInInspector] public PlayerController _playerController;

    public void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    /// <summary>
    /// Hide eyes when this component is disabled.
    /// </summary>
    private void OnDisable()
    {
        _eyeParent.gameObject.SetActive(false);
        _eyeObject.gameObject.SetActive(false);
    }

    /// <summary>
    /// Show eyes when this component is enabled.
    /// </summary>
    private void OnEnable()
    {
        _eyeParent.gameObject.SetActive(true);
        _eyeObject.gameObject.SetActive(true);
    }

    private void Update()
    {
        Vector3 targetPos = _playerController.transform.position;
        _eyeParent.right = targetPos - transform.position;  // Rotate to point towards critter

        Vector3 rot = _eyeParent.rotation.eulerAngles;
        float value = Mathf.Clamp(rot.z + 90, 360 - 20, 360 + 20);  // Amount to rotate

        _eyeParent.transform.rotation = Quaternion.Euler(rot.x, rot.y, value);
    }

}
