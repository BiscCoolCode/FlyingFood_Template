using DG.Tweening;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Collider[] colliders;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform bubble;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //colliders = GetComponents<Collider>();
        //colliders[0].
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveCollision(string tag)
    {
        if (tag == "Head")
        {
            bubble.transform.DOScale(Vector3.one * Random.Range(0.5f, 5.0f), 2.5f);
        }
        else if(tag == "Body")
        {
            animator.SetBool("Die", true);
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Projectile"))
        {
            return;
        }

        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint contactPoint = collision.GetContact(i);
            Collider collider = contactPoint.thisCollider;
            if (collider = colliders[0])
            {
                print("collider 0");
            }
            else if (collider = colliders[1])
            {
                print("collider 1");
            }

        }
    }*/
}
