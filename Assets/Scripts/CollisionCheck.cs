using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private NPC parentNPC;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentNPC = GetComponentInParent<NPC>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        parentNPC.ReceiveCollision(gameObject.tag);
    }
}
