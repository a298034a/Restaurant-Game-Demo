using Sirenix.OdinInspector;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Restaurant
{
    [CreateAssetMenu(fileName = "FurnitureData", menuName = "Game Data/FurnitureData", order = 1)]
    public class FurnitureData : ScriptableObject
    {
        public string Name;
        public string Type;
        public int Price;
        public int Charm;
        public Vector3Int Size = new Vector3Int(1, 1, 1);
        public Sprite[] Sprites;

#if UNITY_EDITOR
        [Button("Get Sprites")]
        private void _GetSprites()
        {
            if (string.IsNullOrEmpty(Name))
            { return; }

            Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath("Assets/_Art/Furniture/" + Name + ".png").OfType<Sprite>().ToArray();
            Sprites = new Sprite[sprites.Length];

            for (int j = 0; j < sprites.Length; j++)
            {
                Sprites[j] = sprites[j];
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
#endif
    }
}
