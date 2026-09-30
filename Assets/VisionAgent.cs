using UnityEngine;

public class VisionAgent : MonoBehaviour
{
 [SerializeField] float visionRange = 1f;
 [SerializeField] Transform target;

 private void Update()
 {
    if(target == null) {Debug.LogWarning("No target assigned"); return;}

    bool b = CheckRange();

    if (b == true)
    {
        Debug.Log("I can see you");
    }
    else
    {
        Debug.Log("No,sorry mate can't see you!");
    }
 }

 bool CheckRange()
 {
    float distance = Vector3.Distance(transform.position, target.position);

    return distance < visionRange;
 }
}
