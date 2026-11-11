using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

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
            Debug.Log("POKECARDSLIVE Detectada: " + trackedImage.referenceImage.name);
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in args.updated)
            UpdateImage(trackedImage);
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (imageName != null && pokemons.ContainsKey(imageName))
        {
            GameObject pokemon = pokemons[imageName];
            pokemon.transform.position = trackedImage.transform.position;
            pokemon.transform.rotation = trackedImage.transform.rotation;
            pokemon.SetActive(true);

            Debug.Log("POKECARDSLIVE Detectada: " + imageName);
        }
    }
}
