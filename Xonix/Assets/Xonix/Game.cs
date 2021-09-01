using UnityEngine;

public class Game : MonoBehaviour
{
    public void Awake()
    {
        Continue();
    }

    public static void Stop()
    {
        Time.timeScale = 0;
    }
    public static void Continue()
    {
        Time.timeScale = 1;
    }

}
