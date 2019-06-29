using Unity.UIWidgets.material;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public abstract class L : DefaultMaterialLocalizations
    {
        public static L of(BuildContext context)
        {
            return Localizations.of<L>(context, typeof(MaterialLocalizations));
        }

        public virtual string Title => "TETRIS";
        public virtual string Reward => "REWARD";
        public virtual string Points => "Points";
        public virtual string Clears => "Clears";
        public virtual string Level => "Level";
        public virtual string Next => "Next";
        public virtual string Sounds => "SOUNDS";
        public virtual string PauseOrResume => "PAUSE/RESUME";
        public virtual string Reset => "RESET";
        
    }
}