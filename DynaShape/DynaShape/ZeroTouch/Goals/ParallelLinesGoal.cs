using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// ParallelLinesGoal
    /// </summary>
    public  class ParallelLinesGoal
    {
        private ParallelLinesGoal(){}

        /// <summary>
        /// Force a set of lines to be parallel.
        /// </summary>
        /// <param name="lineStartPoints"></param>
        /// <param name="lineEndPoints"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.ParallelLinesGoal Create(
            List<Point> lineStartPoints,
            List<Point> lineEndPoints,
            [DefaultArgument("1.0")] float weight)
        {
            if (lineStartPoints.Count != lineEndPoints.Count)
                throw new Exception("Error: lineStartPoints count is not equal to lineEndPoints count");

            List<Triple> pointPairs = new List<Triple>();

            for (int i = 0; i < lineStartPoints.Count; i++)
            {
                pointPairs.Add(lineStartPoints[i].ToTriple());
                pointPairs.Add(lineEndPoints[i].ToTriple());
            }

            return new DynaShape.Goals.ParallelLinesGoal(pointPairs, weight);
        }


        /// <summary>
        /// Force a set of lines to be parallel.
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.ParallelLinesGoal Create(
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
        /// Adjust the goal's parameters while the solver is running.
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.ParallelLinesGoal Change(
            DynaShape.Goals.ParallelLinesGoal goal,
            [DefaultArgument("-1.0")] float weight)
        {
            if (weight >= 0.0) goal.Weight = weight;
            return goal;
        }
    }
}
