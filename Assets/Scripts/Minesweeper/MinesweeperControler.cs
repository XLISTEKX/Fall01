using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinesweeperControler : MonoBehaviour
{
    [Header("Game Settings")]
    public int gameAreaSize;
    public int bombCount;
    public float cellPadding;
    public int gameTime;
    public int currentCellUncovered = 0;
    [Header("GameOffsets")]
    public GameObject gameOver;
    public Slider gameTimerSlider;
    public GameObject gameCell;
    public GameObject cellSpawn;
    public MinesweeperCell[,] allCells;
    public List<Vector2> bombCordinates = new List<Vector2>();

    private void Start()
    {
        InvokeRepeating("changeGameTime", 1, 1);
        allCells = new MinesweeperCell[gameAreaSize, gameAreaSize];

        spawnBombs();

        float width = cellSpawn.GetComponent<RectTransform>().rect.width;
        width = (width - (gameAreaSize + 1) * cellPadding) / gameAreaSize;

        float height = cellSpawn.GetComponent<RectTransform>().rect.height;
        height = (height - (gameAreaSize + 1) * cellPadding) / gameAreaSize;

        cellSpawn.GetComponent<GridLayoutGroup>().cellSize = new Vector2(width, height);

        for(int i = 0; i < gameAreaSize; i++)
        {
            for(int j = 0; j < gameAreaSize; j++)
            {
                MinesweeperCell temp = Instantiate(gameCell, cellSpawn.transform).GetComponent<MinesweeperCell>();
                temp.position = new Vector2(i, j);
                allCells[i,j] = temp;
            }
        }
        initCells();

    }
    void initCells()
    {
        foreach(Vector2 bc in bombCordinates)
        {
            allCells[(int)bc.x, (int)bc.y].cellNumber = 9;
            int maxArea = gameAreaSize - 1;
            if(bc.x == 0)
            {
                if (bc.y == 0)
                {
                    allCells[1, 0].cellNumber++;
                    allCells[1, 1].cellNumber++;
                    allCells[0, 1].cellNumber++;
                }
                else if(bc.y == maxArea)
                {
                    allCells[1, maxArea - 1].cellNumber++;
                    allCells[1, maxArea    ].cellNumber++;
                    allCells[0, maxArea - 1].cellNumber++;
                }
                else
                {
                    allCells[0, (int) bc.y + 1].cellNumber++;
                    allCells[1, (int)bc.y + 1].cellNumber++;
                    allCells[1, (int)bc.y].cellNumber++;
                    allCells[1, (int)bc.y - 1].cellNumber++;
                    allCells[0, (int)bc.y-1].cellNumber++;
                }
            }
            else if (bc.x == maxArea)
            {
                if (bc.y == 0)
                {
                    allCells[maxArea - 1, 0].cellNumber++;
                    allCells[maxArea - 1, 1].cellNumber++;
                    allCells[maxArea, 1].cellNumber++;
                }
                else if (bc.y == maxArea)
                {
                    allCells[maxArea - 1, maxArea].cellNumber++;
                    allCells[maxArea - 1, maxArea - 1].cellNumber++;
                    allCells[maxArea, maxArea - 1].cellNumber++;
                }
                else
                {
                    allCells[maxArea, (int)bc.y + 1].cellNumber++;
                    allCells[maxArea, (int)bc.y - 1].cellNumber++;
                    allCells[maxArea - 1, (int)bc.y + 1].cellNumber++;
                    allCells[maxArea - 1, (int)bc.y - 1].cellNumber++;
                    allCells[maxArea - 1, (int)bc.y ].cellNumber++;
                }
            }
            else if (bc.y == 0)
            {
                allCells[(int)bc.x + 1, 0].cellNumber++;
                allCells[(int)bc.x - 1, 0].cellNumber++;
                allCells[(int)bc.x, 1].cellNumber++;
                allCells[(int)bc.x + 1, 1].cellNumber++;
                allCells[(int)bc.x - 1,1].cellNumber++;

            }
            else if (bc.y == maxArea)
            {
                allCells[(int)bc.x + 1, maxArea].cellNumber++;
                allCells[(int)bc.x - 1, maxArea].cellNumber++;
                allCells[(int)bc.x, maxArea - 1].cellNumber++;
                allCells[(int)bc.x + 1, maxArea - 1].cellNumber++;
                allCells[(int)bc.x - 1, maxArea - 1].cellNumber++;
            }
            else
            {
                allCells[(int)bc.x + 1, (int)bc.y].cellNumber++;
                allCells[(int)bc.x + 1, (int)bc.y - 1].cellNumber++;
                allCells[(int)bc.x, (int)bc.y - 1].cellNumber++;
                allCells[(int)bc.x - 1, (int)bc.y - 1].cellNumber++;
                allCells[(int)bc.x - 1, (int)bc.y].cellNumber++;
                allCells[(int)bc.x - 1, (int)bc.y + 1].cellNumber++;
                allCells[(int)bc.x, (int)bc.y + 1].cellNumber++;
                allCells[(int)bc.x + 1, (int)bc.y + 1].cellNumber++;

            }
        }

        foreach(MinesweeperCell cell in allCells)
        {
            cell.initCell();
        }
    }

    void spawnBombs()
    {
        while (bombCordinates.Count < bombCount)
        {
            bool bombFound = false;
            Vector2 tempVector = new Vector2(Random.Range(0, gameAreaSize), Random.Range(0, gameAreaSize));

            foreach (Vector2 bc in bombCordinates)
            {
                if (bc == tempVector)
                {
                    bombFound = true;
                    break;
                }

            }
            if (bombFound) continue;

            bombCordinates.Add(tempVector);
        }
    }

    public void unCoverBombs()
    {
        foreach(Vector2 bc in bombCordinates)
        {
            allCells[(int)bc.x, (int)bc.y].unCoverCell();
        }
        gameOver.SetActive(true);
        CancelInvoke("changeGameTime");
    }

    void changeGameTime()
    {
        
        gameTime -= 1;
        if (gameTime == 0)
        {
            CancelInvoke("changeGameTime");
            gameOver.SetActive(true);
        }

            gameTimerSlider.value = gameTime;

    }
    public void resetGame()
    {
        SceneManager.LoadScene(0);
    }

    public void updateUncoveredCells()
    {
        currentCellUncovered++;
        if(currentCellUncovered >= gameAreaSize * gameAreaSize - bombCount)
        {
            PlayerStatsInv psi = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<PlayerStatsInv>();
            psi.updateStats();
            psi.money += 50;
            psi.mineSweeperWon++;
            psi.saveStats();

            resetGame();
        }
    }

}
