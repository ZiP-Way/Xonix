using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{
    public void LevelRestart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
