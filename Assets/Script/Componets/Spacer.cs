using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public class Spacer : StatelessWidget
    {
        private readonly int mFlex;

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