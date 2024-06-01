using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// LengthGoal
    /// </summary>
    public  class LengthGoal
    {
        /// <summary>
        /// Creates a LengthGoal to force a pair of nodes to maintain the specified distance/length.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="targetLength">The length to maintain. If unspecified, the length will default to the length between the two nodes.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="LengthGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.LengthGoal ByStartPointEndPoint(
            Point startPoint,
            Point endPoint,
            [DefaultArgument("-1.0")] float targetLength,
            [DefaultArgument("1.0")] float weight)
        {
            DynaShape.Goals.LengthGoal goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.LengthGoal>();

            Triple start = startPoint.ToTriple();
            Triple end = endPoint.ToTriple();

            goal.Initialize(
                start,
                end,
                targetLength >= 0.0
                    ? targetLength
                    : start.DistanceTo(end),
                weight);

            return goal;
        }


        /// <summary>
        /// Creates a LengthGoal to maintain the specified distance between two nodes located at the start and end of the given line.
        /// </summary>
        /// <param name="line">A line to extract the start and end points from.</param>
        /// <param name="targetLength">The length to maintain. If unspecified, the length will default to the length between the two nodes.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="LengthGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.LengthGoal ByLine(
            Line line,
            [DefaultArgument("-1.0")] float targetLength,
            [DefaultArgument("1.0")] float weight)
        {
            DynaShape.Goals.LengthGoal goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.LengthGoal>();

            Triple start = line.StartPoint.ToTriple();
            Triple end = line.EndPoint.ToTriple();

            goal.Initialize(
                start,
                end,
                targetLength >= 0.0
                    ? targetLength
                    : start.DistanceTo(end),
                weight);

            return goal;
        }
    }
}
