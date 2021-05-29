namespace CryptoAlgorithms.Core.LWE_Problem
{
    public class VectorIntPair
    {
        public int[] A { get; private set; }
        public int B { get; private set; }

        public VectorIntPair(int[] a, int b)
        {
            A = a;
            B = b;
        }

        public override string ToString()
        {
            var a = "{" + string.Join(", ", A) + "}";
            return $"(a = {a}, b = {B})";
        }
    }
}
