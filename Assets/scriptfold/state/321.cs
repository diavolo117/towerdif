using UnityEngine;

public class GameResultUI : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;

    void Start()
    {
        // Проверяем что было в предыдущей сцене
        if (LastGameResult.PlayerWon)
        {
            winScreen.SetActive(true);
            loseScreen.SetActive(false);
        }
        else
        {
            winScreen.SetActive(false);
            loseScreen.SetActive(true);
        }

        // После отображения результата сбрасываем (на всякий случай)
        LastGameResult.PlayerWon = false;
    }
}
