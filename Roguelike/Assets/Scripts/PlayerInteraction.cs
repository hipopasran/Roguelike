using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerController controller;

    private void Awake()
    {
        controller = GetComponentInParent<PlayerController>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Finish")
        {
            controller.enabled = false;
            GameManager.Instance.Finish();
        }
    }
}
