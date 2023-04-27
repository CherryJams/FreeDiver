using UnityEngine;

public class GameHelper : MonoBehaviour
{
   public void SwitchToGameUI()
    {
        CanvasManager.GetInstance().SwitchCanvas(CanvasType.GameUI);
    }

   public void SetGameActive(bool gameActive)
   {
      GameManager.GetInstance().SetGameActive(gameActive); 
   }
   public void RestartGame()
   {
      GameManager.GetInstance().RestartGame();
   }
}