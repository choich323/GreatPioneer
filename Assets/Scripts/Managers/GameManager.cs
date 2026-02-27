using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const ulong INVALID_UID = 0;
    
    [SerializeField] private GameField _gameField;

    private ulong _uid = INVALID_UID;
    
    public GameField GameField => _gameField;

    public void Init()
    {
        _gameField.Init();
    }
    
    public ulong GetUid()
    {
        _uid++;
        return _uid;
    }
}
