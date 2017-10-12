using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AssignmentGUI
{
    public class OldGUIManager : MonoBehaviour
    {
        [Header("GUI Elements")]
        public GUIStyle titleStyler = new GUIStyle();
        public GUIStyle playStyler = new GUIStyle();
        public GUIStyle styler = new GUIStyle();
        public GUIStyle optionsStyler = new GUIStyle();
        public GUIStyle optionsMenuStyler = new GUIStyle();
        public GUIStyle resolutionStyler = new GUIStyle();
        public GUILayout layout = new GUILayout();
        public GUIStyle boxStyle = new GUIStyle();
        public GUIStyle sliderStyle = new GUIStyle();
        public Font font;
        public Font optionsFont;
        public Texture texture;
        public Texture2D sliderBG;
        [Range(0f, 1f)]
        public float hue, saturation, value;
        [Header("Key Binding")]
        public KeyCode forward;
        public KeyCode left;
        public KeyCode right;
        public KeyCode backward;
        public KeyCode interact;
        public KeyCode jump;
        public KeyCode sprint;
        public KeyCode crouch;
        private KeyCode holdingKey;
        [Header("KeyBindsStrings")]
        string forwardText;
        string backwardText;
        string leftText;
        string rightText;
        string interactText;
        string jumpText;
        string crouchText;
        string sprintText;
        [Header("Booleans")]
        public bool showControls;
        public bool showOptions;
        public bool showVolume;
        public bool showBrightness;
        public bool showResolution;
        public bool fullScreenMode;
        public bool enableControls;
        public bool startRGB;
        public bool muteToggle;
        [Header("Text")]
        private string currRes;
        [Header("Resolution")]
        public int[] scrH;
        public int[] scrW;
        [Header("Sliders")]
        public float volumeSlider, holdingVolume;
        public float brightnessSlider;
        [Header("Audio & Light")]
        public AudioSource music;
        public Light dirLight;

        private Vector2 scrollPositon = Vector2.zero;
        private float sW;
        private float sH;
        string path;
        INIParser ini = new INIParser();
        private IniFile inip;

        void Start()
        {
            path = Application.dataPath;
            Debug.Log(path + "/Scripts/GUIAssesment.ini");
            ini.Open(path + "/Scripts/GUIAssesment.ini");
            //KeyBinds Strings
            forwardText = ini.ReadValue("Key Binds", "inputForward", "inputForward");
            backwardText = ini.ReadValue("Key Binds", "inputBackward", "inputBackward");
            leftText = ini.ReadValue("Key Binds", "inputLeft", "inputLeft");
            rightText = ini.ReadValue("Key Binds", "inputRight", "inputRight");
            interactText = ini.ReadValue("Key Binds", "inputInteract", "inputInteract");
            jumpText = ini.ReadValue("Key Binds", "inputJump", "inputJump");
            crouchText = ini.ReadValue("Key Binds", "inputCrouch", "inputCrouch");
            sprintText = ini.ReadValue("Key Binds", "inputSprint", "inputSprint");
            //Slider Values
            string volumeValue = ini.ReadValue("Sliders", "bVolume", "bVolume");
            string brightnessValue = ini.ReadValue("Sliders", "bBrightness", "bBrightness");
            //Resolution Values
            string resXValue = ini.ReadValue("Resolution", "bScreenX", "bScreenX");
            string resYValue = ini.ReadValue("Resolution", "bScreenY", "bScreenY");
            //Bool Values
            string RGBBool = ini.ReadValue("Booleans", "bRGBMode", "bRGBMode");
            //Conversion KeyBinds
            forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), forwardText);
            backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), backwardText);
            left = (KeyCode)System.Enum.Parse(typeof(KeyCode), leftText);
            right = (KeyCode)System.Enum.Parse(typeof(KeyCode), rightText);
            interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), interactText);
            jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), jumpText);
            crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), crouchText);
            sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), sprintText);
            //Conversion Sliders
            volumeSlider = float.Parse(volumeValue);
            brightnessSlider = float.Parse(brightnessValue);
            //Conversion Resolution
            sW = int.Parse(resXValue);
            sH = int.Parse(resYValue);
            //Bool convert
            startRGB = Convert.ToBoolean(RGBBool);
            ini.Close();

            dirLight = GameObject.FindGameObjectWithTag("MainLight").GetComponent<Light>();
            music = GameObject.Find("MainMusic").GetComponent<AudioSource>();
            volumeSlider = music.volume;
            brightnessSlider = dirLight.intensity;
        }


        void Update()
        {

            sW = Screen.width / 16;
            sH = Screen.height / 9;
            if (startRGB)
            {
                hue += 0.001f;
                saturation = 1;
                value = 1;
            }
            else
            {
                startRGB = false;
                saturation = 0;
                value = 0;
            }
            if (hue >= 1)
            {
                hue = 0;

            }

            if (showResolution)
            {
                currRes = "";
            }
            else
            {
                currRes = Screen.width + "x" + Screen.height;
            }

            if (music != null)
            {
                if (muteToggle == false)
                {
                    if (music.volume != volumeSlider)
                    {
                        holdingVolume = volumeSlider;
                        music.volume = volumeSlider;
                    }
                }
                else
                {
                    volumeSlider = 0;
                    music.volume = 0;
                }
            }
            if (dirLight != null)
            {
                if (brightnessSlider != dirLight.intensity)
                {
                    dirLight.intensity = brightnessSlider;
                }
            }



        }

        public void WriteFile()
        {

            ini.Open(path + "/Scripts/GUIAssesment.ini");
            ini.WriteValue("Sliders", "bVolume", volumeSlider);
            ini.WriteValue("Sliders", "bBrightness", brightnessSlider);
            ini.WriteValue("Key Binds", "inputForward", forward.ToString());
            ini.WriteValue("Key Binds", "inputBackward", backward.ToString());
            ini.WriteValue("Key Binds", "inputLeft", left.ToString());
            ini.WriteValue("Key Binds", "inputRight", right.ToString());
            ini.WriteValue("Key Binds", "inputInteract", interact.ToString());
            ini.WriteValue("Key Binds", "inputJump", jump.ToString());
            ini.WriteValue("Key Binds", "inputCrouch", crouch.ToString());
            ini.WriteValue("Key Binds", "inputSprint", sprint.ToString());
            ini.WriteValue("Resolution", "bScreenX", sW);
            ini.WriteValue("Resolution", "bScreenY", sH);
            ini.WriteValue("Booleans", "bRGBMode", startRGB);
            ini.Close();
        }
        public void Load()
        {

        }

        void OnGUI()
        {
            Event e = Event.current;

            #region GUI Styling
            // Title Label Styling
            titleStyler.fontSize = (Screen.width / 16) + (Screen.height / 9);
            titleStyler.normal.textColor = Color.black;
            titleStyler.font = font;
            // Play Button Styling
            playStyler.fontSize = ((Screen.width / 24) + (Screen.height / 11));
            playStyler.font = font;
            playStyler.normal.textColor = Color.HSVToRGB(hue, saturation, value);
            playStyler.hover.textColor = Color.white;
            playStyler.active.textColor = Color.black;

            // Options and Exit Button Styling
            styler.fontSize = ((Screen.width / 32) + (Screen.height / 18));
            styler.font = font;
            styler.normal.textColor = Color.HSVToRGB(hue, saturation, value);
            styler.hover.textColor = Color.white;
            styler.active.textColor = Color.black;

            //Options Title Styling
            optionsStyler.fontSize = ((Screen.width / 24) + (Screen.height / 11));
            optionsStyler.font = font;
            optionsStyler.normal.textColor = Color.white;

            //Options Buttons Styling
            optionsMenuStyler.fontSize = ((Screen.width / 32) + (Screen.height / 18));
            optionsMenuStyler.font = font;
            optionsMenuStyler.normal.textColor = Color.HSVToRGB(hue, saturation, value);
            optionsMenuStyler.hover.textColor = Color.white;
            optionsMenuStyler.active.textColor = Color.red;

            //Resolution Buttons Styling
            resolutionStyler.fontSize = ((Screen.width / 48) + (Screen.height / 36));
            resolutionStyler.font = font;
            resolutionStyler.normal.textColor = Color.HSVToRGB(hue, saturation, value);
            resolutionStyler.hover.textColor = Color.white;
            resolutionStyler.active.textColor = Color.red;

            //Slider Styling
            sliderStyle.active.background = sliderBG;
            #endregion

            if (!showOptions)
            {
                GUI.Label(new Rect(0.25f * sW, 3f * sH, 0, 0), "Slasher Game", titleStyler);//titleStyler

                if (GUI.Button(new Rect(0.25f * sW, 5f * sH, 2.7f * sW, 1.6f * sH), "Play", playStyler))//playStyler
                {
                    print("Loading Game...");
                    SceneManager.LoadSceneAsync(1);
                }
                if (GUI.Button(new Rect(0.25f * sW, 6.7f * sH, 3.2f * sW, 1f * sH), "Options", styler))//styler
                {
                    print("Options");
                    showOptions = true;
                }
                if (GUI.Button(new Rect(0.25f * sW, 7.8f * sH, 1.75f * sW, 0.75f * sH), "Exit", styler))//styler
                {
                    print("Exiting Game");
                    Application.Quit();
                }
                //Debug.Log("Key: " + e.keyCode);
            }
            if (showOptions)
            {
                int spacer = 0;
                GUI.Box(new Rect(0, 0, 11 * sW, 10 * sH), "");
                enableControls = !enableControls;
                if (enableControls)
                {
                    GUI.Label(new Rect(0.25f * sW, 1f * sH, 0, 0), "Options", optionsStyler);
                    if (GUI.Button(new Rect(sW, (3.5f * sH) + (spacer * sW), 5f * sW, sH), "Volume        > " + (volumeSlider * 100).ToString("F0"), optionsMenuStyler))//playStyler
                    {
                        showVolume = !showVolume;
                        showBrightness = false;
                        showResolution = false;
                        showControls = false;
                    }
                    if (showVolume)
                    {
                        showBrightness = false;
                        GUI.Box(new Rect(8f * sW, (2.75f * sH) + (spacer * sW), 0.5f * sW, 2 * sH), "", sliderStyle);
                        volumeSlider = GUI.VerticalSlider(new Rect(8.17f * sW, (2.75f * sH) + (spacer * sW), 2 * sW, 2 * sH), volumeSlider, 1, 0);
                    }
                    spacer++;
                    if (GUI.Button(new Rect(sW, (3.5f * sH) + (spacer * sW), 5f * sW, sH), "Brightness > " + (brightnessSlider * 100).ToString("F0"), optionsMenuStyler))//styler
                    {
                        showBrightness = !showBrightness;
                        showVolume = false;
                        showResolution = false;
                        showControls = false;
                    }
                    if (showBrightness)
                    {
                        showVolume = false;
                        GUI.Box(new Rect(8f * sW, (2.75f * sH) + (spacer * sW), 0.5f * sW, 2 * sH), "", sliderStyle);
                        brightnessSlider = GUI.VerticalSlider(new Rect(8.17f * sW, (2.75f * sH) + (spacer * sW), 2 * sW, 2 * sH), brightnessSlider, 1, 0);
                    }
                    spacer++;
                    if (GUI.Button(new Rect(sW, (3.5f * sH) + (spacer * sW), 5f * sW, sH), "Resoultion  > " + currRes, optionsMenuStyler))//styler
                    {
                        print("Resolution");
                        showResolution = !showResolution;
                        showBrightness = false;
                        showVolume = false;
                        showControls = false;
                    }
                    if (showResolution)
                    {
                        float resSpace = 0;
                        int screenArray = 0;
                        scrollPositon = GUI.BeginScrollView(new Rect(6.25f * sW, (3.5f * sH) + (spacer * sW), 3.75f * sW, 2 * sH), scrollPositon, new Rect(sW, sH, 3 * sW, 4f * sH));
                        for (int i = 0; i < 8; i++)
                        {
                            resSpace += 0.5f;
                            if (GUI.Button(new Rect(sW, (0.5f * sH) + (resSpace * sW), 3.25f * sW, 0.5f * sH), " " + scrW[screenArray] + " x " + scrH[screenArray], resolutionStyler))
                            {
                                Screen.SetResolution(scrW[screenArray], scrH[screenArray], fullScreenMode);
                            }
                            screenArray++;
                        }
                        GUI.EndScrollView();
                        
                    }

                    spacer++;
                    if (GUI.Button(new Rect(sW, (3.5f * sH) + (spacer * sW), 3.5f * sW, sH), "Controls", optionsMenuStyler))
                    {
                        showControls = !showControls;
                        showBrightness = false;
                        enableControls = false;
                        showResolution = false;
                        showVolume = false;
                    }
                    spacer++;
                    if (GUI.Button(new Rect(sW, (3.5f * sH) + (spacer * sW), 2.25f * sW, sH), "Back", optionsMenuStyler))//styler
                    {
                        print("Going Back to main Menu");
                        showOptions = false;
                        enableControls = false;
                    }
                    if (GUI.Button(new Rect(4 * sW, (3.5f * sH) + (spacer * sW), 2.25f * sW, sH), "Apply", optionsMenuStyler))
                    {
                        WriteFile();
                    }
                    if (GUI.Button(new Rect(7 * sW, (3.5f * sH) + (spacer * sW), 2.25f * sW, sH), "Mute", optionsMenuStyler))
                    {
                        ToggleVol();
                    }
                }

                if (showControls)
                {

                    // Debug.Log(e.keyCode);
                    int controlSpacer = 0;
                    GUI.Box(new Rect(0, 0, 11 * sW, 10 * sH), "");
                    GUI.Label(new Rect(0.25f * sW, 1f * sH, 0, 0), "Controls", optionsStyler);
                    scrollPositon = GUI.BeginScrollView(new Rect(sW, (3f * sH), 11 * sW, 4.5f * sH), scrollPositon, new Rect(sW, sH, 5 * sW, 8 * sH));
                    if (GUI.Button(new Rect(sW, (sH) + (controlSpacer * sW), 4f * sW, sH), "Forward >      " + forward.ToString(), optionsMenuStyler))//styler
                    {
                        if (!(backward == KeyCode.None || right == KeyCode.None || left == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
                        {
                            holdingKey = forward;
                            forward = KeyCode.None;
                            forwardText = forward.ToString();
                        }
                        if (forward == KeyCode.None)
                        {
                            if (e.isKey)
                            {
                                Debug.Log("Key: " + e.keyCode);
                                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                                {
                                    forward = e.keyCode;
                                    holdingKey = KeyCode.None;
                                    forwardText = forward.ToString();
                                }
                                else
                                {
                                    forward = holdingKey;
                                    holdingKey = KeyCode.None;
                                    forwardText = forward.ToString();
                                }

                            }
                        }
                    }
                    controlSpacer++;
                    if (GUI.Button(new Rect(sW, (sH) + (controlSpacer * sW), 2.25f * sW, sH), "Backward >   " + backward.ToString(), optionsMenuStyler))//styler
                    {
                        if (!(forward == KeyCode.None || right == KeyCode.None || left == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
                        {
                            holdingKey = backward;
                            backward = KeyCode.None;
                            if (backward == KeyCode.None)
                            {
                                if (e.isKey)
                                {
                                    Debug.Log("Key: " + e.keyCode);
                                    if (!(e.keyCode == forward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                                    {
                                        backward = e.keyCode;
                                        holdingKey = KeyCode.None;
                                        backwardText = backward.ToString();
                                    }
                                    else
                                    {
                                        backward = holdingKey;
                                        holdingKey = KeyCode.None;
                                        backwardText = backward.ToString();
                                    }

                                }
                            }
                        }

                    }
                    controlSpacer++;
                    if (GUI.Button(new Rect(sW, (sH) + (controlSpacer * sW), 2.25f * sW, sH), "Left >             " + left.ToString(), optionsMenuStyler))//styler
                    {
                        if (!(backward == KeyCode.None || right == KeyCode.None || forward == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
                        {
                            holdingKey = left;
                            left = KeyCode.None;
                        }
                        if (left == KeyCode.None)
                        {
                            if (e.isKey)
                            {
                                Debug.Log("Key: " + e.keyCode);
                                if (!(e.keyCode == backward || e.keyCode == forward || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                                {
                                    left = e.keyCode;
                                    holdingKey = KeyCode.None;
                                    leftText = left.ToString();
                                }
                                else
                                {
                                    left = holdingKey;
                                    holdingKey = KeyCode.None;
                                    leftText = left.ToString();
                                }
                            }
                        }
                    }
                    controlSpacer++;
                    if (GUI.Button(new Rect(sW, (sH) + (controlSpacer * sW), 2.25f * sW, sH), "Right >          " + right.ToString(), optionsMenuStyler))//styler
                    {
                        if (!(backward == KeyCode.None || forward == KeyCode.None || left == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
                        {
                            holdingKey = right;
                            right = KeyCode.None;
                        }
                        if (right == KeyCode.None)
                        {
                            if (e.isKey)
                            {
                                Debug.Log("Key: " + e.keyCode);
                                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == forward || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                                {
                                    right = e.keyCode;
                                    holdingKey = KeyCode.None;
                                    rightText = right.ToString();
                                }
                                else
                                {
                                    right = holdingKey;
                                    holdingKey = KeyCode.None;
                                    rightText = right.ToString();
                                }

                            }
                        }
                    }
                    controlSpacer++;
                    if (GUI.Button(new Rect(sW, (sH) + (controlSpacer * sW), 2.25f * sW, sH), "Jump >           " + jump.ToString(), optionsMenuStyler))//styler
                    {
                        if (!(backward == KeyCode.None || right == KeyCode.None || left == KeyCode.None || crouch == KeyCode.None || forward == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
                        {
                            holdingKey = jump;
                            jump = KeyCode.None;
                        }
                        if (jump == KeyCode.None)
                        {
                            if (e.isKey)
                            {
                                Debug.Log("Key: " + e.keyCode);
                                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == forward || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                                {
                                    jump = e.keyCode;
                                    holdingKey = KeyCode.None;
                                    jumpText = jump.ToString();
                                }
                                else
                                {
                                    jump = holdingKey;
                                    holdingKey = KeyCode.None;
                                    jumpText = jump.ToString();
                                }

                            }
                        }
                    }
                    controlSpacer++;
                    if (GUI.Button(new Rect(sW, (sH) + (controlSpacer * sW), 2.25f * sW, sH), "Sprint >        " + sprint.ToString(), optionsMenuStyler))//styler
                    {
                        if (!(backward == KeyCode.None || right == KeyCode.None || left == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || forward == KeyCode.None || interact == KeyCode.None))
                        {
                            holdingKey = sprint;
                            sprint = KeyCode.None;
                        }
                        if (sprint == KeyCode.None)
                        {
                            if (e.isKey)
                            {
                                Debug.Log("Key: " + e.keyCode);
                                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == forward || e.keyCode == interact))
                                {
                                    sprint = e.keyCode;
                                    holdingKey = KeyCode.None;
                                    sprintText = sprint.ToString();
                                }
                                else
                                {
                                    sprint = holdingKey;
                                    holdingKey = KeyCode.None;
                                    sprintText = sprint.ToString();
                                }
                            }
                        }
                    }
                    controlSpacer++;
                    if (GUI.Button(new Rect(sW, (sH) + (controlSpacer * sW), 2.25f * sW, sH), "Crouch >       " + crouch.ToString(), optionsMenuStyler))//styler
                    {
                        if (!(backward == KeyCode.None || right == KeyCode.None || left == KeyCode.None || forward == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
                        {
                            holdingKey = crouch;
                            crouch = KeyCode.None;
                        }
                        if (crouch == KeyCode.None)
                        {
                            if (e.isKey)
                            {
                                Debug.Log("Key: " + e.keyCode);
                                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == forward || e.keyCode == sprint || e.keyCode == interact))
                                {
                                    crouch = e.keyCode;
                                    holdingKey = KeyCode.None;
                                    crouchText = crouch.ToString();
                                }
                                else
                                {
                                    crouch = holdingKey;
                                    holdingKey = KeyCode.None;
                                    crouchText = crouch.ToString();
                                }
                            }
                        }
                    }
                    controlSpacer++;
                    if (GUI.Button(new Rect(sW, (sH) + (controlSpacer * sW), 2.25f * sW, sH), "Interact >     " + interact.ToString(), optionsMenuStyler))//styler
                    {
                        if (!(backward == KeyCode.None || right == KeyCode.None || left == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || forward == KeyCode.None))
                        {
                            holdingKey = interact;
                            interact = KeyCode.None;
                        }
                        if (interact == KeyCode.None)
                        {
                            if (e.isKey)
                            {
                                Debug.Log("Key: " + e.keyCode);
                                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == forward))
                                {
                                    interact = e.keyCode;
                                    holdingKey = KeyCode.None;
                                    interactText = interact.ToString();
                                }
                                else
                                {
                                    interact = holdingKey;
                                    holdingKey = KeyCode.None;
                                    interactText = interact.ToString();
                                }

                            }
                        }
                    }
                    GUI.EndScrollView();

                    if (GUI.Button(new Rect(sW, (sH) + (controlSpacer * sW), 2.25f * sW, sH), "Back >       ", optionsMenuStyler))//styler
                    {
                        print("Going Back to main Menu");
                        showControls = !showControls;
                        enableControls = true;
                        showOptions = true;
                    }
                }
            }
        }
        bool ToggleVol()
        {
            if (muteToggle == true)
            {
                muteToggle = false;
                volumeSlider = holdingVolume;
                return false;
            }
            else
            {
                muteToggle = true;
                holdingVolume = volumeSlider;
                volumeSlider = 0;
                music.volume = 0;
                return true;
            }
        }

    }
}