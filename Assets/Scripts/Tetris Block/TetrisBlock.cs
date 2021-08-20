using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    private float _lastFall = 0f;
    private float _lose;
    private Spawner _spawner;
    private GameController _gameController;
    private float _levelSpeed;
    private bool _canPlay = true;
    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
        _spawner = FindObjectOfType<Spawner>();
        LoadLevelSpeed();
    }
   private void Update()
    {
        if(_canPlay)
        { 
        
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (IsValidGridPos())
            {
                UpdateMatrixGrid();
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (IsValidGridPos())
            {
                UpdateMatrixGrid();
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
            {
            transform.Rotate(new Vector3(0, 0, -90));
            if (IsValidGridPos())
            {
                UpdateMatrixGrid();
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 90));
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time -_lastFall >=_levelSpeed)
        {
            transform.position += new Vector3(0, -1, 0);
            if(IsValidGridPos())
            {
                UpdateMatrixGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                Matrix.DeleteWholeRows();
                    _spawner.SpawnRandom();
                _lose = transform.position.y;
                GameOverCheck();
                enabled = false;
            }
            _lastFall = Time.time ;
                
        }
        }
        CanPlayCheck();
    }
   private bool IsValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 vect = Matrix.RoundVector(child.position);
            if(!Matrix.IsInsideBorder(vect))
                return false;
            if (Matrix.grid[(int)vect.x, (int)vect.y] != null && Matrix.grid[(int)vect.x, (int)vect.y].parent != transform)
                return false;
        }
        return true;
    }
   private void UpdateMatrixGrid()
    {
        for (int y = 0; y < Matrix._column; ++y)
        {
            for (int x = 0; x < Matrix._row;++x)
            {if(Matrix.grid[x,y] !=null)
                {
                    if (Matrix.grid[x, y].parent == transform)
                    {
                        Matrix.grid[x, y] = null;
                    }
                }
            }
        }
        foreach (Transform child in transform)
        {
            Vector2 vect = Matrix.RoundVector(child.position);
            Matrix.grid[(int)vect.x, (int)vect.y] = child;
        }
    }
   private void GameOverCheck()
    {
        if (_lose >= 14)
        {
            _gameController.GameOver();
            _spawner.canSpawm = false;
        }
    }

    private void LoadLevelSpeed()
    {
        if(PlayerPrefs.GetInt(Tags.GAME_SPEED) == 1)
        {
            _levelSpeed = 1;
        }
        else if (PlayerPrefs.GetInt(Tags.GAME_SPEED) == 2)
        {
            _levelSpeed = 0.6f;
        }
        else if (PlayerPrefs.GetInt(Tags.GAME_SPEED) == 3)
        {
            _levelSpeed = 0.4f;
        }
        else if (PlayerPrefs.GetInt(Tags.GAME_SPEED) == 4)
        {
            _levelSpeed = 0.2f;
        }
    }
    private void CanPlayCheck()
    {
        if (Time.timeScale == 0)
        { _canPlay = false; }
        else _canPlay = true;
    }

}
