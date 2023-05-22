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
        //private string menuMusic;
        //private string gameMusic;

        private Song menuMusic; 
        private Song gameMusic;

        private SoundEffect buttonClick;

        private SoundEffect login; 
        private SoundEffect logout; 

        private SoundEffect batteryPickUP; 
        private SoundEffect specialAmmoPickUP; 
        private SoundEffect solarUpgradePickUp; 
        #endregion

        #region properties 
        private static Song Music { get; set; }
        #endregion

        private SoundMixer()
        {
            
        }

        #region methods 
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
        }

        public void PlayMenuMusic()
        {
            MediaPlayer.Play(menuMusic);
            MediaPlayer.IsRepeating = true;
        }

        public void PlayGameMusic()
        {
            MediaPlayer.Play(gameMusic);
            MediaPlayer.IsRepeating = true; 
        }

        public void ButtonFx()
        {
            buttonClick.Play();
        }

        public void LoginFx()
        {
            login.Play();
        }

        public void LogoutFx()
        {
            logout.Play();
        }

        public void BatteryPickUp()
        {
            batteryPickUP.Play();
        }

        public void SpecialAmmoPickUp()
        {
            specialAmmoPickUP.Play();
        }

        public void SolarUpgradePickUp()
        {
            solarUpgradePickUp.Play();
        }
        #endregion
    }
}
