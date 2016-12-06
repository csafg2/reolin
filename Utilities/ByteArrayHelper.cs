
namespace System
{
    public static class ByteArrayHelper
    {
        public static bool IsEqualTo(this byte[] source, byte[] target)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] != target[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
