namespace DeveBlockStacker.Core.Helpers
{
    public static class ArrayHelper
    {
        public static int Width(this bool[,] array)
        {
            return array.GetLength(0);
        }

        public static int Height(this bool[,] array)
        {
            return array.GetLength(1);
        }
    }
}
