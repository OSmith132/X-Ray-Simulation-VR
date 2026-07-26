using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LimitMovement : MonoBehaviour
{
    public Vector3 minLocalPosition = new Vector3(-0.25f, 0.2f, -0.6f);
    public Vector3 maxLocalPosition = new Vector3(0.15f, 0.75f, 0.6f);

    [SerializeField, Tooltip("Coordinates of the object below to ensure no collision during motion")] Transform minObject;

    [SerializeField, Tooltip("How far below minObject's axis the handle's axis sits")] float handleAxisOffset = -1f;


    private Rigidbody rigidBody;


    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }



    void FixedUpdate()
    {
        Vector3 localPos = transform.localPosition;


        float minY = minLocalPosition.y;
        if (minObject != null && transform.parent != null)
        {
            Vector3 offsetWorldPos = minObject.position - new Vector3(0f, handleAxisOffset, 0f);
            float minObjectLocalY = transform.parent.InverseTransformPoint(offsetWorldPos).y;

            minY = Mathf.Max(minLocalPosition.y, minObjectLocalY);
        }



        localPos.x = Mathf.Clamp(localPos.x, minLocalPosition.x, maxLocalPosition.x);
        localPos.y = Mathf.Clamp(localPos.y, minY, maxLocalPosition.y);
        localPos.z = Mathf.Clamp(localPos.z, minLocalPosition.z, maxLocalPosition.z);

        rigidBody.MovePosition(transform.parent.TransformPoint(localPos));

    }
}