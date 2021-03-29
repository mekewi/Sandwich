using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Text gameState;
    void Start()
    {
        GameEventsHub.Instance.Register<WinLoseEvent>(OnWinLose);
    }
    private void OnDestroy()
    {
        GameEventsHub.Instance.UnRegister<WinLoseEvent>(OnWinLose);
    }
    private void OnWinLose(WinLoseEvent obj)
    {
        gameState.text = obj.isWin ? "Winner" : "Loser";
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoHome()
    {
        SceneManager.LoadScene("Main");
    }
}
