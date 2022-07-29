using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    /// <summary>
    /// 이동할 씬.
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
