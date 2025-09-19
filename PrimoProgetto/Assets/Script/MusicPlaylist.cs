using UnityEngine;
using System.Collections;

public class MusicPlaylist : MonoBehaviour
{
    #region SerializeField Variabili

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip track1;
    [SerializeField] private AudioClip track2;

    #endregion

    #region Unity Methods

    // Avvia la coroutine per riprodurre le tracce in sequenza all'inizio.
    private void Start()
    {
        StartCoroutine(PlayTracksSequentially());
    }

    #endregion

    #region Coroutine

    // Riproduce i brani uno dopo l'altro aspettando la fine di ciascuno.
    private IEnumerator PlayTracksSequentially()
    {
        audioSource.clip = track1;
        audioSource.Play();
        // Attende la durata della prima traccia
        yield return new WaitForSeconds(track1.length);

        audioSource.clip = track2;
        audioSource.Play();
        yield return new WaitForSeconds(track2.length);

    }

    #endregion
}
