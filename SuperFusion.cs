using System;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;
using BoneLib;
using BoneLib.BoneMenu;

[assembly: MelonInfo(typeof(SuperFusion.Main), "Super Fusion", "1.0.0", "Nono")]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]

namespace SuperFusion
{
    public class Main : MelonMod
    {
        // Preferences
        public static MelonPreferences_Category PrefCategory;
        public static MelonPreferences_Entry<bool> PrefEnabled;
        public static MelonPreferences_Entry<bool> PrefSuperStrength;
        public static MelonPreferences_Entry<bool> PrefInvisible;
        public static MelonPreferences_Entry<bool> PrefAntiKick;

        // Runtime
        private static bool _enabled = false;
        private static bool _superStrength = false;
        private static bool _invisible = false;
        private static bool _antiKick = true;
        private static bool _menuOpen = false;

        // BoneMenu pages
        private static Page _mainPage;
        private static Page _ownerPage;

        // Hand menu (simple flag for now)
        private static bool _handMenuVisible = false;

        public override void OnInitializeMelon()
        {
            PrefCategory = MelonPreferences.CreateCategory("SuperFusion");
            PrefEnabled = PrefCategory.CreateEntry("Enabled", false);
            PrefSuperStrength = PrefCategory.CreateEntry("SuperStrength", false);
            PrefInvisible = PrefCategory.CreateEntry("Invisible", false);
            PrefAntiKick = PrefCategory.CreateEntry("AntiKick", true);

            _enabled = PrefEnabled.Value;
            _superStrength = PrefSuperStrength.Value;
            _invisible = PrefInvisible.Value;
            _antiKick = PrefAntiKick.Value;

            CreateBoneMenu();
            MelonLogger.Msg("Super Fusion loaded. Enable it in Preferences > Super Fusion");
        }

        public override void OnUpdate()
        {
            if (!_enabled) return;

            // Keep anti-kick and strength alive
            if (_antiKick)
            {
                // Placeholder: real anti-kick hooks would go here
            }

            if (_superStrength)
            {
                ApplySuperStrength();
            }

            if (_invisible)
            {
                ApplyInvisible();
            }
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (_enabled)
            {
                if (_superStrength) ApplySuperStrength();
                if (_invisible) ApplyInvisible();
            }
        }

        private void CreateBoneMenu()
        {
            // Main page in Preferences (like Fusion)
            _mainPage = Page.Root.CreatePage("Super Fusion", new Color(0.2f, 0.8f, 1f));

            _mainPage.CreateBool("Enable Super Fusion", Color.green, _enabled, (v) =>
            {
                _enabled = v;
                PrefEnabled.Value = v;
                PrefCategory.SaveToFile();
                MelonLogger.Msg(v ? "Super Fusion ENABLED" : "Super Fusion DISABLED");
            });

            _mainPage.CreateBool("Super Strength", Color.red, _superStrength, (v) =>
            {
                _superStrength = v;
                PrefSuperStrength.Value = v;
                PrefCategory.SaveToFile();
                if (v) ApplySuperStrength();
                else ResetStrength();
                PlayClick();
            });

            _mainPage.CreateBool("Invisible", Color.magenta, _invisible, (v) =>
            {
                _invisible = v;
                PrefInvisible.Value = v;
                PrefCategory.SaveToFile();
                if (v) ApplyInvisible();
                else ResetInvisible();
                PlayClick();
            });

            // Owner submenu
            _ownerPage = _mainPage.CreatePage("Owner", new Color(1f, 0.6f, 0.1f));

            _ownerPage.CreateFunction("Spawn Gun", Color.cyan, () =>
            {
                MelonLogger.Msg("[Owner] Spawn Gun activated (placeholder)");
                PlayClick();
            });

            _ownerPage.CreateFunction("Nimbus", Color.white, () =>
            {
                MelonLogger.Msg("[Owner] Nimbus activated (placeholder)");
                PlayClick();
            });

            _ownerPage.CreateBool("Anti-Kick / Anti-Ban", Color.green, _antiKick, (v) =>
            {
                _antiKick = v;
                PrefAntiKick.Value = v;
                PrefCategory.SaveToFile();
                MelonLogger.Msg(v ? "Anti-Kick ON" : "Anti-Kick OFF");
                PlayClick();
            });

            // Player list placeholder (real list needs Fusion player data)
            _ownerPage.CreateFunction("Refresh Player List", Color.yellow, () =>
            {
                MelonLogger.Msg("[Owner] Player list refresh (needs Fusion hooks)");
                PlayClick();
            });

            // Info
            _mainPage.CreateFunction("Info", Color.gray, () =>
            {
                MelonLogger.Msg("Super Fusion v1.0 - Hand menu + Super Strength + Invisible + Owner tools");
            });
        }

        private void ApplySuperStrength()
        {
            try
            {
                // Extreme force - much stronger than Invincible skin
                // Adjusts mass so movement stays controllable
                var player = Player.PhysicsRig;
                if (player != null)
                {
                    // Placeholder: real implementation hooks into impact/damage multipliers
                    // and rigidbody mass on hands/body
                }
            }
            catch (Exception e)
            {
                MelonLogger.Warning("SuperStrength: " + e.Message);
            }
        }

        private void ResetStrength()
        {
            // Restore normal values
        }

        private void ApplyInvisible()
        {
            try
            {
                // Hide local player mesh/avatar from network view
                // Real implementation uses Fusion representation or layer tricks
            }
            catch (Exception e)
            {
                MelonLogger.Warning("Invisible: " + e.Message);
            }
        }

        private void ResetInvisible()
        {
            // Show player again
        }

        private void PlayClick()
        {
            // Simple click feedback (BoneLib has audio helpers in newer versions)
            try
            {
                // Placeholder for futuristic click sound
            }
            catch { }
        }
    }
}
