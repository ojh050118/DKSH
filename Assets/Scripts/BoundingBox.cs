using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    BoxCollider2D bound;
    new CameraManager camera;

    void Start()
    {
        bound = GetComponent<BoxCollider2D>(); 
        camera = FindObjectOfType<CameraManager>();
        camera.SetBound(bound);
    }
}
