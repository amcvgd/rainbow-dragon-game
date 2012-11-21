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

        //dragon parts
        public const string DRAGON_HEAD = "dragon_head";
        public const string DRAGON_BODY = "dragon_body";
        public const string DRAGON_TAIL = "dragon_tail";

        //rainbow
        public const string RAINBOW_PART = "rainbow_part";

        //particles
        public const string BLUE_PARTICLE = "blue_particle";
        public const string GREEN_PARTICLE = "green_particle";
        public const string RED_PARTICLE = "red_particle";

        //default paths for textures
        public const string NORMAL_ARROW_PATH = "Enemies\\normal_arrow";
        public const string SLOW_ARROW_PATH = "Enemies\\slow_arrow";
        public const string INVERSE_ARROW_PATH = "Enemies\\inverse_arrow";
        public const string POISON_ARROW_PATH = "Enemies\\poison_arrow";
        public const string NORMAL_SUN_PATH = "Pickups\\normal_sun";
        public const string ACCELERATING_SUN_PATH = "Pickups\\accelerating_sun";
        public const string INVINCIBLE_SUN_PATH = "Pickups\\invincible_sun";
        public const string DRAGON_HEAD_PATH = "Player\\dragon_head";
        public const string DRAGON_BODY_PATH = "Player\\dragon_body";
        public const string DRAGON_TAIL_PATH = "Player\\dragon_tail";
        public const string RAINBOW_PART_PATH = "Player\\rainbow_part";
        public const string BACKGROUND_BASE_PATH = "Levels\\";
        public const string PARTICLE_BASE_PATH = "Particles\\";

        //effects
        public const string CHARGE_FIELD = "charge_field";
        public const string RAINBOW_CIRCLE = "rainbow_circle";
        public const string EFFECT_BASE_PATH = "Effects\\";

    }
}
