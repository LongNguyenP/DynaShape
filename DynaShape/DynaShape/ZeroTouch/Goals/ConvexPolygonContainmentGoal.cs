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
        /// Creates a ConvexPolygonContainmentGoal to contain polygons within another polygon.
        /// </summary>
        /// <param name="centers"></param>
        /// <param name="radii"></param>
        /// <param name="polygonVertices"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.ConvexPolygonContainmentGoal Create(
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
