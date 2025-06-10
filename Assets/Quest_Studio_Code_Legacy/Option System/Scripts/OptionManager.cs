using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.UI;

namespace Quest_Studio
{
    public class OptionManager : MonoBehaviour
    {
        // Option Value
        #region 
        [Header("Option Value")]
        [SerializeField] private OptionValue optionValue;
        private OptionValue GetOptionValue() {
            if (optionValue == null) { Debug.Log("Missing Option Value Reference!"); }
            return optionValue;
        }
        #endregion

        // Return Button
        #region 
        [Header("Return Button")]
        [SerializeField] private Button returnBtn;
        private Button GetReturnBtn()
        { 
            if (returnBtn == null) { Debug.Log("Missing Return Button Reference!"); }
            return returnBtn;
        }

        private void OnClickedReturnBtn()
        {
            this.gameObject.SetActive(false);
        }
        #endregion

        // Volume Setting
        #region
        [Header("Volume Setting")]
        // Main
        #region 
        [SerializeField] private Slider mainSlider;
        private Slider GetMainSlider()
        {
            if (mainSlider == null) { Debug.Log("Missing Main Slider Reference!"); }
            return mainSlider;
        }

        private void UpdateMainSliderValue()
        {
            GetOptionValue().mainVolume = GetMainSlider().value;
        }

        [SerializeField] private Button mainMuteBtn;
        private Button GetMainMuteBtn()
        { 
            if (mainMuteBtn == null) { Debug.Log("Missing Main Mute Button Reference!"); }
            return mainMuteBtn;
        }
        
        private Image GetMainMuteImage()
        {
            Image mainMuteImage = mainMuteBtn.GetComponent<Image>();
            if (mainMuteImage == null) { Debug.Log("Missing Main Mute Image Component!"); }
            return mainMuteImage;
        }

        private void OnClickedMainMuteBtn()
        {
            GetOptionValue().mainIsMute = !GetOptionValue().mainIsMute;
            GetMainMuteImage().sprite = GetMuteSprite(GetOptionValue().mainIsMute);
        }
        #endregion

        // Music
        #region 
        [SerializeField] private Slider musicSlider;
        private Slider GetMusicSlider()
        {
            if (musicSlider == null) { Debug.Log("Missing Music Slider Reference!"); }
            return musicSlider;
        }

        private void UpdateMusicSliderValue()
        {
            GetOptionValue().musicVolume = GetMusicSlider().value;
        }

        [SerializeField] private Button musicMuteBtn;
        private Button GetMusicMuteBtn()
        { 
            if (musicMuteBtn == null) { Debug.Log("Missing Music Mute Button Reference!"); }
            return musicMuteBtn;
        }
        
        private Image GetMusicMuteImage()
        {
            Image musicMuteImage = musicMuteBtn.GetComponent<Image>();
            if (musicMuteImage == null) { Debug.Log("Missing Music Mute Image Component!"); }
            return musicMuteImage;
        }

        private void OnClickedMusicMuteBtn()
        {
            GetOptionValue().musicIsMute = !GetOptionValue().musicIsMute;
            GetMusicMuteImage().sprite = GetMuteSprite(GetOptionValue().musicIsMute);
        }
        #endregion

        // Sound
        #region 
        [SerializeField] private Slider soundSlider;
        private Slider GetSoundSlider()
        {
            if (soundSlider == null) { Debug.Log("Missing Sound Slider Reference!"); }
            return soundSlider;
        }

        private void UpdateSoundSliderValue()
        {
            GetOptionValue().soundVolume = GetSoundSlider().value;
        }

        [SerializeField] private Button soundMuteBtn;
        private Button GetSoundMuteBtn()
        { 
            if (soundMuteBtn == null) { Debug.Log("Missing Sound Mute Button Reference!"); }
            return soundMuteBtn;
        }
        
        private Image GetSoundMuteImage()
        {
            Image soundMuteImage = soundMuteBtn.GetComponent<Image>();
            if (soundMuteImage == null) { Debug.Log("Missing Sound Mute Image Component!"); }
            return soundMuteImage;
        }

        private void OnClickedSoundMuteBtn()
        {
            GetOptionValue().soundIsMute = !GetOptionValue().soundIsMute;
            GetSoundMuteImage().sprite = GetMuteSprite(GetOptionValue().soundIsMute);
        }
        #endregion

        // Reference Mute Sprite
        #region 
        [Header("Mute Sprite")]
        [Tooltip("0 = Not Mute, 1 = Is Mute")]
        [SerializeField] private Sprite[] muteSprites = new Sprite[2];
        private Sprite GetMuteSprite(bool isMute)
        {
            Sprite sprite = null;
            foreach (Sprite muteSprite in muteSprites)
            {
                if (muteSprite == null) { Debug.Log("Missing Mute Sprite Reference!"); }
            }
            if (isMute) { sprite = muteSprites[1]; }
            else { sprite = muteSprites[0]; }
            return sprite;
        }
        #endregion

        #endregion

        // Language
        #region
        [Header("Language")]
        [SerializeField] private Toggle[] languageToggles = new Toggle[0];
        private Toggle GetLanguageToggle(OptionValue.Language language)
        {
            if (languageToggles.Length == 0)
            {
                Debug.Log("Missing Toggle Component!");
                return null;
            }
            switch (language)
            {
                case OptionValue.Language.English:
                    return languageToggles[0];

                case OptionValue.Language.TraditionalChinese:
                    return languageToggles[1];

                case OptionValue.Language.Cantonese:
                    return languageToggles[2];

                default:
                    return null;
            }
        }

        private void OnToggleEnglishLanguage()
        {
            if (!GetLanguageToggle(OptionValue.Language.English).isOn) { return; }
            GetOptionValue().language = OptionValue.Language.English;
        }

        private void OnToggleTraditionalChineseLanguage()
        {
            if (!GetLanguageToggle(OptionValue.Language.TraditionalChinese).isOn) { return; }
            GetOptionValue().language = OptionValue.Language.TraditionalChinese;
        }

        private void OnToggleCantoneseLanguage()
        {
            if (!GetLanguageToggle(OptionValue.Language.Cantonese).isOn) { return; }
            GetOptionValue().language = OptionValue.Language.Cantonese;
        }

        #endregion

        // Method
        #region 
        // Set Listener
        #region 
        public virtual void SetListener()
        {
            // Return Button
            #region 
            GetReturnBtn().onClick.AddListener(OnClickedReturnBtn);
            #endregion

            // Main
            #region 
            GetMainSlider().onValueChanged.AddListener(delegate { UpdateMainSliderValue(); });
            GetMainMuteBtn().onClick.AddListener(OnClickedMainMuteBtn);
            #endregion

            // Music
            #region 
            GetMusicSlider().onValueChanged.AddListener(delegate { UpdateMusicSliderValue(); });
            GetMusicMuteBtn().onClick.AddListener(OnClickedMusicMuteBtn);
            #endregion

            // Sound
            #region 
            GetSoundSlider().onValueChanged.AddListener(delegate { UpdateSoundSliderValue(); });
            GetSoundMuteBtn().onClick.AddListener(OnClickedSoundMuteBtn);
            #endregion

            // Language
            #region 
            GetLanguageToggle(OptionValue.Language.English).onValueChanged.AddListener(delegate { OnToggleEnglishLanguage(); });
            GetLanguageToggle(OptionValue.Language.TraditionalChinese).onValueChanged.AddListener(delegate { OnToggleTraditionalChineseLanguage(); });
            GetLanguageToggle(OptionValue.Language.Cantonese).onValueChanged.AddListener(delegate { OnToggleCantoneseLanguage(); });
            #endregion

        }
        #endregion

        // On Start Setup
        #region 
        public virtual void OnStartSetup()
        {
            // Main
            #region 
            GetMainSlider().value = GetOptionValue().mainVolume;
            GetMainMuteImage().sprite = GetMuteSprite(GetOptionValue().mainIsMute);
            #endregion

            // Music
            #region 
            GetMusicSlider().value = GetOptionValue().musicVolume;
            GetMusicMuteImage().sprite = GetMuteSprite(GetOptionValue().musicIsMute);
            #endregion

            // Sound
            #region 
            GetSoundSlider().value = GetOptionValue().soundVolume;
            GetSoundMuteImage().sprite = GetMuteSprite(GetOptionValue().soundIsMute);
            #endregion

            // Language
            #region 
            GetLanguageToggle(GetOptionValue().language).isOn = true;
            #endregion

            this.gameObject.SetActive(false);
        }
        #endregion

        #endregion

        public virtual void Awake()
        {
            // Set Listener
            #region 
            SetListener();
            #endregion

        }

        // Start is called before the first frame update
        public virtual void Start()
        {
            // On Start Setup
            #region 
            OnStartSetup();
            #endregion

        }

    }
}