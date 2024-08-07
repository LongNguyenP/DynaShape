using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// ConvexPolygonContainmentGoal
    /// </summary>
    public  class ConvexPolygonContainmentGoal
    {
        private ConvexPolygonContainmentGoal(){}


        /// <summary>
        /// Creates a ConvexPolygonCollisionGoal to keep a group of circles inside a convex polygon.
        /// </summary>
        /// <param name="centers">The centers of the circles.</param>
        /// <param name="radii">The radii of the circles.</param>
        /// <param name="polygonVertices">The vertices of the convex polygon.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        [NodeCategory("Create")]
        public static DynaShape.Goals.ConvexPolygonContainmentGoal ByPointsRadii(
            List<Point> centers,
            List<float> radii,
            [DefaultArgument("null")] List<Point> polygonVertices,
            [DefaultArgument("1000.0")] float weight)
        {
            if (centers.Count != radii.Count)
                throw new Exception("Error: centers count is not equal to radii count");

            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.ConvexPolygonContainmentGoal>();

            goal.Initialize(
                centers.ToTriples(),
                radii,
                polygonVertices == null ? new List<Triple>() : polygonVertices.ToTriples(),
                weight);

            return goal;
        }
    }
}
