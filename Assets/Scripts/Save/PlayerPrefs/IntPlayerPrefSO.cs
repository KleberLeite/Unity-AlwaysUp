using UnityEngine;

namespace AlwaysUp.Save
{
    [CreateAssetMenu(menuName = "Save/Int PlayerPref")]
    public class IntPlayerPrefSO : PlayerPrefSO<int>
    {
        public override int Get() => PlayerPrefs.GetInt(_key, _defaultValue);

        public override void Reset() => PlayerPrefs.SetInt(_key, _defaultValue);

        public override void Set(int value) => PlayerPrefs.SetInt(_key, value);
    }
}
