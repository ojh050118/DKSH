using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    /// <summary>
    /// 이동할 씬.
    /// </summary>
    public string DestinationScene;

    private PlayerAction thePlayer;
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerAction>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = DestinationScene;
            SceneManager.LoadScene(DestinationScene);
        }
    }
}
