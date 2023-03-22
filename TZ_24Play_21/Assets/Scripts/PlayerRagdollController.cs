using UnityEngine;

public class PlayerRagdollController : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _allPlayerRigibodies;
    [SerializeField] private Animator _playerAnimator;
    
    private void Awake()
    {
        MakePhysicalPlayerStatus(true);
    }
    
    public void MakePhysicalPlayerStatus(bool status)
    {
        _playerAnimator.enabled = status;
        for (int i = 0; i < _allPlayerRigibodies.Length; i++)
        {
            _allPlayerRigibodies[i].isKinematic = status;
        }
    }
}
