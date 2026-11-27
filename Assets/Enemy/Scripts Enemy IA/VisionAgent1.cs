using UnityEngine;
using System.Collections;
using System;

public class VisionAgent1 : MonoBehaviour
{
 [SerializeField] float visionRange = 1f;
 [SerializeField] float visionAngle = 45f;
 [SerializeField] Transform target;
 [SerializeField] Transform lineOfSightPivot;

  bool detected;

public Action OnDetected;
public Action OnLoseDetection;
 private void Update()
 {
    if(target == null) {Debug.LogWarning("No target assigned"); return;}

    bool b = CheckRange();

    if(b == false) { UnDetected(); return; }
    b = CheckAngle();

    if (b == false) {UnDetected(); return; }

    b = LineOfSightCheck();

    if(b == false)
    {
        UnDetected();
        return;
    }

    Detected();
 }
 private bool LineOfSightCheck()
 {
    VisionPoints visionPoints = target.GetComponent<VisionPoints>();

    Vector3 direction = (visionPoints.centerOfMass.position - lineOfSightPivot.position).normalized;
    Ray ray = new Ray(lineOfSightPivot.position, direction);

    RaycastHit hit;
    if(Physics.Raycast(ray, out hit)){
        if (hit.transform == target)
        {
            return true;
           
        }
    }
    return false;
 }
 
 private void Detected()
 {
    if(detected == false)
    {
        detected = true;
        //Game Over - Destruindo o jogador
        Destroy(target.gameObject);
        OnDetected?.Invoke();

    }
 }
 private void UnDetected()
 {
    if(detected == true)
    {
        detected = false;
        OnLoseDetection?.Invoke();
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
private void OnDrawGizmosSelected()
{
    Gizmos.DrawWireSphere(transform.position, visionRange);

    Quaternion rotation = Quaternion.AngleAxis(visionAngle, Vector3.up);
    Vector3 endPoint = transform.position + (rotation * transform.forward * visionRange);
    Gizmos.DrawLine(transform.position, endPoint);

    rotation = Quaternion.AngleAxis(-visionAngle, Vector3.up);
    endPoint = transform.position + (rotation * transform.forward * visionRange);
    Gizmos.DrawLine(transform.position, endPoint);
}
}

