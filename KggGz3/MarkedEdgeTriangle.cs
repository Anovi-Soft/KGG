namespace KggGz3
{
    public class MarkedEdgeTriangle: Triangle
    {
        public bool IsMarked { get; set; }

        public MarkedEdgeTriangle(Triangle triangle)
            :base(triangle.A, triangle.B, triangle.C)
        {
        }
        

    }
}