using UnityEngine;

public class GameHelper : MonoBehaviour
{
   public void SwitchToGameUI()
    {
        CanvasManager.GetInstance().SwitchCanvas(CanvasType.GameUI);
    }

   public void SetGameActive()
   {
      GameManager.GetInstance().SetGameActive(true); 
   }
   public void RestartGame()
   {
      GameManager.GetInstance().RestartGame();
   }
}