using UnityEngine;

public class Player : MonoBehaviour {

    public Game gameManager;
    public Transform addCubeTransform;

    private Transform selectedCube;

    private bool mouseEventDownLocked = false;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameManager.IsGamePaused()) {
            Transform cameraTransform = GetComponent<Camera>().transform;
            Ray ray = new Ray(cameraTransform.position + cameraTransform.forward / 2, cameraTransform.forward);

            if (selectedCube != null) {
                HandleAddition(ray);
            } else {
                HandleDeletion(ray);
            }

            if (Input.GetMouseButtonUp(0)) {
                mouseEventDownLocked = false;
            }
        }
	}

    private void HandleAddition(Ray ray) {
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, 6.0f)) {
            Vector3 position = raycastHit.point + raycastHit.normal / 2;
            
            addCubeTransform.position = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), Mathf.Round(position.z));

            if (Input.GetMouseButtonDown(0)) {
                mouseEventDownLocked = true;
                Instantiate(selectedCube, addCubeTransform.position, addCubeTransform.rotation);
                selectedCube = null;
                SetAddCubeVisibility(false);
            } else {
                SetAddCubeVisibility(true);
            }
        } else {
            SetAddCubeVisibility(false);
        }

        gameManager.OnStopMiningRequest();
    }

    private void HandleDeletion(Ray ray) {
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, 2.0f)) {
            CubePropperties cubePropperties = raycastHit.collider.gameObject.GetComponent<CubePropperties>();
            int stamina = cubePropperties.stamina;

            gameManager.OnMining(cubePropperties);

            if (!mouseEventDownLocked && Input.GetMouseButton(0)) {
                stamina--;
                if (stamina <= 0) {
                    Destroy(raycastHit.collider.gameObject);
                } else {
                    cubePropperties.stamina = stamina;
                }
            }
        } else {
            gameManager.OnStopMiningRequest();
        }

        SetAddCubeVisibility(false);
    }

    public void SetCubeToCreate(Transform cubeToCreate) {
        selectedCube = cubeToCreate;
        Color color = selectedCube.gameObject.GetComponent<Renderer>().sharedMaterial.color;
        color.a = 0.5f;
        addCubeTransform.gameObject.GetComponent<Renderer>().sharedMaterial.color = color;
        gameManager.SetMenuVisibility(false);
    }

    public void CancelCubeToCreateSelection() {
        selectedCube = null;
        gameManager.SetMenuVisibility(false);
    }

    private void SetAddCubeVisibility(bool visible) {
        if (addCubeTransform.gameObject.activeSelf != visible) {
            addCubeTransform.gameObject.SetActive(visible);
        }
    }
}
