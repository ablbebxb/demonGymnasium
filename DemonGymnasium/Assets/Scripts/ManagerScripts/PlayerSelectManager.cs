using UnityEngine;
using System.Collections;

public class PlayerSelectManager : MonoBehaviour {
    public Entity currentCharacterSelected;
    public bool ignoreClick;

    Camera mainCamera;
    GameManager gameManager;

    void Start()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>();
        gameManager = GetComponent<GameManager>();
    }

    void Update()
    {
        if (!ignoreClick && Input.GetButtonDown("Fire1"))
        {
            mouseClicked();
        }
    }

    void mouseClicked()
    {

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Tile tile = hit.collider.GetComponent<Tile>();
            if (tile != null && gameManager.currentTurn == tile.getCurrentEntity().entityType)
            {
                currentCharacterSelected = tile.getCurrentEntity();
            }
        }
        else
        {
            resetSelection();
        }
    }

    public void resetSelection()
    {
        currentCharacterSelected = null;
    }
}
