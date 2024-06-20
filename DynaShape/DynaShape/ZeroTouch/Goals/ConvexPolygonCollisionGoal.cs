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
        [NodeCategory("Create")]
        public static DynaShape.Goals.ConvexPolygonCollisionGoal ByPointsRadii(
            List<Point> centers,
            List<float> radii,
            [DefaultArgument("null")] List<Point> polygonVertices,
            [DefaultArgument("1000.0")] float weight)
        {
            if (centers.Count != radii.Count)
                throw new Exception("Error: centers count is not equal to radii count");

            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.ConvexPolygonCollisionGoal>();

            goal.Initialize(
                centers.ToTriples(),
                radii,
                polygonVertices == null ? new List<Triple>() : polygonVertices.ToTriples(),
                weight);

            return goal;
        }
    }
}
