using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        public Font font;
        public Font optionsFont;
        public Texture texture;
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
        public bool startRGB;
        [Header("Sliders")]
        public float volumeSlider, holdingVolume;
        public float brightnessSlider;

        private float sW;
        private float sH;

        void Awake()
        {

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
            optionsStyler.font = optionsFont;
            optionsStyler.normal.textColor = Color.black;

            //Options Buttons Styling
            optionsStyler.fontSize = ((Screen.width / 32) + (Screen.height / 18));
            optionsStyler.font = optionsFont;
            optionsStyler.normal.textColor = Color.HSVToRGB(hue, saturation, value);
            optionsStyler.hover.textColor = Color.white;
            optionsStyler.active.textColor = Color.black;

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

                GUI.Label(new Rect(0.25f * sW, 1f * sH, 0, 0), "Options", optionsStyler);
                if (GUI.Button(new Rect(sW, (3.5f * sH) + (spacer * sW), sW, sH), "Volume - " + (volumeSlider * 100).ToString("F0") + "%", optionsMenuStyler))//playStyler
                {
                    showVolume = !showVolume;
                }
                if (showVolume && showBrightness == false)
                {
                    GUI.Box(new Rect(2.5f * sW, (2.75f * sH) + (spacer * sW), 0.5f * sW, 2 * sH), "");
                    volumeSlider = GUI.VerticalSlider(new Rect(2.67f * sW, (2.75f * sH) + (spacer * sW), 2 * sW, 2 * sH), volumeSlider, 0, 1);
                }
                spacer++;
                if (GUI.Button(new Rect(sW, (3.5f * sH) + (spacer * sW), sW, sH), "Brightness", optionsMenuStyler))//styler
                {
                    print("Options");
                    showOptions = true;
                }
                spacer++;
                if (GUI.Button(new Rect(sW, (3.5f * sH) + (spacer * sW), sW, sH), "Resoultion", optionsMenuStyler))//styler
                {
                    print("Options");
                    showOptions = true;
                }
                spacer++;
                if (GUI.Button(new Rect(sW, (3.5f * sH) + (spacer * sW), sW, sH), "Back", optionsMenuStyler))//styler
                {
                    print("Going Back to main Menu");
                    showOptions = false;
                }
            }
        }
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
    }
}