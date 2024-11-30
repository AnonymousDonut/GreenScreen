using BepInEx;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilla;

namespace GreenScreen
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        bool EnableConfigGui = false;
        bool GreenScreenDeleted = false;
        Material ShaderXColor = new Material(Shader.Find("GorillaTag/UberShader"))

        {
            color = Color.green
        };

        void Start()
        {
            /* A lot of Gorilla Tag systems will not be set up when start is called /*
			/* Put code in OnGameInitialized to avoid null references */

            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            /* Set up your mod here */
            /* Code here runs at the start and whenever your mod is enabled*/

            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup)*/

            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            GameObject Green = GameObject.CreatePrimitive(PrimitiveType.Plane);
            Green.GetComponent<Renderer>().material = ShaderXColor;
            Green.transform.rotation = Quaternion.Euler(new Vector3(90, 300.8443f, 0));
            Green.transform.localPosition = new Vector3(-30.0603f, 16.9783f, -114.2557f);
            Green.GetComponent<MeshCollider>().enabled = false;
            Green.name = "GreenScreen";
            Green.transform.localScale = new Vector3(1, 0.1f, 0.5f);

        }
        void Update()
        {
            if (Keyboard.current.tabKey.wasPressedThisFrame)
            {
                EnableConfigGui = !EnableConfigGui;

            }
            else if (Keyboard.current.tabKey.wasPressedThisFrame && !EnableConfigGui)
            {
                EnableConfigGui = false;
            }


        }
        void OnGUI()
        {
            if (EnableConfigGui = !EnableConfigGui)
            {
                GUI.Box(new Rect(10, 10, 150, 200), "GreenScreenConfigs");
                if (GUI.Button(new Rect(15, 50, 140, 40), "Green"))
                {
                    ShaderXColor.color = Color.green;
                }
                if (GUI.Button(new Rect(15, 100, 140, 40), "Blue"))
                {
                    ShaderXColor.color = Color.blue;
                }
                if (GUI.Button(new Rect(15, 150, 140, 40), "Red"))
                {
                    ShaderXColor.color = Color.red;
                }

            }
        }

        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            /* Activate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = true;
        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            /* Deactivate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = false;
        }
    }
}
    
