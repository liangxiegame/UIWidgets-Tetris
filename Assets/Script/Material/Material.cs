using Unity.UIWidgets.foundation;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Image = Unity.UIWidgets.ui.Image;

namespace TerisGame
{
    public class GameMaterial : StatefulWidget
    {
        public Widget Child;

        public GameMaterial(Widget child)
        {
            Child = child;
        }

        public override State createState()
        {
            return new GameMaterialState();
        }

        public static Image getMaterial(BuildContext context)
        {
            var state = context.ancestorStateOfType(new TypeMatcher<GameMaterialState>()) as GameMaterialState;
            D.assert(state != null, ()=>"can not find GameMaterial widget");
            return state.Material;
        }
    }

    public class GameMaterialState : State<GameMaterial>
    {
        public Image Material;

        public override void initState()
        {
            base.initState();
            DoLoadMaterial();
        }


        void DoLoadMaterial()
        {
            if (Material != null)
            {
                return;
            }

            var texture = Resources.Load<Texture2D>("material");

            var codec = new ImageCodec(new Image(texture));
            var frame = codec.getNextFrame();
            setState(() => { Material = frame.image; });
        }

        public override Widget build(BuildContext context)
        {
            return Material == null ? new Container() : widget.Child;
        }
    }
}