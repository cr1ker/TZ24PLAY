using UnityEngine;

public class MainYellowMovingBox : MonoBehaviour
{
    [SerializeField] private GameObject _failText;
    [SerializeField] private GameObject _tryAgainButton;
    private RoadGenerator _roadGenerator;
    private PlayerRagdollController _playerRagdollController;

    private void Start()
    {
        _roadGenerator = FindObjectOfType<RoadGenerator>();
        _playerRagdollController = FindObjectOfType<PlayerRagdollController>();
    }
    
    public void GameOver()
    {
        _roadGenerator.FinishLevel();
        _playerRagdollController.MakePhysicalPlayerStatus(false);
        SetFailTextActive();
        Invoke(nameof(SetButtonActive), 3);
    }

    private void SetButtonActive() => _tryAgainButton.SetActive(true);

    private void SetFailTextActive() => _failText.SetActive(true);
}
