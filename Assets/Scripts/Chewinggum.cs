using UnityEngine;

public class Chewinggum : MonoBehaviour
{
    [SerializeField] private GameObject hitEffect;
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
