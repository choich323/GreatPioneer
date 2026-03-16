using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform _uiParent;

    private HUDController _topHUDController;
    
    public void Init()
    {
        CreateHUD();
    }

    void CreateHUD()
    {
        Managers.Data.TryGetPrefabInfo((int)PrefabID.UITopHUDPanel, out var hudInfo);
        var hudObj = Instantiate<GameObject>(hudInfo.prefab, _uiParent);
        if (hudObj == null)
        {
            Debug.LogError("UIManager hudObj is null");
            return;
        }
        
        hudObj.transform.localPosition = Vector3.zero;
        var rect = hudObj.GetComponent<RectTransform>();
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        _topHUDController = hudObj.GetComponent<HUDController>();
    }

    void DestroyHUD()
    {
        Destroy(_topHUDController);
        _topHUDController = null;
    }

    public void AttachUI(RectTransform argTarget, RectTransform argParent) {
        argTarget.SetParent(argParent);
    
        argTarget.anchorMin = Vector2.zero;
        argTarget.anchorMax = Vector2.one;
        argTarget.offsetMin = Vector2.zero;
        argTarget.offsetMax = Vector2.zero;
        argTarget.localScale = Vector3.one;
        argTarget.localPosition = Vector3.zero;
    }
}
