using Unity.UIWidgets.widgets;

namespace TerisGame
{
    public class Spacer : StatelessWidget
    {
        private int mFlex = 1;

        public Spacer(int flex = 1)
        {
            mFlex = flex;
        }

        public override Widget build(BuildContext context)
        {
            return new Expanded(
                flex: mFlex,
                child: SizedBox.shrink()
            );
        }
    }
}