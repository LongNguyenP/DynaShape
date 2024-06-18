using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// OnLineGoal
    /// </summary>
    public  class OnLineGoal
    {
        private OnLineGoal(){}


        /// <summary>
        /// Creates an OnLineGoal to force a set of nodes to lie on the specified line.
        /// This differs from the CoLinear goal, where the target line is computed based on the current node positions rather than being defined and fixed in advance.
        /// </summary>
        /// <param name="startPosition">The start position of the node.</param>
        /// <param name="targetLineOrigin">The target line's origin.</param>
        /// <param name="targetLineDirection">The target line's direction.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="OnLineGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.OnLineGoal ByStartPositionOriginDirection(
            List<Point> startPositions,
            [DefaultArgument("Point.Origin()")] Point targetLineOrigin,
            [DefaultArgument("Vector.XAxis()")] Vector targetLineDirection,
            [DefaultArgument("1.0")] float weight)
        {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.OnLineGoal>();
            goal.Initialize(
                startPositions.ToTriples(),
                targetLineOrigin.ToTriple(),
                targetLineDirection.ToTriple().Normalise(),
                weight);
            return goal;
        }


        /// <summary>
        /// Creates an OnLineGoal to force a set of nodes to lie on the specified line.
        /// This differs from the CoLinear goal, where the target line is computed based on the current node positions rather than being defined and fixed in advance.
        /// </summary>
        /// <param name="startPositions">The start positions of the nodes.</param>
        /// <param name="targetLine">The line to attempt to force the set of nodes to lie on.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="OnLineGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.OnLineGoal ByStartPositionsLine(
            List<Point> startPositions,
            [DefaultArgument("Line.ByStartPointEndPoint(Point.Origin(), Point.ByCoordinates(1.0, 0.0, 0.0))")] Line
                targetLine,
            [DefaultArgument("1.0")] float weight)
        {
            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.OnLineGoal>();

            Triple targetLineOrigin = targetLine.StartPoint.ToTriple();
            Triple targetLineDirection = (targetLine.EndPoint.ToTriple() - targetLineOrigin).Normalise();

            goal.Initialize(
                startPositions.ToTriples(),
                targetLineOrigin,
                targetLineDirection,
                weight);

            return goal;
        }
    }
}
