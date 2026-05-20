using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class BoardManager : MonoBehaviour
{

    public  int[,] grid = new int[Constant.boardx, Constant.boardy];
    private  List<(int x, int y)> oPieceInitPositionList = new List<(int x, int y)>{
    (4, 0),
    (4, 1),
    (5, 0),
    (5, 1)};

    public GameObject oPiecePrefab;
    private Vector3 initialVector3 = new Vector3(0, Constant.boardy-2, 0);
    private GameObject activeObject;
    private List<(int x, int y)> activePiecePositionList = new List<(int x, int y)>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GameLoop());
    }

    private bool CheckIfObjectFitsGrid()
    {
        foreach (var piece in oPieceInitPositionList)
            if (grid[piece.x, piece.y] == 1)
            {
                return false;
            }

        return true;
    }      

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("update triggered");
    }


    private void InitOPieceOnGrid()
    {
        grid[4, 0] = 1;
        grid[4, 1] = 1;
        grid[5, 0] = 1;
        grid[5, 1] = 1;
    }

    IEnumerator GameLoop()
    {
        while (true)
        { 

            printGrid();
            if (activeObject == null)
            {
                //pick object to create
                //sample OPiece
                bool objectFits = CheckIfObjectFitsGrid();

                if (objectFits)
                {
                    InitOPieceOnGrid();
                    activePiecePositionList.Clear();
                    InitActivePiecePositions();
                    activeObject = Instantiate(oPiecePrefab, initialVector3, Quaternion.identity);
                    yield return new WaitForSeconds(1f);
                }

            }

            if (canPieceMoveDown())
            {
                ClearFromGridCurrentPositions();
                UpdateActivePiecePositions();
                OccupyNewPositionsOnGrid();
                activeObject.transform.position += Vector3.down;
            }
            else
                activeObject = null;

            printGrid();

            yield return new WaitForSeconds(1f);
        }
    }

    private void InitActivePiecePositions()
    {
        foreach ( var position in oPieceInitPositionList)
        activePiecePositionList.Add(position);
    }

    private void UpdateActivePiecePositions()
    {
        List<(int x, int y)> newPositions = new List<(int x, int y)>();

        foreach (var positionItem in activePiecePositionList)
            newPositions.Add((positionItem.x, positionItem.y + 1));

        activePiecePositionList = newPositions;
    }

    private void OccupyNewPositionsOnGrid()
    {
        foreach (var positionItem in activePiecePositionList)
           grid[positionItem.x, positionItem.y] = 1;
    }

    private void ClearFromGridCurrentPositions()
    {
        foreach (var positionItem in activePiecePositionList)
            grid[positionItem.x, positionItem.y] = 0;
    }

    private bool canPieceMoveDown()
    {
        bool canMoveDown = true;

        foreach (var positionItem in activePiecePositionList)
        {
            (int x, int y) newPosition = (positionItem.x, positionItem.y+1);
            Debug.Log("lolo" + newPosition);

            if (newPosition.x >9 || newPosition.x < 0 || newPosition.y > 19 || newPosition.y < 0 || (grid[newPosition.x, newPosition.y] == 1 && !activePiecePositionList.Contains(newPosition)) ) //can not move condition
            {
                canMoveDown = false;
                break;
            }
        }


        return canMoveDown;
    }

    private void printGrid()
    {

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < Constant.boardx; i++)
        {            
            for (int j = 0; j < Constant.boardy; j++)
            {
                sb.Append(grid[i, j] + " ");
            }
          
            sb.Append('\n');
        }

        Debug.Log(sb.ToString());
    }
}
