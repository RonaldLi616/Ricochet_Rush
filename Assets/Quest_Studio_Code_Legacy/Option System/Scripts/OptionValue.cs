using UnityEngine;

namespace Quest_Studio
{
    [CreateAssetMenu(menuName = "Option Values")]

    public class OptionValue : ScriptableObject
    {
        /*<--------------------Basic Status-------------------->*/
        // Volume
        #region
        [Header("Volume")]
        [Range(0f, 1f)] public float mainVolume;
        public bool mainIsMute;
        [Range(0f, 1f)] public float musicVolume;
        public bool musicIsMute;
        [Range(0f, 1f)] public float soundVolume;
        public bool soundIsMute;

        #endregion

        /*<--------------------Extra-------------------->*/
        // Language
        #region 
        public enum Language
        {
            English,
            TraditionalChinese,
            Cantonese
        }
        [Header("Language")]
        public Language language = Language.English;

        #endregion

    }
}