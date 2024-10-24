using UnityEngine;

public class Quit_game : MonoBehaviour
{
    public void Quit()
    {
        // エディタ上での動作を停止
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // ビルド後にアプリケーションを終了
        Application.Quit();
        #endif
    }
}
