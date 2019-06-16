using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;

namespace TetrisApp
{
    public class AppConstants
    {
        public static Color APP_BACKGROUND_COLOR    = new Color(0xffefcc19);
        public static Color SCREEN_BACKGROUND_COLOR = new Color(0xff9ead86);


        #region BRICK

        public static Color BRICK_NULL      = Colors.black12;
        public static Color BRICK_NORMAL    = Colors.black87;
        public static Color BRICK_HIGHLIGHT = new Color(0xFF560000);

        #endregion


        #region BRICKS

        public const int GAME_PAD_10X20_W = 10;
        public const int GAME_PAD_10x20_H = 20;

        #endregion


        #region JOY_STICKS

        public static Size DIRECTION_BUTTON_SIZE = Size.square(48);

        #endregion

        public const int PLAYPER_PANEL_PADDING = 6;
    }
}