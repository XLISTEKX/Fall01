using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinesweeperControler : MonoBehaviour
{
    public int gameAreaSize;
    public GameObject gameCell;
    public GameObject cellSpawn;
    public List<GameObject> allCells;

    private void Start()
    {
        float width = cellSpawn.GetComponent<RectTransform>().rect.width / (gameAreaSize +1) ;
        width -= (gameAreaSize + 1) * 10;
        float hight = cellSpawn.GetComponent<RectTransform>().rect.height / (gameAreaSize +1);
        Debug.Log(width + " Width");
        Debug.Log(cellSpawn.GetComponent<RectTransform>().rect.width + " Gameplay width");
        //width -= 5 * (gameAreaSize);
        cellSpawn.GetComponent<GridLayoutGroup>().cellSize = new Vector2(width, hight);

        for(int i = 0; i < gameAreaSize * gameAreaSize; i++)
        {
            GameObject temp = Instantiate(gameCell, cellSpawn.transform);
            allCells.Add(temp);
        }
        Debug.Log(width + " Width");
        Debug.Log(cellSpawn.GetComponent<RectTransform>().rect.width + " Gameplay width");
    }


}
