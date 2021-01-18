using TMPro;
using UnityEngine;

public class GameFinished : MonoBehaviour
{
    public GameObject FinishedGamePanel;
    public GameObject gamePanel;
    public TextMeshProUGUI textoPuntuacion;
    public TextMeshProUGUI textoPastillas;
    public PlayerComportamiento pc;


    private void OnTriggerEnter(Collider other)
    {
        
        gamePanel.SetActive(false);
        FinishedGamePanel.SetActive(true);
        textoPuntuacion.text = pc.puntuacion.ToString();
        textoPastillas.text = pc.puntuacionPasillas.ToString();
        pc.gameOverBool = true;
        
    }
}
