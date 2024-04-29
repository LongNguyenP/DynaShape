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
        public static DynaShape.Goals.OnLineGoal Create(
            List<Point> startPosition,
            [DefaultArgument("Point.Origin()")] Point targetLineOrigin,
            [DefaultArgument("Vector.XAxis()")] Vector targetLineDirection,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.OnLineGoal(
                startPosition.ToTriples(),
                targetLineOrigin.ToTriple(),
                targetLineDirection.ToTriple().Normalise(),
                weight);
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
        public static DynaShape.Goals.OnLineGoal Create(
            List<Point> startPositions,
            [DefaultArgument("Line.ByStartPointEndPoint(Point.Origin(), Point.ByCoordinates(1.0, 0.0, 0.0))")] Line
                targetLine,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.OnLineGoal(startPositions.ToTriples(), targetLine, weight);
        }


        /// <summary>
        /// Modifies the OnLineGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="onLineGoal">The OnLineGoal to modify.</param>
        /// <param name="targetLineOrigin">An optional new origin to use.</param>
        /// <param name="targetLineDirection">An optional new direction to use.</param>
        /// <param name="weight">An optional new weight for the OnLineGoal.</param>
        /// <returns name="OnLineGoal"></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.OnLineGoal Change(
            DynaShape.Goals.OnLineGoal onLineGoal,
            [DefaultArgument("null")] Point targetLineOrigin,
            [DefaultArgument("null")] Vector targetLineDirection,
            [DefaultArgument("-1.0")] float weight)
        {
            if (targetLineOrigin != null) onLineGoal.TargetLineOrigin = targetLineOrigin.ToTriple();
            if (targetLineDirection != null) onLineGoal.TargetLineDirection = targetLineDirection.ToTriple();
            if (weight >= 0.0) onLineGoal.Weight = weight;
            return onLineGoal;
        }


        /// <summary>
        /// Modifies the OnLineGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="onLineGoal">The OnLineGoal to modify.</param>
        /// <param name="targetLine">An optional new target line to use.</param>
        /// <param name="weight">An optional new weight for the OnLineGoal.</param>
        /// <returns name="OnLineGoal"></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.OnLineGoal Change(
            DynaShape.Goals.OnLineGoal onLineGoal,
            [DefaultArgument("null")] Line targetLine,
            [DefaultArgument("-1.0")] float weight)
        {
            if (targetLine != null)
            {
                onLineGoal.TargetLineOrigin = targetLine.StartPoint.ToTriple();
                onLineGoal.TargetLineDirection =
                    (targetLine.EndPoint.ToTriple() - targetLine.StartPoint.ToTriple()).Normalise();
            }

            if (weight >= 0.0) onLineGoal.Weight = weight;
            return onLineGoal;
        }
    }
}
