using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu: MonoBehaviour
{
    public Image character;
    public Sprite funCharacter;

    public void StartButton() {
        SceneManager.LoadScene(1);
    }
    public void FunButton() {
        character.sprite = funCharacter;
    }
    public void QuitButton() {
        Application.Quit();
    }
}
