using UnityEngine;

public class MushroomDestroyer : MonoBehaviour
{
    public enum Kind { Safe, Poison, Fatal }

    public Kind kind;                    
    public float pushForce = 2f;         
    public float lifeMin = 2.5f;         
    public float lifeMax = 3.5f;

    Rigidbody rootBody;
    bool broken;

    void Awake()
    {
        
        gameObject.tag = kind.ToString();

        
        int layer = LayerMask.NameToLayer("Mushroom");
        if (layer != -1)
        {
            foreach (Transform t in GetComponentsInChildren<Transform>())
                t.gameObject.layer = layer;
        }

        
        rootBody = GetComponent<Rigidbody>();
        if (rootBody == null) rootBody = gameObject.AddComponent<Rigidbody>();
        rootBody.collisionDetectionMode = CollisionDetectionMode.Continuous;

        
        if (GetComponent<Collider>() == null)
        {
            Bounds b = new Bounds(transform.position, Vector3.zero);
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                b.Encapsulate(r.bounds);

            CapsuleCollider cap = gameObject.AddComponent<CapsuleCollider>();
            cap.center = transform.InverseTransformPoint(b.center);
            cap.radius = Mathf.Max(b.extents.x, b.extents.z) / transform.lossyScale.x;
            cap.height = b.size.y / transform.lossyScale.y;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (broken || !collision.gameObject.CompareTag("Floor")) return;
        broken = true;

        
        Transform[] parts = new Transform[transform.childCount];
        for (int i = 0; i < parts.Length; i++)
            parts[i] = transform.GetChild(i);

        Vector3 speed = rootBody.linearVelocity;
        transform.DetachChildren();

        foreach (Transform part in parts)
        {
            MeshCollider mc = part.gameObject.AddComponent<MeshCollider>();
            mc.convex = true;

            Rigidbody rb = part.gameObject.AddComponent<Rigidbody>();
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.linearVelocity = speed;
            rb.AddForce(Random.insideUnitSphere * pushForce + Vector3.up * pushForce, ForceMode.Impulse);
            rb.AddTorque(Random.insideUnitSphere * pushForce, ForceMode.Impulse);

            Destroy(part.gameObject, Random.Range(lifeMin, lifeMax));
        }

        Destroy(gameObject);
    }
}