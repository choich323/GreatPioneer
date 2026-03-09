using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const ulong INVALID_UID = 0;
    private const int DEFAULT_SLOT_COUNT = 2;
    
    [SerializeField] private GameField _gameField;

    private ulong _uid = INVALID_UID;
    private int _slotCountMax = DEFAULT_SLOT_COUNT;
    
    public GameField GameField => _gameField;
    public ulong CurUid => _uid;
    public int SlotCountMax => _slotCountMax;
    public bool IsGameOver => _gameField.IsGameOver();
        
    public void Init()
    {
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
}
