using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameField _gameField;
    
    public GameField GameField => _gameField;
}
