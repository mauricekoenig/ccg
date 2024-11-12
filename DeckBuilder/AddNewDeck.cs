using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddNewDeck : MonoBehaviour {

    private CollectionViewport viewport;

    public void Init (CollectionViewport viewPort) {

        this.viewport = viewPort;
    }
    public void CreateNewDeck () {

        if (viewport == null) return;
        viewport.AddNewDeck();
    }
}
