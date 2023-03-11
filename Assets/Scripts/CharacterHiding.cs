using UnityEngine;

public class CharacterHiding : MonoBehaviour
{
    private bool _isHiding = false;
    private Vector3 _hidePosition;

    private void Update()
    {
        CheckUnhide();
    }
    
    public void Hide(Vector3 holePosition)
    {
        _hidePosition = holePosition;
        _isHiding = true;
        gameObject.SetActive(false);
        gameObject.transform.position = new Vector3(holePosition.x, -1.4f, holePosition.z);
        gameObject.SetActive(true);
        GameManager.Instance.CharacterMovement.DisableMovement();
    }

    public void Unhide()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = new Vector3(_hidePosition.x, 0, _hidePosition.z);
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
