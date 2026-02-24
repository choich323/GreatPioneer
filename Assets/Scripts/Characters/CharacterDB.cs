using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDB", menuName = "Custom/CharacterDB")]
public class CharacterDB : ScriptableObject
{
    public List<GameObject> characterPrefabs;
}