using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatUI : MonoBehaviour
{
    public void LevelRestart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
