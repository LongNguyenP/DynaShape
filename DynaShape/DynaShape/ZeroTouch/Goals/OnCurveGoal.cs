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
        public static DynaShape.Goals.OnCurveGoal Create(
            List<Point> startPositions,
            Curve targetCurve,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.OnCurveGoal(startPositions.ToTriples(), targetCurve, weight);
        }


        /// <summary>
        /// Modifies the OnCurveGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="onCurveGoal">The OnCurveGoal to modify.</param>
        /// <param name="targetCurve">An optional new target curve for the OnCurveGoal.</param>
        /// <param name="weight">An optional new weight for the OnCurveGoal.</param>
        /// <returns name="OnCurveGoal"></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.OnCurveGoal Change(
            DynaShape.Goals.OnCurveGoal onCurveGoal,
            [DefaultArgument("null")] Curve targetCurve,
            float weight)
        {
            if (targetCurve != null) onCurveGoal.TargetCurve = targetCurve;
            if (weight >= 0.0) onCurveGoal.Weight = weight;
            return onCurveGoal;
        }
    }
}
