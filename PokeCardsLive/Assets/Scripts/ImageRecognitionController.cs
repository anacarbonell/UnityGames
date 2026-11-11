using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageRecognitionController : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager trackedImageManager;

    [SerializeField]
    private PokemonData[] pokemonData;

    private Dictionary<string, GameObject> pokemons = new Dictionary<string, GameObject>();

    void Awake()
    {
        foreach (PokemonData data in pokemonData)
        {
            GameObject prefab = Instantiate(data.prefab);
            prefab.SetActive(false);
            pokemons.Add(data.imageName, prefab);
        }
    }

    void OnEnable()
    {
        trackedImageManager.trackablesChanged.AddListener(OnTrackedImagesChanged);
    }

    void OnDisable()
    {
        trackedImageManager.trackablesChanged.RemoveListener(OnTrackedImagesChanged);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {
            UpdateImage(trackedImage);
            Debug.Log("POKECARDSLIVE Detectada: " + trackedImage.referenceImage.name);
        }
        foreach (ARTrackedImage trackedImage in args.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (var removed in args.removed)
        {
            Debug.Log("POKECARDSLIVE Elimina: " + removed.Value.referenceImage.name);
            HideImage(removed.Value);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (imageName != null && pokemons.ContainsKey(imageName))
        {
            GameObject pokemon = pokemons[imageName];
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                pokemon.transform.position = trackedImage.transform.position;
                pokemon.transform.rotation = trackedImage.transform.rotation;
                pokemon.SetActive(true);
            }
            else
            {
                Debug.Log("POKECARDSLIVE Desaparece: " + imageName);
                pokemon.SetActive(false);
            }
        }
    }

    private void HideImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (imageName != null && pokemons.ContainsKey(imageName))
        {
            pokemons[imageName].SetActive(false);
        }
    }
}
