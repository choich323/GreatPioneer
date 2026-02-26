using System;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance;
    public static Managers I => _instance;
    
    private PoolManager _poolManager;
    private DataManager _dataManager;
    private GameManager _gameManager;
    
    // 접근용 프로퍼티
    public static PoolManager Pool => I._poolManager;
    public static DataManager Data => I._dataManager;
    public static GameManager Game => I._gameManager;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            InitManagers();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitManagers()
    {
        _poolManager = GetComponent<PoolManager>();
        _poolManager.Init();
        
        _dataManager = new DataManager();
        _dataManager.Init();
        
        _gameManager = GetComponent<GameManager>();
    }
}
