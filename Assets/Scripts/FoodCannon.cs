using UnityEngine;

public class FoodCannon : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject chewingGum;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(chewingGum, shootPoint.position, Random.rotation);
        }
    }
}
