using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using UnityEngine.SceneManagement;

namespace AssignmentGUI
{
    public class GUIManager : MonoBehaviour
    {
        [Header("GUI Elements")]
        public GUIStyle titleStyler = new GUIStyle();
        public GUIStyle playStyler = new GUIStyle();
        public GUIStyle styler = new GUIStyle();
        public GUIStyle optionsStyler = new GUIStyle();
        public GUIStyle optionsMenuStyler = new GUIStyle();
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
        [Header("Booleans")]
        public bool showControls;
        public bool showOptions;
        public bool showVolume;
        public bool showBrightness;
        public bool showResolution;
        public bool startRGB;
        [Header("Text")]
        private string currRes;
        [Header("Resolution")]
        private int[] scrH;
        private int[] scrW;
        [Header("Sliders")]
        public float volumeSlider, holdingVolume;
        public float brightnessSlider;
        string path;
        INIParser ini = new INIParser();

        private float sW;
        private float sH;

        void Awake()
        {
            path = Application.dataPath;
            Debug.Log(path + "/Scripts/GUIAssesment.ini");

            ini.Open("C:/Users/cody.amies1/Source/Repos/GUIAssessment/Assets/Scripts");
            ini.ReadValue("Sliders", "bVolume", 1);
            ini.Close();
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

        }

        public void WriteFile()
        {

            ini.Open("C:/Users/cody.amies1/Source/Repos/GUIAssessment/Assets/Scripts");
            ini.WriteValue("Sliders", "bVolume", volumeSlider);
            ini.Close();
        }
        public void Load()
        {

        }

        void OnGUI()
        {
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
            }
            if (showOptions)
            {
                int spacer = 0;
                GUI.Box(new Rect(0, 0, 9 * sW, 10 * sH), "");
                GUI.Label(new Rect(0.25f * sW, 1f * sH, 0, 0), "Options", optionsStyler);
                if (GUI.Button(new Rect(sW, (3.5f * sH) + (spacer * sW), 2.55f * sW, sH), "Volume        > " + (volumeSlider * 100).ToString("F0"), optionsMenuStyler))//playStyler
                {
                    showVolume = !showVolume;
                }
                if (showVolume)
                {
                    showBrightness = false;
                    GUI.Box(new Rect(8f * sW, (2.75f * sH) + (spacer * sW), 0.5f * sW, 2 * sH), "", sliderStyle);
                    volumeSlider = GUI.VerticalSlider(new Rect(8.17f * sW, (2.75f * sH) + (spacer * sW), 2 * sW, 2 * sH), volumeSlider, 1, 0);
                }
                spacer++;
                if (GUI.Button(new Rect(sW, (3.5f * sH) + (spacer * sW), 4.5f * sW, sH), "Brightness > " + (brightnessSlider * 100).ToString("F0"), optionsMenuStyler))//styler
                {
                    showBrightness = !showBrightness;
                }
                if (showBrightness)
                {
                    showVolume = false;
                    GUI.Box(new Rect(8f * sW, (2.75f * sH) + (spacer * sW), 0.5f * sW, 2 * sH), "", sliderStyle);
                    brightnessSlider = GUI.VerticalSlider(new Rect(8.17f * sW, (2.75f * sH) + (spacer * sW), 2 * sW, 2 * sH), brightnessSlider, 1, 0);
                }
                spacer++;
                if (GUI.Button(new Rect(sW, (3.5f * sH) + (spacer * sW), sW, sH), "Resoultion", optionsMenuStyler))//styler
                {
                    print("Options");
                    showResolution = !showResolution;
                }
                if (showResolution)
                {
                    GUI.Button(new Rect(sW, sH, sW, sH), currRes);
                }
                spacer++;
                if (GUI.Button(new Rect(sW, (3.5f * sH) + (spacer * sW), sW, sH), "Apply", optionsMenuStyler))
                {
                    WriteFile();
                }
                spacer++;
                if (GUI.Button(new Rect(sW, (3.5f * sH) + (spacer * sW), sW, sH), "Back", optionsMenuStyler))//styler
                {
                    print("Going Back to main Menu");
                    showOptions = false;
                }

            }
        }
        #region Keybinding
        public void Forward()
        {
            //if none of the other keys are blank
            //then we can make edit this key
            if (!(backward == KeyCode.None || right == KeyCode.None || left == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
            {
                //set our holding key to the key of this button
                holdingKey = forward;
                //set this button to none allowing only this to be editable
                forward = KeyCode.None;
            }
        }
        public void Backward()
        {
            //if none of the other keys are blank
            //then we can make edit this key
            if (!(forward == KeyCode.None || right == KeyCode.None || left == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
            {
                //set our holding key to the key of this button
                holdingKey = backward;
                //set this button to none allowing only this to be editable
                backward = KeyCode.None;
            }
        }
        public void Left()
        {
            //if none of the other keys are blank
            //then we can make edit this key
            if (!(backward == KeyCode.None || right == KeyCode.None || forward == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
            {
                //set our holding key to the key of this button
                holdingKey = left;
                //set this button to none allowing only this to be editable
                left = KeyCode.None;
            }
        }
        public void Right()
        {
            //if none of the other keys are blank
            //then we can make edit this key
            if (!(backward == KeyCode.None || forward == KeyCode.None || left == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
            {
                //set our holding key to the key of this button
                holdingKey = right;
                //set this button to none allowing only this to be editable
                right = KeyCode.None;
            }
        }
        public void Jump()
        {
            //if none of the other keys are blank
            //then we can make edit this key
            if (!(backward == KeyCode.None || right == KeyCode.None || left == KeyCode.None || crouch == KeyCode.None || forward == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
            {
                //set our holding key to the key of this button
                holdingKey = jump;
                //set this button to none allowing only this to be editable
                jump = KeyCode.None;
            }
        }
        public void Crouch()
        {
            //if none of the other keys are blank
            //then we can make edit this key
            if (!(backward == KeyCode.None || right == KeyCode.None || left == KeyCode.None || forward == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
            {
                //set our holding key to the key of this button
                holdingKey = crouch;
                //set this button to none allowing only this to be editable
                crouch = KeyCode.None;
            }
        }
        public void Sprint()
        {
            //if none of the other keys are blank
            //then we can make edit this key
            if (!(backward == KeyCode.None || right == KeyCode.None || left == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || forward == KeyCode.None || interact == KeyCode.None))
            {
                //set our holding key to the key of this button
                holdingKey = sprint;
                //set this button to none allowing only this to be editable
                sprint = KeyCode.None;
            }
        }
        public void Interact()
        {
            //if none of the other keys are blank
            //then we can make edit this key
            if (!(backward == KeyCode.None || right == KeyCode.None || left == KeyCode.None || crouch == KeyCode.None || jump == KeyCode.None || sprint == KeyCode.None || forward == KeyCode.None))
            {
                //set our holding key to the key of this button
                holdingKey = interact;
                //set this button to none allowing only this to be editable
                interact = KeyCode.None;
            }
        }
        #endregion
    }
}