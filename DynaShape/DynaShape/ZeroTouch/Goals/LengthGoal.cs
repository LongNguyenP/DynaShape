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
        private LengthGoal(){}

        /// <summary>
        /// Creates a LengthGoal to force a pair of nodes to maintain the specified distance/length.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="targetLength">The length to maintain. If unspecified, the length will default to the length between the two nodes.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="LengthGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.LengthGoal ByStartEnd(
            Point start,
            Point end,
            [DefaultArgument("-1.0")] float targetLength,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.LengthGoal(
                start.ToTriple(),
                end.ToTriple(),
                targetLength >= 0.0
                    ? targetLength
                    : (end.ToTriple() - start.ToTriple()).Length,
                weight);
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
            return new DynaShape.Goals.LengthGoal(
                line.StartPoint.ToTriple(),
                line.EndPoint.ToTriple(),
                targetLength >= 0.0 ? targetLength : (float)line.Length,
                weight);
        }


        /// <summary>
        /// Modify the LengthGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="lengthGoal">The LengthGoal to modify.</param>
        /// <param name="targetLength">An optional new length for the LengthGoal to maintain.</param>
        /// <param name="weight">An optional new weight for the LengthGoal.</param>
        /// <returns name="LengthGoal"></returns>
        public static DynaShape.Goals.LengthGoal Change(
            DynaShape.Goals.LengthGoal lengthGoal,
            [DefaultArgument("-1.0")] float targetLength,
            [DefaultArgument("-1.0")] float weight)
        {
            if (targetLength >= 0.0) lengthGoal.TargetLength = targetLength;
            if (weight >= 0.0) lengthGoal.Weight = weight;
            return lengthGoal;
        }
    }
}
