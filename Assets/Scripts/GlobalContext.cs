using UnityEngine;

public class GlobalContext : MonoBehaviour
{
    public static GlobalContext Instance;

    //здесь я реализовал глобальный контекст с простым синглтоном и сделал класс для хранения данных о выбранном пазле, это нужно для загрузки пазла. 

    public PuzzleSetUpData puzzleSetUpData;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
        DontDestroyOnLoad(this);

        puzzleSetUpData = new();
    }
}
