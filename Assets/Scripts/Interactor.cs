using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour {

    public GameObject gameCursor;
    public GameObject mineCursor;
    public Text cubeName;
    public Text stamina;

    public void SetActive(bool active) {
        gameObject.SetActive(active);
    }

    public void SetCubeName(string name) {
        cubeName.text = name;
        if (!cubeName.gameObject.activeSelf) {
            cubeName.gameObject.SetActive(true);
        }
    }

    public void SetStamina(int stamina) {
        this.stamina.text = stamina + "";
        if (!this.stamina.gameObject.activeSelf) {
            this.stamina.gameObject.SetActive(true);
        }
    }

    public void ShowGameCursor() {
        SwitchCursors(true);
    }

    public void ShowMiningCursor() {
        SwitchCursors(false);
    }

    public void HideCubeInfo() {
        if (cubeName.gameObject.activeSelf) {
            cubeName.gameObject.SetActive(false);
        }
        if (stamina.gameObject.activeSelf) {
            stamina.gameObject.SetActive(false);
        }
    }

    private void SwitchCursors(bool showGameCursor) {
        if (gameCursor.activeSelf != showGameCursor) {
            gameCursor.SetActive(showGameCursor);
        }
        if (mineCursor.activeSelf == showGameCursor) {
            mineCursor.SetActive(!showGameCursor);
        }
    }
}
