﻿using UnityEngine;
using System.Collections;

public class PlayerSelectManager : MonoBehaviour {
    public Entity currentCharacterSelected;
    public bool ignoreClick;
    
    Camera mainCamera;
    GameManager gameManager;
    PlayerModal playerModal;

    void Start()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>();
        gameManager = GetComponent<GameManager>();
        playerModal = GameObject.FindObjectOfType<PlayerModal>();
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
            if (tile != null)
            {
                Entity tileEntity = tile.getCurrentEntity();
                if (tileEntity != null && tileEntity.entityType == gameManager.currentTurn)
                {
                    currentCharacterSelected = tile.getCurrentEntity();
                    playerModal.SetUIPos(tileEntity.transform);
                    playerModal.Enable();
                }
            }
        }
        else
        {
            resetSelection();
        }
        gameManager.performAction();
    }

    public void resetSelection()
    {
        currentCharacterSelected = null;
    }
}
