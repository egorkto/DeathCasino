using UnityEngine;
using UnityEngine.UI;

public class PrizeDefiner : MonoBehaviour
{
    private WheelElement _currentElement;
    private Image image;
    public void TryApplyCurrentElement()
    {
        var ray = Physics2D.Raycast(transform.position, new Vector2(0, -1));

        if (_currentElement != null)
            _currentElement.Apply();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, new Vector2(0, -1));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("srh");
    }
}
