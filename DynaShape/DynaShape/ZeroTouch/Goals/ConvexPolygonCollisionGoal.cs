using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// ConvexPolygonCollisionGoal
    /// </summary>
    public  class ConvexPolygonCollisionGoal
    {
        private ConvexPolygonCollisionGoal(){}

        /// <summary>
        /// Creates a ConvexPolygonCollisionGoal to simulate clashes between polygons that are convex in nature.
        /// </summary>
        /// <param name="centers">The centers of the Polygons.</param>
        /// <param name="radii">The radii of a circle circumscribed about the given Polygon.</param>
        /// <param name="polygonVertices">The vertices of the given Polygon.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="ConvexPolygonCollisionGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.ConvexPolygonCollisionGoal Create(
            List<Point> centers,
            List<float> radii,
            [DefaultArgument("null")] List<Point> polygonVertices,
            [DefaultArgument("1000.0")] float weight)
        {
            if (centers.Count != radii.Count)
                throw new Exception("Error: centers count is not equal to radii count");
            return new DynaShape.Goals.ConvexPolygonCollisionGoal(
                centers.ToTriples(),
                radii,
                polygonVertices == null ? new List<Triple>() : polygonVertices.ToTriples(),
                weight);
        }

        /// <summary>
        /// Modifies the given ConvexPolygonCollisionGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="convexPolygonCollisionGoal">A ConvexPolygonCollisionGoal to modify.</param>
        /// <param name="radii">Optional new radii for the given ConvexPolygonCollisionGoal.</param>
        /// <param name="polygonVertices">Optional new vertices for the given ConvexPolygonCollisionGoal.</param>
        /// <param name="weight">An optional new weight for the given ConvexPolygonCollisionGoal.</param>
        /// <returns name="ConvexPolygonCollisionGoal"></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.ConvexPolygonCollisionGoal Change(
            DynaShape.Goals.ConvexPolygonCollisionGoal convexPolygonCollisionGoal,
            [DefaultArgument("null")] List<float> radii,
            [DefaultArgument("null")] List<Point> polygonVertices,
            [DefaultArgument("-1.0")] float weight)
        {
            if (radii != null)
            {
                if (convexPolygonCollisionGoal.NodeCount != radii.Count)
                    throw new Exception("Error: radii count is not equal to node count");
                convexPolygonCollisionGoal.Radii = radii.ToArray();
            }

            if (polygonVertices != null) convexPolygonCollisionGoal.PolygonVertices = polygonVertices.ToTriples();
            if (weight >= 0.0) convexPolygonCollisionGoal.Weight = weight;
            return convexPolygonCollisionGoal;
        }
    }
}
