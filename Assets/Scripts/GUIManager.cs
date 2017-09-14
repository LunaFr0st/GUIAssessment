using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AssignmentGUI
{
    public class GUIManager : MonoBehaviour
    {
        public GUIStyle titleStyler = new GUIStyle();
        public GUIStyle playStyler = new GUIStyle();
        public GUIStyle styler = new GUIStyle();
        public GUILayout layout = new GUILayout();
        public Font font;
        public Texture texture;
        [Range(0f, 1f)]
        public float hue, saturation, value;


        private float sW;
        private float sH;

        void Update()
        {
            sW = Screen.width / 16;
            sH = Screen.height / 9;
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
            playStyler.normal.textColor = Color.HSVToRGB(hue,saturation,value);
            playStyler.hover.textColor = Color.white;
            playStyler.active.textColor = Color.black;

            // Options and Exit Button Styling
            styler.fontSize = ((Screen.width / 32) + (Screen.height / 18));
            styler.font = font;
            styler.normal.textColor = Color.HSVToRGB(hue, saturation, value);
            styler.hover.textColor = Color.white;
            styler.active.textColor = Color.black;
            #endregion
            //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture, ScaleMode.StretchToFill);
            //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), GUITexture.texture(texture));
            GUI.Label(new Rect(0.25f * sW, 3f * sH, 0, 0), "Slasher Game", titleStyler);//titleStyler

            if (GUI.Button(new Rect(0.25f * sW, 5f * sH, 2.7f * sW, 1.6f * sH), "Play", playStyler))//playStyler
            {
                print("Loading Game...");
                SceneManager.LoadSceneAsync(1);
            }
            if (GUI.Button(new Rect(0.25f * sW, 6.7f * sH, 3.2f * sW, 1f * sH), "Options", styler))//styler
            {
                print("Options");
            }
            if (GUI.Button(new Rect(0.25f * sW, 7.8f * sH, 1.75f * sW, 0.75f * sH), "Exit", styler))//styler
            {
                print("Exiting Game");
                Application.Quit();
            }

        }
    }
}