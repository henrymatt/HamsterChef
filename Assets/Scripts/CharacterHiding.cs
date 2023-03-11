using UnityEngine;

public class CharacterHiding : MonoBehaviour
{
    private bool _isHiding = false;
    private Vector3 _hidePosition;
    [SerializeField] private int _maxInventoryToHide = 3;

    private void Update()
    {
        CheckUnhide();
    }
    
    public void Hide(Vector3 holePosition)
    {
        if (GameManager.Instance.CharacterCheekPouches.GetCurrentInventoryAmount() > _maxInventoryToHide)
        {
            EventHandler.CallDidFailAttemptToHide();
            return;
        }
        
        _hidePosition = holePosition;
        _isHiding = true;
        gameObject.SetActive(false);
        transform.position = new Vector3(holePosition.x, -1.4f, holePosition.z);
        gameObject.SetActive(true);
        GameManager.Instance.CharacterMovement.DisableMovement();
        EventHandler.CallDidHideEvent();
    }

    public void Unhide()
    {
        gameObject.SetActive(false);
        transform.position = new Vector3(_hidePosition.x, 0, _hidePosition.z);
        gameObject.SetActive(true);
        GameManager.Instance.CharacterMovement.EnableMovement();
        _isHiding = false;
    }

    private void CheckUnhide()
    {
        if (_isHiding == false) return;

        if (Input.GetButtonDown("Fire1"))
        {
            Unhide();
        }
    }
    
    
}
