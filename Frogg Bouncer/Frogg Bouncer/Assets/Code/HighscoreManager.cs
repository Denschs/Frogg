using TMPro;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public int highscoreFairiesGuests;
    public int highscoreButterfliesFed;

    // Schl�ssel f�r die Highscores in PlayerPrefs
    private string highscoreFairiesGuestsKey = "HighscoreFairiesGuests";
    private string highscoreButterfliesFedKey = "HighscoreButterfliesFed";

    // Referenzen auf die TextMeshPro-Objekte f�r die Anzeige der Highscores
    public TextMeshProUGUI fairiesHighscoreText;
    public TextMeshProUGUI butterfliesHighscoreText;

    void Start()
    {
        CodeEventHandler.GameEnded += SaveHighscores;
        // Beim Starten des Spiels die Highscores aus den PlayerPrefs laden
        LoadHighscores();
        // Aktualisiere die angezeigten Highscores
        UpdateHighscoreDisplay();
        //PlayerPrefs.DeleteAll();
    }

    public void SaveHighscores(int fairiesGuestsScore, int butterfliesFedScore)
    {
        // �berpr�fen, ob die aktuellen Highscores h�her sind als die in den PlayerPrefs gespeicherten Highscores
        if (fairiesGuestsScore > highscoreFairiesGuests)
        {
            highscoreFairiesGuests = fairiesGuestsScore;
            PlayerPrefs.SetInt(highscoreFairiesGuestsKey, highscoreFairiesGuests);
        }

        if (butterfliesFedScore > highscoreButterfliesFed)
        {
            highscoreButterfliesFed = butterfliesFedScore;
            PlayerPrefs.SetInt(highscoreButterfliesFedKey, highscoreButterfliesFed);
        }

        PlayerPrefs.Save(); // Wichtig, um die �nderungen zu speichern

        // Aktualisiere die angezeigten Highscores
        UpdateHighscoreDisplay();
    }

    public void LoadHighscores()
    {
        // Die Highscores aus den PlayerPrefs laden und in die entsprechenden Variablen setzen
        highscoreFairiesGuests = PlayerPrefs.GetInt(highscoreFairiesGuestsKey, 0);
        highscoreButterfliesFed = PlayerPrefs.GetInt(highscoreButterfliesFedKey, 0);
    }

    // Methode zum Aktualisieren der angezeigten Highscores
    public void UpdateHighscoreDisplay()
    {
        // Aktualisiere den Text des TextMeshPro-Objekts f�r Fairies mit dem entsprechenden Highscore
        fairiesHighscoreText.text = highscoreFairiesGuests.ToString();
        // Aktualisiere den Text des TextMeshPro-Objekts f�r Butterflies mit dem entsprechenden Highscore
        butterfliesHighscoreText.text = highscoreButterfliesFed.ToString();
    }
    private void OnDisable()
    {
        CodeEventHandler.GameEnded -= SaveHighscores;
    }
}
