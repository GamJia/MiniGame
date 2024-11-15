using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] GameObject line; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(line, Vector3.zero, Quaternion.identity, this.transform);
        }
    }
}
