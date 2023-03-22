using UnityEngine;

public class RedStaticBox : MonoBehaviour
{
    private YellowBoxController _yellowBoxController;
    
    private void OnEnable()
    {
        _yellowBoxController = FindObjectOfType<YellowBoxController>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        YellowMovingBox yellowMovingBox = other.gameObject.GetComponent<YellowMovingBox>();
        MainYellowMovingBox mainYellowMovingBox = other.gameObject.GetComponent<MainYellowMovingBox>();
        if (yellowMovingBox)
        {
            yellowMovingBox.transform.SetParent(null);
            _yellowBoxController.RemoveLostBox(yellowMovingBox.gameObject);
            _yellowBoxController.RefreshBoxTrail();
            PlayVibration();
            ClearAndFreezeBox(yellowMovingBox.GetComponent<Rigidbody>());
        }
        if (mainYellowMovingBox)
        {
            PlayVibration();
            mainYellowMovingBox.GameOver();
        }
    }

    private void ClearAndFreezeBox(Rigidbody boxRG)
    {
        boxRG.constraints = RigidbodyConstraints.None;
        boxRG.constraints = RigidbodyConstraints.FreezePositionX;
        boxRG.constraints = RigidbodyConstraints.FreezeRotation;
    }
    
    private void PlayVibration() => Handheld.Vibrate();
}
