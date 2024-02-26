using UnityEngine;

public class WheelElement : MonoBehaviour
{
    [SerializeField] private string _testString;

    public void Apply()
    {
        Debug.Log(_testString);
    }
}