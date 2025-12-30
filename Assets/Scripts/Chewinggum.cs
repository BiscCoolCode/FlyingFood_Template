using UnityEngine;

public class Chewinggum : MonoBehaviour
{
    [SerializeField] private GameObject hitEffect;

    private void Start()
    {
        Invoke("SelfDelete", 30.0f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
        SelfDelete();
    }

    private void SelfDelete()
    {
        Destroy(gameObject);
    }
}
