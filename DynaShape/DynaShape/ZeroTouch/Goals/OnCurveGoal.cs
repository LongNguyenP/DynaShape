using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// OnCurveGoal
    /// </summary>
    public  class OnCurveGoal
    {
        private OnCurveGoal(){}


        /// <summary>
        /// Creates an OnCurveGoal to force a set of nodes to lie on the specified curve.
        /// </summary>
        /// <param name="startPositions">The starting positions of the nodes.</param>
        /// <param name="targetCurve">The curve to try to force the nodes to lie on.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="OnCurveGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.OnCurveGoal ByPoints(
            List<Point> startPositions,
            Curve targetCurve,
            [DefaultArgument("1.0")] float weight)
        {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.OnCurveGoal>();
            goal.Initialize(startPositions.ToTriples(), targetCurve, weight);
            return goal;
        }
    }
}
