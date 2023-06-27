using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.U2D;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //� ���������� ������ ������ ����� �������, �� �� ������ ���� ��������������� ��� ������ ������ � �������� ��� ���.
    [SerializeField] private Transform gamePanel;
    [SerializeField] private Transform sellsContainer;
    [SerializeField] private Transform puzzlePiecesContainer;
    private List<PuzzlePiece> puzzlePieces;
    [SerializeField] private PuzzlePiece piecePrefab;
    private List<PuzzleSell> puzzleSells;
    [SerializeField] private PuzzleSell sellPrefab;

    private int size;

    [SerializeField] private Image DraggedPiece;
    private int DraggedID = -1;

    void Start()
    {
        puzzlePieces = new();
        puzzleSells = new();
        size = GlobalContext.Instance.puzzleSetUpData.selectedDifficulty;
        FillGameField();
        FillPuzzlePiecesContainer();
    }

    private void Update()
    {
        //������� ������������� � ���� �����, ������ ����� ���������� ������� drag&drop ��� �� ���� ������.
        if (DraggedID != -1)
        {
            DraggedPiece.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }
    }

    private void FillPuzzlePiecesContainer()
    {
        //���� � ������ �������, ��, �� ��� �� ��� ���� ��������� ��������, ��� ��� ����� ���� ������������� ����.
        Sprite[] spritePieces = Resources.LoadAll<Sprite>($"Images/PuzzleSets/{GlobalContext.Instance.puzzleSetUpData.selectedDiffFolderName}/{GlobalContext.Instance.puzzleSetUpData.selectedImageName}");
        
        //�� ���� ������� � ���� ����� ������� � ���������� ����� ������, ������� ������� �� ������ ������ � ����� ������.
        HashSet<int> ExcludeID = new();
        int id = Random.Range(0, spritePieces.Length);

        for (int i = 0; i < spritePieces.Length;i++)
        {
            while(ExcludeID.Contains(id)) id = Random.Range(0, spritePieces.Length);

            PuzzlePiece piece = Instantiate(piecePrefab, puzzlePiecesContainer);
            puzzlePieces.Add(piece);
            piece.pieceButton.image.sprite = spritePieces[id];
            piece.ID = id;
            piece.pieceButton.onClick.AddListener(() =>
            {
                if (DraggedID != -1) return;
                DraggedPiece.sprite = piece.pieceButton.image.sprite;
                DraggedPiece.gameObject.SetActive(true);
                DraggedID = piece.ID;
                piece.gameObject.SetActive(false);
            });
            ExcludeID.Add(id);
        }
    }
    private void FillGameField()
    {
        //� ����� ������ � ������ ��������, ����������� ���� ��� ������� ������ � �������� ��� ��������.
        sellsContainer.GetComponent<GridLayoutGroup>().constraintCount = size;

        for (int i = 0; i < Mathf.Pow(size, 2); i++)
        {
            PuzzleSell sell = Instantiate(sellPrefab, sellsContainer);
            puzzleSells.Add(sell);
            sell.ID = i;
            sell.pieceButton.onClick.AddListener(() =>
            {
                if (DraggedID != -1)
                {
                    if(sell.PuttedID != -1)
                    {
                        foreach (PuzzlePiece piece in puzzlePieces)
                        {
                            if (piece.pieceButton.image.sprite == sell.pieceButton.image.sprite) piece.gameObject.SetActive(true);
                        }
                    } 
                    sell.pieceButton.image.sprite = DraggedPiece.sprite;
                    sell.PuttedID = DraggedID;
                    DraggedPiece.gameObject.SetActive(false);
                    DraggedID = -1;
                    WinCheck();
                    return;
                }

                if (sell.pieceButton.image.sprite != null)
                {
                    foreach(PuzzlePiece piece in puzzlePieces)
                    {
                        if (piece.pieceButton.image.sprite == sell.pieceButton.image.sprite) piece.gameObject.SetActive(true);
                    }
                    sell.pieceButton.image.sprite = null;
                    sell.PuttedID = -1;
                }
            });
        }
    }

    private void WinCheck()
    {
        //�������� �� ������, � ������� �������� ID �����, ����������� � ������ � ID ����� ������. ���� ��� �� ���������, �� ����� ����������� return.
        foreach (PuzzleSell sell in puzzleSells)
        {
            if (sell.ID != sell.PuttedID) return;
        }
        Instantiate(Resources.Load<WinPanel>("Prefabs/Panels/WinPanel"), gamePanel.transform.parent);
    }
}
