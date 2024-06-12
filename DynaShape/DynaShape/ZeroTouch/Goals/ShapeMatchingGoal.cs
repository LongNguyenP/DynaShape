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
        public static DynaShape.Goals.ShapeMatchingGoal ByPoints(
            List<Point> startPositions,
            [DefaultArgument("null")] List<Point> targetShapePoints,
            [DefaultArgument("false")] bool allowScaling,
            [DefaultArgument("1.0")] float weight)
        {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.ShapeMatchingGoal>();

            if (targetShapePoints == null)
            {
                goal.Initialize(
                    startPositions.ToTriples(),
                    allowScaling,
                    weight);
            }
            else
            {
                goal.Initialize(
                    startPositions.ToTriples(),
                    targetShapePoints.ToTriples(),
                    allowScaling,
                    weight);
            }

            return goal;
        }
    }
}
