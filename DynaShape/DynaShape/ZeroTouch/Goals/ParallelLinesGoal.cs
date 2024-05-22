using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// ParallelLinesGoal
    /// </summary>
    public  class ParallelLinesGoal
    {
        private ParallelLinesGoal(){}

        /// <summary>
        /// Creates a ParallelLinesGoal to force a set of lines to be parallel.
        /// </summary>
        /// <param name="startPoints">The start points of the lines.</param>
        /// <param name="endPoints">The end points of the lines.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="ParallelLinesGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.ParallelLinesGoal ByStartEndPositions(
            List<Point> startPoints,
            List<Point> endPoints,
            [DefaultArgument("1.0")] float weight)
        {
            if (startPoints.Count != endPoints.Count)
                throw new Exception("Error: lineStartPoints count is not equal to lineEndPoints count");

            List<Triple> pointPairs = new List<Triple>();

            for (int i = 0; i < startPoints.Count; i++)
            {
                pointPairs.Add(startPoints[i].ToTriple());
                pointPairs.Add(endPoints[i].ToTriple());
            }

            return new DynaShape.Goals.ParallelLinesGoal(pointPairs, weight);
        }


        /// <summary>
        /// Creates a ParallelLinesGoal to force a set of lines to be parallel.
        /// </summary>
        /// <param name="lines">The lines to force to be parallel.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="ParallelLinesGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.ParallelLinesGoal ByLines(
            List<Line> lines,
            [DefaultArgument("1.0")] float weight)
        {
            List<Triple> startPositions = new List<Triple>();
            foreach (Line line in lines)
            {
                startPositions.Add(line.StartPoint.ToTriple());
                startPositions.Add(line.EndPoint.ToTriple());
            }
            return new DynaShape.Goals.ParallelLinesGoal(startPositions, weight);
        }


        /// <summary>
        /// Modifies the ParallelLinesGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="parallelLinesGoal">The ParallelLinesGoal to modify.</param>
        /// <param name="weight">An optional new weight for the ParallelLinesGoal.</param>
        /// <returns name="ParallelLinesGoal"></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.ParallelLinesGoal Change(
            DynaShape.Goals.ParallelLinesGoal parallelLinesGoal,
            [DefaultArgument("-1.0")] float weight)
        {
            if (weight >= 0.0) parallelLinesGoal.Weight = weight;
            return parallelLinesGoal;
        }
    }
}
