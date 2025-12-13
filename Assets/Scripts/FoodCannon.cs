using UnityEngine;

public class FoodCannon : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject chewingGum;
    [SerializeField] private float projectileSpeed = 30.0f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(chewingGum, shootPoint.position, Random.rotation);
        projectile.GetComponent<Rigidbody>().linearVelocity = shootPoint.forward * projectileSpeed;
    }
}
