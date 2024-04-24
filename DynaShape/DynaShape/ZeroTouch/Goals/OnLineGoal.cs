using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// OnLineGoal
    /// </summary>
    public  class OnLineGoal
    {
        private OnLineGoal(){}

        /// <summary>
        /// Force a set of nodes to lie on the specified line.
        /// This is different from other CoLinear goal, where the target line is computed based on the current node positions rather than being defined and fixed in advance.
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="targetLineOrigin"></param>
        /// <param name="targetLineDirection"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.OnLineGoal OnLineGoal_Create(
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
        /// Force a set of nodes to lie on the specified line.
        /// This is different from other CoLinear goal, where the target line is computed based on the current node positions rather than being defined and fixed in advance.
        /// </summary>
        /// <param name="startPositions"></param>
        /// <param name="targetLine"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.OnLineGoal OnLineGoal_Create(
            List<Point> startPositions,
            [DefaultArgument("Line.ByStartPointEndPoint(Point.Origin(), Point.ByCoordinates(1.0, 0.0, 0.0))")] Line
                targetLine,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.OnLineGoal(startPositions.ToTriples(), targetLine, weight);
        }


        /// <summary>
        /// Adjust the goal's parameters while the solver is running.
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="targetLineOrigin"></param>
        /// <param name="targetLineDirection"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.OnLineGoal OnLineGoal_Change(
            DynaShape.Goals.OnLineGoal goal,
            [DefaultArgument("null")] Point targetLineOrigin,
            [DefaultArgument("null")] Vector targetLineDirection,
            [DefaultArgument("-1.0")] float weight)
        {
            if (targetLineOrigin != null) goal.TargetLineOrigin = targetLineOrigin.ToTriple();
            if (targetLineDirection != null) goal.TargetLineDirection = targetLineDirection.ToTriple();
            if (weight >= 0.0) goal.Weight = weight;
            return goal;
        }


        /// <summary>
        /// Adjust the goal's parameters while the solver is running.
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="targetLine"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.OnLineGoal OnLineGoal_Change(
            DynaShape.Goals.OnLineGoal goal,
            [DefaultArgument("null")] Line targetLine,
            [DefaultArgument("-1.0")] float weight)
        {
            if (targetLine != null)
            {
                goal.TargetLineOrigin = targetLine.StartPoint.ToTriple();
                goal.TargetLineDirection =
                    (targetLine.EndPoint.ToTriple() - targetLine.StartPoint.ToTriple()).Normalise();
            }

            if (weight >= 0.0) goal.Weight = weight;
            return goal;
        }
    }
}
