using UnityEngine;

public class BarrierSetup : MonoBehaviour
{
    void Start()
    {
        int mushroom = LayerMask.NameToLayer("Mushroom");
        int barrier = LayerMask.NameToLayer("Barrier");

        Physics.IgnoreLayerCollision(mushroom, barrier, true);
    }
}