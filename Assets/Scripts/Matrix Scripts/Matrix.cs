using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/**
 * RoundVector округление Vecor2 до int.
 * IsInsideBorder проверка фигуры по оси X (находится в экране, или за его пределами).
 * DeleteRow удаление линии по оси X при полном заполнение фигурами.
 * DecreseRow смещение заполненной части экрана по оси Y , при заполнение по оси X.
 * DecreseRowAbove смещение заполненной части экрана по оси Y+1 , при заполнение по оси X.
 * IsRowFull проверка заполенности по оси X.
 * DeleteWholeRows проверка всего игрового поля.
 */

public class Matrix : MonoBehaviour
{
    public static event UnityAction<int>ScoreChanged;
    private static int _score = 10;
    public static int _row = 10;
    public static int _column = 20;
    public static Transform[,] grid = new Transform[_row, _column];

    public static Vector2 RoundVector(Vector2 vect)
    {
        return new Vector2(Mathf.Round(vect.x), Mathf.Round(vect.y));
    }
    public static bool IsInsideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < _row && (int)pos.y >= 0);  
    }
    public static void DeleteRow(int y)
    {
        for (int x = 0; x < _row; ++x)
        {
            GameObject.Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
        ScoreChanged?.Invoke(_score);
    }
    public static void DecreseRow(int y)
    {
        for (int x = 0; x < _row; ++x)
        {
            if(grid[x,y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }
    public static void DecreseRowAbove(int y)
    {
        for (int i = y; i < _column; ++i)
        {
            DecreseRow(i);
        }
    }
    public static bool IsRowFull(int y)
    {
        for (int x = 0; x < _row; ++x)
        {
            if (grid[x, y] == null)
                return false;
        }
        return true;
    }
    public static void DeleteWholeRows()
    {
        for (int y = 0; y < _column; ++y)
        {
            if(IsRowFull(y))
            {
                DeleteRow(y);
                DecreseRowAbove(y + 1);
                --y;
            }
        }
    }
}
