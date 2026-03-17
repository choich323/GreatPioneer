using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const ulong INVALID_UID = 0;
    private const int DEFAULT_SLOT_COUNT = 2;
    private const int DEFAULT_GAME_SPEED = 1;
    
    private ulong _uid = INVALID_UID;
    private int _slotCountMax = DEFAULT_SLOT_COUNT;
    private int _curGameSpeed = DEFAULT_GAME_SPEED;
    private GameField _gameField;
    
    public GameField GameField => _gameField;
    public ulong CurUid => _uid;
    public int SlotCountMax => _slotCountMax;
    public bool IsGameOver => _gameField.IsGameOver();
        
    public void Init()
    {
        var gameFieldObj = Managers.Pool.Instantiate<GameField>(PrefabID.GameField);
        if (gameFieldObj == null)
        {
            Debug.LogError("Game field could not be instantiated.");
            return;
        }
        _gameField = gameFieldObj.GetComponent<GameField>();
        _gameField.Init();
    }
    
    public ulong GetNewUid()
    {
        _uid++;
        return _uid;
    }

    public void SetSlotCount(int argCount)
    {
        _slotCountMax = argCount;
    }

    public void EndGame(bool isPlayerWin)
    {
        if(isPlayerWin)
            Debug.Log($"You Win!");
        else
            Debug.Log($"You Lose!");
    }

    public void SetGameSpeed(int argSpeed)
    {
        _curGameSpeed = argSpeed;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = _curGameSpeed;
    }
}
