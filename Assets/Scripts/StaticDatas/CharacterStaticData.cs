using UnityEngine;

namespace CardBuildingGame.StaticDatas
{
    [CreateAssetMenu(fileName = "NewCharacter", menuName = "StaticData/Character")]
    public class CharacterStaticData : StaticData
    {
        public override string StaticDataID => Name;

        [Header("Info")]
        public string Name = "EmptyName";

        [Header("Bundle")]
        public string BundlePath;
        
        [Header("Health")]
        public int Health = 30;
        public int MaxHealth = 30;
        public int Defense = 0;
        public int Energy = 10;

        private void OnValidate()
        {
            if (Health > MaxHealth)
                Health = MaxHealth;

            if (MaxHealth < 1) MaxHealth = 1;
            if (Defense < 0) Defense = 0;
            if (Health < 1) Health = 1;

            if (BundlePath == null)
                Debug.Log("Assign BundlePath for CharacterStaticData");
        }
    }
}