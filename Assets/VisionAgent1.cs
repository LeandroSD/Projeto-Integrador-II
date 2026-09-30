using UnityEngine;

public class VisionAgent1 : MonoBehaviour
{
 [SerializeField] float visionRange = 1f;
  [SerializeField] float visionAngle = 45f;
 [SerializeField] Transform target;

 private void Update()
 {
    if(target == null) {Debug.LogWarning("No target assigned"); return;}

    bool b = CheckRange();

    if(b == false) {return; }
    b = CheckAngle();

    if (b == true)
    {
        Debug.Log("I can see you");
        target.gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
    }
    else
    {
        Debug.Log("No,sorry mate can't see you!");
        target.gameObject.GetComponentInChildren<Renderer>().material.color = Color.white;
    }
 }

 bool CheckRange()
 {
    float distance = Vector3.Distance(transform.position, target.position);

    return distance < visionRange;
 }

 bool CheckAngle()
    {
        Vector3 targetDirection = target.position - transform.position;
        float angle = Vector3.Angle(targetDirection, transform.forward);

        return angle < visionAngle;
    }
}

