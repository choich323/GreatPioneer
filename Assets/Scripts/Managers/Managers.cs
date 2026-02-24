using System;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance;
    public static Managers I => _instance;

    [Header("ManagerList")]
    [SerializeField] private PoolManager _poolManager;

    // 접근용 프로퍼티들
    public static PoolManager Pool => I._poolManager;

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
        _poolManager.Init();
    }
}
