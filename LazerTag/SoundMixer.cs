using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag
{
    public class SoundMixer
    {
        #region singleton 
        private static SoundMixer instance;

        public static SoundMixer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SoundMixer();
                }
                return instance;
            }
        }
        #endregion

        #region fields 
        private Song menuMusic; 
        private Song gameMusic;

        private SoundEffect buttonClick;

        private SoundEffect login; 
        private SoundEffect logout; 

        private SoundEffect batteryPickUP; 
        private SoundEffect specialAmmoPickUP; 
        private SoundEffect solarUpgradePickUp;

        private SoundEffect shoot; 
        #endregion

        #region properties 
        /// <summary>
        /// Propert for getting and setting the overall volume/master
        /// </summary>
        public static float Volume { get; set; }

        public static bool MusicOn { get; set; }

        public static bool SoundEffectsOn { get; set; }
        #endregion

        /// <summary>
        /// private constructor for SoundMixer
        /// </summary>
        private SoundMixer()
        {
            Volume = 0.5f; 

            MusicOn = true;
            SoundEffectsOn = true; 
        }

        #region methods
        /// <summary>
        /// Method used for loading content
        /// </summary>
        /// <param name="content">the contentmanager we can access</param>
        public void LoadContent(ContentManager content)
        {
            // load all music 
            menuMusic = content.Load<Song>("Music\\joshua-mclean_blue-blast");
            gameMusic = content.Load<Song>("Music\\joshua-mclean_stasis");

            // load all sound effects 
            buttonClick = content.Load<SoundEffect>("Sounds\\cursor_style_2");

            login = content.Load<SoundEffect>("Sounds\\confirm_style_2_echo_001");
            logout = content.Load<SoundEffect>("Sounds\\back_style_2_echo_001");

            batteryPickUP = content.Load<SoundEffect>("Sounds\\02_Heal_02");
            specialAmmoPickUP = content.Load<SoundEffect>("Sounds\\39_Absorb_04");
            solarUpgradePickUp = content.Load<SoundEffect>("Sounds\\16_Atk_buff_04");

            shoot = content.Load<SoundEffect>("Sounds\\LASRGun_Classic Blaster A Fire_03"); 
        }

        /// <summary>
        /// method used to set the music
        /// </summary>
        public void SetMusic()
        {
            if (MusicOn)
            {
                MediaPlayer.Resume();
            }
            else
            {
                MediaPlayer.Pause();
            }
        }

        /// <summary>
        /// method used to play the music in the menu
        /// </summary>
        public void PlayMenuMusic()
        {
            if (MusicOn)
            {
                MediaPlayer.Volume = Volume; 
                MediaPlayer.Play(menuMusic);
                MediaPlayer.IsRepeating = true;
            }
        }

        /// <summary>
        /// method used to play the game in GameState
        /// </summary>
        public void PlayGameMusic()
        {
            if (MusicOn)
            {
                MediaPlayer.Volume = Volume;
                MediaPlayer.Play(gameMusic);
                MediaPlayer.IsRepeating = true;
            }
        }

        /// <summary>
        /// Method for playing a soundeffect on a buttonclick
        /// </summary>
        public void ButtonFx()
        {
            if (SoundEffectsOn)
            {
                buttonClick.Play(volume: Volume, pitch: 0.0f, pan: 0.0f);
            }
        }

        /// <summary>
        /// Method for playing a soundeffect on login
        /// </summary>
        public void LoginFx()
        {
            if (SoundEffectsOn)
            {
                login.Play(volume: Volume, pitch: 0.0f, pan: 0.0f);
            }
        }

        /// <summary>
        /// Method for playing a soundeffect on a logout
        /// </summary>
        public void LogoutFx()
        {
            if (SoundEffectsOn)
            {
                logout.Play(volume: Volume, pitch: 0.0f, pan: 0.0f);
            }
        }

        /// <summary>
        /// Method for playing a soundeffect on picking up a Battery
        /// </summary>
        public void BatteryPickUp()
        {
            if (SoundEffectsOn)
            {
                batteryPickUP.Play(volume: Volume, pitch: 0.0f, pan: 0.0f);
            }
        }

        /// <summary>
        /// Method for playing a soundeffect on picking up a SpecialAmmo
        /// </summary>
        public void SpecialAmmoPickUp()
        {
            if (SoundEffectsOn)
            {
                specialAmmoPickUP.Play(volume: Volume, pitch: 0.0f, pan: 0.0f);
            }
        }

        /// <summary>
        /// Method for playing a soundeffect on picking up a SolarUpgrade
        /// </summary>
        public void SolarUpgradePickUp()
        {
            if (SoundEffectsOn)
            {
                solarUpgradePickUp.Play(volume: Volume, pitch: 0.0f, pan: 0.0f);
            }
        }

        /// <summary>
        /// Method for playing a soundeffect on shooting
        /// </summary>
        public void ShootFx()
        {
            if (SoundEffectsOn)
            {
                shoot.Play(volume: Volume, pitch: 0.0f, pan: 0.0f);
            }
        }
        #endregion
    }
}
