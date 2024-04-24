using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// EqualLengthsGoal
    /// </summary>
    public  class EqualLengthsGoal
    {
        private EqualLengthsGoal(){}

        /// <summary>
        /// Force a sequence of nodes to maintain equal distances.
        /// </summary>
        /// <param name="startPositions"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.EqualLengthsGoal EqualLengthsGoal_Create(
            List<Point> startPositions,
            [DefaultArgument("1.0")] float weight)
        {
            List<Triple> triples = new List<Triple> { startPositions[0].ToTriple() };

            for (int i = 1; i < startPositions.Count - 1; i++)
            {
                triples.Add(startPositions[i].ToTriple());
                triples.Add(triples[triples.Count - 1]);
            }

            triples.Add(startPositions[startPositions.Count - 1].ToTriple());

            return new DynaShape.Goals.EqualLengthsGoal(triples, weight);
        }


        /// <summary>
        /// Force a set of line segments to maintain equal lengths.
        /// </summary>
        /// <param name="lineStarts"></param>
        /// <param name="lineEnds"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.EqualLengthsGoal EqualLengthsGoal_Create(
            List<Point> lineStarts,
            List<Point> lineEnds,
            [DefaultArgument("1.0")] float weight)
        {
            List<Triple> triples = new List<Triple>();

            int n = lineStarts.Count < lineEnds.Count ? lineStarts.Count : lineEnds.Count;

            for (int i = 0; i < n; i++)
            {
                triples.Add(lineStarts[i].ToTriple());
                triples.Add(lineEnds[i].ToTriple());
            }

            return new DynaShape.Goals.EqualLengthsGoal(triples, weight);
        }


        /// <summary>
        /// Force a set of line segments to maintain equal lengths.
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.EqualLengthsGoal EqualLengthsGoal_Create(
            List<Line> lines,
            [DefaultArgument("1.0")] float weight)
        {
            List<Triple> startPositions = new List<Triple>();
            foreach (Line line in lines)
            {
                startPositions.Add(line.StartPoint.ToTriple());
                startPositions.Add(line.EndPoint.ToTriple());
            }
            return new DynaShape.Goals.EqualLengthsGoal(startPositions, weight);
        }


        /// <summary>
        /// Adjust the goal's parameters while the solver is running.
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.EqualLengthsGoal EqualLengthsGoal_Change(
            DynaShape.Goals.EqualLengthsGoal goal,
            [DefaultArgument("-1.0")] float weight)
        {
            if (weight >= 1.0) goal.Weight = weight;
            return goal;
        }
    }
}
