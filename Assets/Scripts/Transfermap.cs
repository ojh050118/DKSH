using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    /// <summary>
    /// 이동할 씸
    /// </summary>
    public string DestinationScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene(DestinationScene);
        }
    }
}
