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
    }
    
    public class GameMaterialState : State<GameMaterial>
    {
        private Image Material;

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

//            var bytes = Resources.Load("material.png");
            
        }

        public override Widget build(BuildContext context)
        {
            return widget.Child;
        }
    }
}