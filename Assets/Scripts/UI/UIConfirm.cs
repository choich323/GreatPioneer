using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIConfirm : APopup
{
    [SerializeField] private TextMeshProUGUI _msgText;
    [SerializeField] private Button _confirmBtn;
    [SerializeField] private Button _cancelBtn;
    [SerializeField] private TextMeshProUGUI _confirmText;
    [SerializeField] private TextMeshProUGUI _cancelText;

    private Action _confirmAction;
    private Action _cancelAction;
    
    public override void Init()
    {
        _cancelBtn.onClick.AddListener(Close);
    }

    public void SetData(string argMsg, Action argConfirmAction, Action argCancelAction, string argConfirmText, string argCancelText)
    {
        _msgText.text = argMsg;
        _confirmAction = argConfirmAction;
        _cancelAction = argCancelAction;
        
        _confirmText.text = argConfirmText;
        _cancelText.text = argCancelText;
        
        _confirmBtn.onClick.RemoveAllListeners();
        _confirmBtn.onClick.AddListener(() =>
        {
            _confirmAction?.Invoke();
            Close();
        });
    }

    public override void Close()
    {
        base.Close();
        
        _cancelAction?.Invoke();
    }
}
