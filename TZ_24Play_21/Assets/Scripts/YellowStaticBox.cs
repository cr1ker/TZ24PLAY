using UnityEngine;

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
            yellowBoxController.OnTakeYellowBox();
            Destroy(gameObject);
        }
    }
}
