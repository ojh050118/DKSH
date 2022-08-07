using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;

    private PlayerAction thePlayer;
    private CameraManager theCamera;

    void Start()
    {
        theCamera = FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<PlayerAction>();

        if(startPoint == thePlayer.currentMapName)
        {
            theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
            thePlayer.transform.position = transform.position;
        }
    }
}
