using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayButton : MonoBehaviour
{
    private XRBaseInteractable interactable;
    public sceneFader SceneFader;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(switchScenes);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void switchScenes(BaseInteractionEventArgs args)
    {
        if (args.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor)args.interactorObject;
            SceneManager.LoadScene("Main Game");
        }
    }
}
