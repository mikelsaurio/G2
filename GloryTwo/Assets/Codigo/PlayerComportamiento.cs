using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerComportamiento : MonoBehaviour
{
    [Header("Jugador")]
    //public Rigidbody rb;
    public GameObject player;
    public float velocidadDeFrente;
    public float velocidadLado;

    [Space]
    [Header("Materiales")]
    //public GameObject obstaculo;
    public Material azul;
    public Material rosa;
    public Material sky;

    [Space]
    [Header("Elementos UI")]
    public TextMeshProUGUI textoPuntuacion;
    //public TextMeshProUGUI textoPuntuacionPastilla;
    public GameObject pausePanel;
    public GameObject endGamePanel;
    public GameObject gamePanel;
    //public Canvas canvas; hacer todo desde el canvas

    [Space]
    [Header("Sonidos")]
    public AudioSource sonidoChoque;

    //Variables privadas

    [HideInInspector]
    public float puntuacion;
    public int puntuacionPasillas;

    public System.DateTime startTime;
    public System.DateTime gameOverTimeFinal;
    public System.DateTime gameOverTimeInicial;
    public System.TimeSpan ts;
    public System.TimeSpan tsGameOver;
    private float tiempoDetenido;
    private double tiempoTranscurrido;
    public bool gameOverBool;
    private float posicionZ;
    private float posicionY;
    private bool moverDer;
    private bool moverIzq;
    private bool cambiarColor;
    private bool rosaColor;

    void Start()
    {
        //Color inicial Atmósfera
        //sky.SetFloat("_AtmosphereThickness", 5);
        
        //Timepo global de inicio del juego
        startTime = System.DateTime.UtcNow;

        //Color inicial del jugador
        player.tag = "rosa";
        player.GetComponent<Renderer>().material = rosa;
        cambiarColor = true;
        rosaColor = true;


    //Juego todavía no se pierde
        gameOverTimeFinal = System.DateTime.UtcNow;
        gameOverTimeInicial = System.DateTime.UtcNow;
        gameOverBool = false;
        tiempoDetenido = 0;
        endGamePanel.SetActive(false);
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
        GameObject.Find("PastillasAkira").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("puntuacionPasillas").ToString();

        //Movimiento inicial
        moverDer = false;
        moverIzq = false;

    }

    void FixedUpdate()
    {

        //Si pierdes desactivas el movimiento del jugador
        if (gameOverBool == false)
        {
            movimientoJugador();
        }

        medirPuntuacionTiempo();

        //cambiarCielo();

        manejoColisiones();

    }

    public void regresarJuego()
    {
        //Revisa si tienes minimo una pastilla para poder continuar
        puntuacionPasillas = PlayerPrefs.GetInt("puntuacionPasillas");
        if (puntuacionPasillas>=1)
        {

            //Habilita movimiento 
            gameOverBool = false;

            //aparece la puntuación y desaparece el panel de fin de juego
            gamePanel.SetActive(true);
            endGamePanel.SetActive(false);

            //regresar jugador a posición antigua
            player.transform.position = new Vector3(0, posicionY + 3, posicionZ);

            //quitar fuerzas
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;

            //la puntuación corre desde el instante en el que se perdió
            gameOverTimeFinal = System.DateTime.UtcNow;

            System.TimeSpan tsGameOver = gameOverTimeFinal - gameOverTimeInicial;

            tiempoDetenido += (float)Math.Floor(tsGameOver.TotalMilliseconds / 100);

            //La música vuelve a sonar
            player.GetComponent<AudioSource>().UnPause();

            //descuenta la pastilla
            puntuacionPasillas -= 1;
            PlayerPrefs.SetInt("puntuacionPasillas",puntuacionPasillas);

            //escribe las pastillas actuales
            GameObject.Find("PastillasAkira").GetComponent<TextMeshProUGUI>().text = puntuacionPasillas.ToString();
        }
        else
        {
            GameObject.Find("ContinueText").GetComponent<TextMeshProUGUI>().text = "Need more  -";
        }

    }

    private void manejoColisiones()
    {
        //manejo de colisiones
        if (player.tag == "rosa")
        {
            Physics.IgnoreLayerCollision(9, 10,false);
            Physics.IgnoreLayerCollision(9, 11,true);
            
        }

        if (player.tag == "azul")
        {
            Physics.IgnoreLayerCollision(9, 10,true);
            Physics.IgnoreLayerCollision(9, 11,false);
        }
    }

    private void movimientoJugador()
    {


        //Avanzar hacia adelante a velocidad constante
        player.GetComponent<Rigidbody>().AddForce(0, 0, velocidadDeFrente * Time.deltaTime, ForceMode.VelocityChange);
        
        //Mover hacia la izqquierda
        if (moverIzq)
        {
            player.GetComponent<Rigidbody>().AddForce(-velocidadLado * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        
        //mover hacia la derecha
        if (moverDer)
        {
            player.GetComponent<Rigidbody>().AddForce(velocidadLado * Time.deltaTime,0, 0, ForceMode.VelocityChange);
        }

        //Cambiar color
        if (moverDer && moverIzq)
        {
            if (cambiarColor == true)
            {
                //Debug.Log("Entraste");

                cambiarColor = false;

                if (rosaColor==true)
                {
                    player.GetComponent<Renderer>().material = azul;
                    player.tag = "azul";
                    rosaColor = false;

                }
                else
                {
                    player.GetComponent<Renderer>().material = rosa;
                    player.tag = "rosa";
                    rosaColor = true;
                }
            }


            
        }

        if (moverDer==false && moverIzq == false)
        {
            cambiarColor = true;

            //Debug.Log("Saliste");
        }


        /*
        if (Input.GetKey("d"))
        {
            moverDer = true;
        }
        else
        {
            moverDer = false;
            cambiarColor = true;
        }


        if (Input.GetKey("a"))
        {
            moverIzq = true;
        }
        else
        {
            moverIzq = false;
            cambiarColor = true;
        }
        */
    }

    private void cambiarCielo()
    {
        //cambiar fondo
        //RenderSettings.skybox.SetFloat("_Exposure", 2);
        //RenderSettings.skybox.SetFloat("_Atmosphere_Thickness", 2);
        //Debug.Log(sky.shader.GetPropertyName(3));
        //_AtmosphereThickness

        sky.SetFloat("_AtmosphereThickness", Convert.ToSingle(5 - 2 * ts.TotalSeconds / 100));
    }

    private void medirPuntuacionTiempo()
    {
        //RELOJ 

        //Medir tiempo
        System.TimeSpan ts = System.DateTime.UtcNow - startTime;

        //Medir Puntuación
        tiempoTranscurrido = Math.Floor(ts.TotalMilliseconds / 100);

        if (gameOverBool==false)
        {
            //Debug.Log(tiempoDetenido + "<-TiempoDetenidoDelta");
            puntuacion = (float)tiempoTranscurrido - tiempoDetenido;
        }

        textoPuntuacion.text = puntuacion.ToString();

        //Debug.Log(ts.Seconds.ToString());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != player.tag && collision.collider.tag != "piso")
        {
            //Colisión con objeto que sea de diferente color y no sea el piso  
            if (gameOverBool==false)
            {
                Debug.Log("GAMEOVER");
                gameOver();
            }
        }
    }

    private void gameOver()
    {
        //Enseñar anuncio
        FindObjectOfType<ADS>().ShowInterstitialAd();

        //Movimiento inicial
        moverDer = false;
        moverIzq = false;

        //sonido game over 
        sonidoChoque.Play();

        //desaparece la puntuación y aparece el menu game over
        gamePanel.SetActive(false);
        endGamePanel.SetActive(true);

        //este código se corre únicamente una vez después de haber perdido
        gameOverBool = true;

        //Cuantos segundos llevabas en el momento que perdiste
        gameOverTimeInicial = System.DateTime.UtcNow;

        //Detiene la música
        player.GetComponent<AudioSource>().Pause();

        //Posición en la que perdiste
        posicionZ = player.transform.position.z;
        posicionY = player.transform.position.y;

        //porcentaje completado
        GameObject.Find("Percentage").GetComponent<TextMeshProUGUI>().text = Math.Floor(puntuacion * 100 / 810) + "% COMPLETED";

    }

    public void restartGame()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void returnToMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

    public void pauseGame()
    {
        Time.timeScale = 0;

        //desaparece la puntuación y aparece el panel de pausa
        gamePanel.SetActive(false);
        pausePanel.SetActive(true);

        //Cuantos segundos llevabas en el momento que perdiste
        gameOverTimeInicial = System.DateTime.UtcNow;

        //Detiene la música
        player.GetComponent<AudioSource>().Pause();
    }

    public void resumeGame()
    {
        Time.timeScale = 1;

        //aparece la puntuación y desapaarece el menu de pausa
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);

        //quitar fuerzas
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        //la puntuación corre desde el instante en el que se perdió
        gameOverTimeFinal = System.DateTime.UtcNow;

        System.TimeSpan tsGameOver = gameOverTimeFinal - gameOverTimeInicial;

        tiempoDetenido += (float)Math.Floor(tsGameOver.TotalMilliseconds / 100);

        //La música vuelve a sonar
        player.GetComponent<AudioSource>().UnPause();
    }

    public void ButtonDerechoDown()
    {
        moverDer = true;
    }

    public void ButtonDerechoUp()
    {
        moverDer =false;
        cambiarColor = true;
    }

    public void ButtonIzqDown()
    {
        moverIzq = true;
        
    }

    public void ButtonIzqUp()
    {
        moverIzq = false;
        cambiarColor = true;
    }
}




