using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float rotationSpeed = 60f; // rotation speed in degrees per second

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}