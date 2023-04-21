using UnityEngine;

public class GameHelper : MonoBehaviour
{
   public void SwitchToGameUI()
    {
        CanvasManager.GetInstance().SwitchCanvas(CanvasType.GameUI);
    }
}