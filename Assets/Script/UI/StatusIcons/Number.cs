using System.Collections.Generic;
using Unity.UIWidgets.widgets;

namespace TetrisApp
{
    public class Number : StatelessWidget
    {
        private int? mNumber;

        private bool mPadWithZero;

        private int mLength;

        public Number(int? number = null, int length = 5, bool padWithZero = false)
        {
            mNumber = number;
            mLength = length;
            mPadWithZero = padWithZero;
        }

        public override Widget build(BuildContext context)
        {
            var digitalStr = mNumber?.ToString() ?? "";

            if (digitalStr.Length > mLength)
            {
                digitalStr = digitalStr.Substring(digitalStr.Length - mLength);
            }

            digitalStr = digitalStr.PadLeft(mLength, mPadWithZero ? '0' : ' ');

            var children = new List<Widget>();
            
            foreach (var digitalChar in digitalStr)
            {
                var number = digitalChar.ToString();

                children.Add(
                    new Digital(
                        digital: string.IsNullOrWhiteSpace(number)
                            ? null as int?
                            : int.Parse(number)
                    )
                );
            }

            return new Row(
                children: children
            );
        }
    }
}