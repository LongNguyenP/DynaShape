using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// ShapeMatchingGoal
    /// </summary>
    public  class ShapeMatchingGoal
    {
        private ShapeMatchingGoal(){}

        /// <summary>
        /// Creates a ShapeMatchingGoal to move a set of nodes to positions that resemble a given target shape.
        /// The target shape is defined by a sequence of points, provided in the desired order.
        /// </summary>
        /// <param name="startPositions">The start positions of the nodes.</param>
        /// <param name="targetShapePoints">The points to move the nodes to.</param>
        /// <param name="allowScaling">Allow scaling of the points to maintain the shape.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="ShapeMatchingGoal"></returns>
        [NodeCategory("Create")]
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
        /// Modifies the ShapeMatchingGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="shapeMatchingGoal">The ShapeMatchingGoal to modify.</param>
        /// <param name="targetShapePoints">An optional new list of points to move the nodes to.</param>
        /// <param name="allowScaling">Optionally allow or disallow scaling.</param>
        /// <param name="weight">An optional new weight for the ShapeMatchingGoal.</param>
        /// <returns name="ShapeMatchingGoal"></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.ShapeMatchingGoal Change(
            DynaShape.Goals.ShapeMatchingGoal shapeMatchingGoal,
            [DefaultArgument("null")] List<Point> targetShapePoints,
            [DefaultArgument("false")] bool? allowScaling,
            [DefaultArgument("-1.0")] float weight)
        {
            if (targetShapePoints != null) shapeMatchingGoal.SetTargetShapePoints(targetShapePoints.ToTriples());
            if (weight >= 0.0) shapeMatchingGoal.Weight = weight;
            if (allowScaling.HasValue) shapeMatchingGoal.AllowScaling = allowScaling.Value;
            return shapeMatchingGoal;
        }
    }
}
