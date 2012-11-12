using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RainbowDragon.HelperClasses
{
    class Constants
    {
        public const int GAME_STATE_START = 0;

        public const int GAME_STATE_INGAME = 1;

        public const int GAME_STATE_OVER = 2;

        //Directions
        public const int LEFT = 0;
        public const int RIGHT = 1;
        public const int UP = 2;
        public const int DOWN = 3;
        public const int NO_DIRECTION = 4;

        //arrow types
        public const string NORMAL_ARROW = "normal_arrow";
        public const string SLOW_ARROW = "slow_arrow";
        public const string INVERSE_ARROW = "inverse_arrow";
        public const string POISON_ARROW = "poison_arrow";

        //sun types
        public const string NORMAL_SUN = "normal_sun";
        public const string ACCELERATING_SUN = "accelerating_sun";
        public const string INVINCIBLE_SUN = "invincible_sun";

        //default paths for textures
        public const string NORMAL_ARROW_PATH = "Enemies\\normal_arrow";
        public const string SLOW_ARROW_PATH = "Enemies\\slow_arrow";
        public const string INVERSE_ARROW_PATH = "Enemies\\inverse_arrow";
        public const string POISON_ARROW_PATH = "Enemies\\poison_arrow";
        public const string NORMAL_SUN_PATH = "Pickups\\normal_sun";
        public const string ACCELERATING_SUN_PATH = "Pickups\\accelerating_sun";
        public const string INVINCIBLE_SUN_PATH = "Pickups\\invincible_sun";


    }
}
