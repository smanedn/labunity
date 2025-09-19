using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Variabili SerializeField

    [SerializeField] private float speed;
    [SerializeField] private float inputX;
    [SerializeField] private float inputZ;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private AudioSource coinSound;
    [SerializeField] private AudioSource dmgSound;
    [SerializeField] private int lives;
    [SerializeField] private TMP_Text livesText;
    

    #endregion

    private int nPoint;

    #region Unity Methods

    private void Start()
    { 
        UpdateLivesText();
    }

    private void Update()
    {
        // Legge l'input da tastiera/joystick
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        GetComponent<Rigidbody>().linearVelocity = new Vector3(inputX, 0, inputZ) * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            // Cambia colore della moneta a verde per feedback visivo
            Renderer coinRenderer = collision.gameObject.GetComponent<Renderer>();
            if (coinRenderer != null)
            {
                coinRenderer.material.color = Color.green;
            }

            if (coinSound != null)
            {
                coinSound.Play();
            }

            // Distrugge la moneta dopo 1 secondo
            StartCoroutine(DestroyCoinAfterDelay(collision.gameObject, 1f));

            nPoint += 10;
            UpdateCoinsText();
        }
        else if (collision.gameObject.CompareTag("Porta"))
        {
            // Inizia animazione apertura porta
            StartCoroutine(OpenDoor(collision.gameObject));
        }
        else if (collision.gameObject.CompareTag("Buco") || collision.gameObject.CompareTag("Enemy"))
        {
            // Suono danno e perde una vita
            if (dmgSound != null)
            {
                dmgSound.Play();
            }
            LoseLife(1);
        }
        else if (collision.gameObject.CompareTag("Checkpoint"))
        {
            SceneManager.LoadScene("Labyrint");
        }
    }

    #endregion

    #region Gestione Vite e Punti


    // Aggiorna il testo delle vite nell'interfaccia utente.
    private void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "Vite: " + lives;
        }
    }


    // Riduce le vite e controlla la morte del giocatore.
    private void LoseLife(int amount = 1)
    {
        lives -= amount;
        if (lives < 0) lives = 0;

        UpdateLivesText();

        if (lives == 0)
        {
            Debug.Log("Giocatore morto!");
            SceneManager.LoadScene("Labyrint");
        }
    }


    // Aggiorna il testo dei punti nell'interfaccia utente.
    private void UpdateCoinsText()
    {
        if (coinsText != null)
        {
            coinsText.text = "Punti: " + nPoint;
        }
    }

    #endregion

    #region Coroutine

    // Distrugge la moneta dopo un ritardo per permettere effetti sonori/visivi.
    private IEnumerator DestroyCoinAfterDelay(GameObject coin, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(coin);
    }

    // Anima l'apertura e chiusura della porta spostandola verticalmente.
    private IEnumerator OpenDoor(GameObject door)
    {
        Vector3 startPos = door.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 2f, 0);
        float duration = 1.0f;
        float elapsed = 0f;

        // Interpolazione lineare per aprire la porta verso l'alto
        while (elapsed < duration)
        {
            // Vector3.Lerp calcola la posizione tra startPos e endPos in base al tempo
            door.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        door.transform.position = endPos;

        // Attesa porta aperta
        yield return new WaitForSeconds(2f);

        // Reset tempo per chiudere la porta
        elapsed = 0f;
        while (elapsed < duration)
        {
            door.transform.position = Vector3.Lerp(endPos, startPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        door.transform.position = startPos;
    }

    #endregion
}
