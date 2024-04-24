using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// ShapeMatchingGoal
    /// </summary>
    public  class ShapeMatchingGoal
    {
        private ShapeMatchingGoal(){}

        /// <summary>
        /// Move a set of nodes to positions that resemble a target shape.
        /// The target shape is defined by a sequence of points, in the same order as how the nodes are specified.
        /// </summary>
        /// <param name="startPositions"></param>
        /// <param name="targetShapePoints"></param>
        /// <param name="allowScaling"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.ShapeMatchingGoal Create(
            List<Point> startPositions,
            [DefaultArgument("null")] List<Point> targetShapePoints,
            [DefaultArgument("false")] bool allowScaling,
            [DefaultArgument("1.0")] float weight)
        {
            return targetShapePoints == null
                ? new DynaShape.Goals.ShapeMatchingGoal(startPositions.ToTriples(), allowScaling, weight)
                : new DynaShape.Goals.ShapeMatchingGoal(startPositions.ToTriples(), targetShapePoints.ToTriples(), allowScaling,
                    weight);
        }


        /// <summary>
        /// Adjust the goal's parameters while the solver is running.
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="targetShapePoints"></param>
        /// <param name="allowScaling"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.ShapeMatchingGoal Change(
            DynaShape.Goals.ShapeMatchingGoal goal,
            [DefaultArgument("null")] List<Point> targetShapePoints,
            [DefaultArgument("false")] bool? allowScaling,
            [DefaultArgument("-1.0")] float weight)
        {
            if (targetShapePoints != null) goal.SetTargetShapePoints(targetShapePoints.ToTriples());
            if (weight >= 0.0) goal.Weight = weight;
            if (allowScaling.HasValue) goal.AllowScaling = allowScaling.Value;
            return goal;
        }
    }
}
