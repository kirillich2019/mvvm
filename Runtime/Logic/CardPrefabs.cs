using System;
using System.Linq;
using PiratesProtobuf.PlayerState;
using UnityEngine;

namespace UISystem.Logic
{
    /// <summary>
    /// Привязка префбов карт к их типам вью моделей
    /// </summary>
    [CreateAssetMenu(fileName = "CardPrefabs", menuName = "ScriptableObjects/CardPrefabs", order = 0)]
    public class CardPrefabs : ScriptableObject
    {
        [SerializeField] private CardPrefab[] cardPrefabs;

        public ViewModelHolder GetPrefab(CardType cardType) => cardPrefabs.First(prefab => prefab.type == cardType).prefab;
    }

    [Serializable]
    public class CardPrefab
    {
        public ViewModelHolder prefab;
        public CardType type;
    }
}