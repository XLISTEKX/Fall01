using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinesweeperCell : MonoBehaviour
{
    public byte cellNumber = 0;
    public TMP_Text cellNumberTXT;
    public GameObject mine;
    public List<MinesweeperCell> neighborCells;
    public GameObject cover;
    public Color[] colors;
    public Vector2 position;

    public bool isClicked = false;

    MinesweeperControler minesweeperControler;

    private void Start()
    {
        minesweeperControler = GameObject.FindGameObjectWithTag("Minesweeper").GetComponent<MinesweeperControler>();
        int gameSize = minesweeperControler.gameAreaSize;
        for(int i = -1; i < 2; i++)
        {
            if (position.x + i < 0 || position.x + i >= gameSize) continue;
            for (int j = -1; j < 2; j++)
            {
                if(position.y + j < 0 || position.y + j >= gameSize) continue;
                neighborCells.Add(minesweeperControler.allCells[(int)position.x + i, (int)position.y + j]);

            }
        }
    }
    public void initCell()
    {

        cellNumberTXT.text = cellNumber.ToString();
        int colorID = cellNumber;
        if(colorID >= 9)
        {
            colorID = 9;
            cellNumberTXT.gameObject.SetActive(false);
            mine.SetActive(true);
        }

        cellNumberTXT.color = colors[colorID];
    }

    public void unCoverCell()
    {
        isClicked = true;
        cover.SetActive(false);
    }
    public void clickCell()
    {
        if (isClicked) return;
        isClicked = true ;
        switch (cellNumber)
        {
            case 0:
                unCoverCell();
                foreach(MinesweeperCell MC in neighborCells)
                {
                    MC.clickCell();
                }
                break;
            case >=9:
                minesweeperControler.unCoverBombs();
                GetComponent<Image>().color = Color.red;
                break;
            default:
                unCoverCell();
                break;
        }
    }
}
