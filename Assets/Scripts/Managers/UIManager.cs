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
        var obj = Managers.Pool.Instantiate<HUDController>(PrefabID.UITopHUDPanel);
        if (obj == null)
        {
            Debug.LogError("Failed to instantiate HUD");
            return;
        }
        obj.transform.SetParent(_uiParent);
        obj.transform.localPosition = Vector3.zero;
        var rect = obj.GetComponent<RectTransform>();
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        _topHUDController = obj.GetComponent<HUDController>();
    }

    void DestroyHUD()
    {
        Managers.Pool.Destroy(_topHUDController, PrefabID.UITopHUDPanel);
        _topHUDController = null;
    }

    public void SetParentToUI(RectTransform argUiParent)
    {
        argUiParent.SetParent(_uiParent);
    }
}
