using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageRecognitionController : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager trackedImageManager;

    [SerializeField]
    private PokemonData[] pokemonData;

    [SerializeField]
    private GameObject infoCanvas;

    [SerializeField]
    private TextMeshProUGUI infoText;

    [SerializeField]
    private GameObject showCanvas;

    [SerializeField]
    private TextMeshProUGUI showText;

    private AudioSource audioSource;

    private Dictionary<string, GameObject> pokemons = new Dictionary<string, GameObject>();
    private Dictionary<string, PokemonData> infoPokemons = new Dictionary<string, PokemonData>();

    private string infoShowing = "";
    private float showTimer = 0f;
    private float showTime = 3f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        foreach (PokemonData data in pokemonData)
        {
            GameObject prefab = Instantiate(data.prefab);
            prefab.SetActive(false);
            pokemons.Add(data.imageName, prefab);
            infoPokemons.Add(data.imageName, data);
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
    void Update()
    {
        if (showTimer > 0)
        {
            showTimer -= Time.deltaTime;
            if (showTimer <= 0)
            {
                showTimer = 0f;
                showCanvas.SetActive(false);
            }
        }
    }

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
                if (infoPokemons.ContainsKey(imageName))
                {
                    PokemonData infoPokemon = infoPokemons[imageName];

                    if (!pokemon.activeSelf)
                    {
                        showText.text = "¡Un " + infoPokemon.displayName + " salvaje ha aparecido!";
                        showTimer = showTime;
                        showCanvas.SetActive(true);
                        audioSource.PlayOneShot(infoPokemon.sound);
                    }

                    if (infoShowing == "")
                    {
                        infoShowing = imageName;
                        infoText.text =
                            infoPokemon.displayName
                            + "\nTipo: "
                            + infoPokemon.type
                            + "\n"
                            + infoPokemon.description;
                        infoCanvas.SetActive(true);
                    }
                }

                pokemon.transform.position = trackedImage.transform.position;
                pokemon.transform.rotation = trackedImage.transform.rotation;
                pokemon.SetActive(true);
            }
            else
            {
                Debug.Log("POKECARDSLIVE Desaparece: " + imageName);
                HidePokemon(imageName);
            }
        }
    }

    private void HideImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (imageName != null && pokemons.ContainsKey(imageName))
        {
            HidePokemon(imageName);
        }
    }

    private void HidePokemon(string imageName)
    {
        pokemons[imageName].SetActive(false);

        if (infoShowing.Equals(imageName))
        {
            infoShowing = "";
            infoCanvas.SetActive(false);
        }
    }
}
