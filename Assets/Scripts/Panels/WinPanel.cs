using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    public Button ToPuzzleSelectButton;
    public Button ExitButton;

    private void Start()
    {
        ToPuzzleSelectButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        });

        ExitButton.onClick.AddListener(() => 
        { 
            Application.Quit();
        });
    }
}

