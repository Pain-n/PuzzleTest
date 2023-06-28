using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectDifficultyPanel : MonoBehaviour
{
    public List<DiffItem> difficulties;
    public Button CloseButton;
    private void Start()
    {
        foreach (DiffItem item in difficulties)
        {
            item.diffButton.onClick.AddListener(() =>
            {
                //Я думал использовать для хранения изображений ScriptableObject, но перетаскивать множество изображений в массив вручную это очень долго, поэтому я подгружаю все прямо из папки.
                //Так быстрее, но тут есть и минусы.
                //Нужно следить чтобы в каждом DiffItem были прописаны необходимые параметры, иначе паззл не прогрузиться. Да, все еще нужно что то делать руками, но работы заметно меньше, чем в варианте с ScriptableObject.
                GlobalContext.Instance.puzzleSetUpData.selectedDifficulty = item.difficulty;
                GlobalContext.Instance.puzzleSetUpData.selectedDiffFolderName = item.diffFolderName;
                SceneManager.LoadScene("GameScene",LoadSceneMode.Single);
            });
        }
        CloseButton.onClick.AddListener(() => 
        { 
            Destroy(gameObject);
        });
    }
}

[Serializable]
public class DiffItem
{
    public int difficulty;
    public string diffFolderName;
    public Button diffButton;
}
