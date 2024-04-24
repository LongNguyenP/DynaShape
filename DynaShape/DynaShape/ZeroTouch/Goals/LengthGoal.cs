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
        /// Force a pair of nodes to maintain the specified distance/length
        /// </summary>
        /// <param name="startPosition1"></param>
        /// <param name="startPosition2"></param>
        /// <param name="targetLength">If unspecified, the target length will be the distance between the starting node positions</param>
        /// <param name="weight"></param>
        /// <returns></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.LengthGoal Create(
            Point startPosition1,
            Point startPosition2,
            [DefaultArgument("-1.0")] float targetLength,
            [DefaultArgument("1.0")] float weight)
        {
            return new DynaShape.Goals.LengthGoal(
                startPosition1.ToTriple(),
                startPosition2.ToTriple(),
                targetLength >= 0.0
                    ? targetLength
                    : (startPosition2.ToTriple() - startPosition1.ToTriple()).Length,
                weight);
        }


        /// <summary>
        /// Maintain the specified distance between two nodes located at the start and end of the given line
        /// </summary>
        /// <param name="line">This line will be used to create two nodes located at is start and end point</param>
        /// <param name="targetLength">If unspecified, the target length will be the distance between the starting node positions</param>
        /// <param name="weight"></param>
        /// <returns></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.LengthGoal Create(
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
        /// Adjust the goal's parameters while the solver is running.
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="targetLength"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.LengthGoal Change(
            DynaShape.Goals.LengthGoal goal,
            [DefaultArgument("-1.0")] float targetLength,
            [DefaultArgument("-1.0")] float weight)
        {
            if (targetLength >= 0.0) goal.TargetLength = targetLength;
            if (weight >= 0.0) goal.Weight = weight;
            return goal;
        }
    }
}
