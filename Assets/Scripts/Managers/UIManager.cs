using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform _safeArea;

    public void PutToSafeArea(RectTransform argRectTransform)
    {
        argRectTransform.transform.SetParent(_safeArea);
    }
}
