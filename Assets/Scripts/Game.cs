using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    public GameObject menu;
    public Interactor interactor;
    public FirstPersonController firstPersonController;
    public Transform whiteCubeTransform;
    public Transform grayCubeTransform;
    public Transform greenCubeTransform;
    public Transform blueCubeTransform;
    public int width = 64;
    public int height = 64;

    // Use this for initialization
    void Start () {
        SetMenuVisibility(false);

        float scale = 20.0f;
        int halfWidth = width / 2;
        int halfHeight = height / 2;

        float offX = Random.Range(0f, 999f);
        float offZ = Random.Range(0f, 999f);

        for (int z = -halfHeight; z < height - halfHeight; z++) {
            for (int x = -halfWidth; x < width - halfWidth; x++) {
                float fx = x / scale + offX;
                float fz = z / scale + offZ;
                int y = (int) (Mathf.PerlinNoise(fx, fz) * scale);
                if (y < 4) {
                    y = 4;
                }
                int dropTo = y - 1;
                while (y >= dropTo) {
                    CreateBlock(x, y, z);
                    y--;
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonUp("Cancel") || Input.GetMouseButtonUp(1)) {
            SetMenuVisibility(!menu.activeSelf);
        }
    }

    public bool IsGamePaused() {
        return menu.activeSelf;
    }

    public void SetMenuVisibility(bool visible) {
        menu.SetActive(visible);
        Cursor.visible = visible;
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
        firstPersonController.enabled = !visible;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMining(CubePropperties cubePropperties) {
        interactor.ShowMiningCursor();
        interactor.SetCubeName(cubePropperties.info);
        interactor.SetStamina(cubePropperties.stamina);
    }

    public void OnStopMiningRequest() {
        interactor.ShowGameCursor();
        interactor.HideCubeInfo();
    }

    private void CreateBlock(int x, int y, int z) {
        Transform transform;
        if (y > 15) {
            transform = grayCubeTransform;
        } else if (y > 5) {
            transform = greenCubeTransform;
        } else if (y > 4) {
            transform = whiteCubeTransform;
        } else {
            transform = blueCubeTransform;
        }

        Instantiate(transform, new Vector3(x, y, z), transform.rotation);
    }
}
