using UnityEngine;

public class IngameMenu : APopup
{
    public override void Init()
    {
        
    }

    public override void Open()
    {
        base.Open();
        
        Managers.Game.PauseGame();
    }
    
    public override void Close()
    {
        Managers.Game.ResumeGame();
        
        base.Close();
    }
}
