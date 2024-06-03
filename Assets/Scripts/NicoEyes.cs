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

    private void Update()
    {
        Vector3 targetPos = _playerController.transform.position;
        _eyeParent.right = targetPos - transform.position;
        Vector3 rot = _eyeParent.rotation.eulerAngles;
        float value = Mathf.Clamp(rot.z + 90, 360 - 50, 360 + 50);
        _eyeParent.transform.rotation = Quaternion.Euler(rot.x, rot.y, value);
        _eyeObject.transform.rotation = Quaternion.Euler(rot.x, rot.y, -value * 2);
    }

}
