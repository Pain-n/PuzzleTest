using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLvlPanel : MonoBehaviour
{
    public Button LvlButtonPrefab;
    public Transform ButtonsContainer;
    void Start()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Images/Originals");

        foreach(Sprite sprite in sprites)
        {
            Button lvlButton = Instantiate(LvlButtonPrefab, ButtonsContainer);
            lvlButton.image.sprite = sprite;

            lvlButton.onClick.AddListener(() =>
            {
                //При выборе уровня записывается название изображения.
                GlobalContext.Instance.puzzleSetUpData.selectedImageName = sprite.name;
                Instantiate(Resources.Load<SelectDifficultyPanel>("Prefabs/Panels/SelectDifficultyPanel"), transform.parent);
            });
        }
    }
}
