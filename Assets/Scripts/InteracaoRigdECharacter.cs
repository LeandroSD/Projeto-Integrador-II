using UnityEngine;
 
public class InteracaoRigdECharacter : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
 
    void Start()
    {
    }
 
    void Update()
    {
    }
 
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.rigidbody != null)
        {
            hit.rigidbody.AddForceAtPosition(-hit.normal * forceMagnitude, hit.point);
        }
    }
}
 
