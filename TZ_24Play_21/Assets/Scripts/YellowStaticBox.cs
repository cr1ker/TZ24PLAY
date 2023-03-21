using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class YellowStaticBox : MonoBehaviour
{
    private int _indexOfLastBoxListElement;
    private YellowBoxController yellowBoxController;

    private void OnEnable()
    {
        yellowBoxController = FindObjectOfType<YellowBoxController>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        YellowMovingBox yellowMovingBox = other.attachedRigidbody.GetComponent<YellowMovingBox>();
        if (yellowMovingBox)
        {
            //yellowMovingBox.GetIndexOfLastBoxListElement(_indexOfLastBoxListElement);
            yellowBoxController.OnTakeYellowBox();
            Destroy(gameObject);
        }
    }
}
