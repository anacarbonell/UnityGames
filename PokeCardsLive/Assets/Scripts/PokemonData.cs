using UnityEngine;

[CreateAssetMenu(fileName = "PokemonData", menuName = "PokeCardsLive/Pokemon")]
public class PokemonData : ScriptableObject
{
    public string imageName;
    public GameObject prefab;
}
