using UnityEngine;

public class FoodCannon : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject chewingGum;
    [SerializeField] private float projectileSpeed = 30.0f;
    [SerializeField] private Camera camera;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Physics.Raycast(camera.transform.position, camera.transform.forward, out hit);
        shootPoint.LookAt(hit.point);
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(chewingGum, shootPoint.position, Random.rotation);
        projectile.GetComponent<Rigidbody>().linearVelocity = shootPoint.forward * projectileSpeed;
    }
}
