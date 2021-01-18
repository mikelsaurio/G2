using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AkiraPill : MonoBehaviour
{
    public GameObject akiraPill;
    private int puntuacionPasillas;

    
    void Start()
    {
        //puntuacionPasillas=PlayerPrefs.GetInt("puntuacionPasillas");
        //GameObject.Find("PastillasAkira").GetComponent<TextMeshProUGUI>().text = puntuacionPasillas.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Recuperar puntuación actual
        puntuacionPasillas = PlayerPrefs.GetInt("puntuacionPasillas");

        //Desaparece la pastilla
        akiraPill.SetActive(false);

        //agrega la puntuación al canvas
        puntuacionPasillas += 1;
        GameObject.Find("PastillasAkira").GetComponent<TextMeshProUGUI>().text = puntuacionPasillas.ToString();

        //agregar puntuacion a playerprefs
        PlayerPrefs.SetInt("puntuacionPasillas",puntuacionPasillas);




        //GameObject.Find("Player").GetComponent<PlayerComportamiento>().agregarPatillas(1);


        /*
        puntuacionPasillas = puntuacionPasillas + pastilla;
        textoPuntuacionPastilla.text = puntuacionPasillas.ToString();
        */


        //guardar númerro de pastillas en PlayerComportaiento
    }
}
