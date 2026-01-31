using KinematicCharacterController;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    [SerializeField] private KinematicCharacterMotor playerMotor;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -5)
        {
            playerMotor.SetPosition(Vector3.zero);
            print("Out of Bounds");
        }
    }
}
