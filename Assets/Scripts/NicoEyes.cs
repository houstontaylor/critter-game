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
        // Calculate direction vector from eyes to player
        Vector3 direction = (_playerController.transform.position - _eyeParent.position).normalized;


        // Calculate the local position offset for the eye objects
        Vector3 localOffset = direction;

        // Add a downward offset to the y component of the localOffset
        localOffset.y -= 2.9f; // You can adjust the value to control the amount of downward offset

        // Apply the local position offset to the eye objects
        _eyeObject.localPosition = localOffset;
    }

}
