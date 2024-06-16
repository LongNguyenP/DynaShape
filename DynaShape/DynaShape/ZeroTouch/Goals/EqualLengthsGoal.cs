using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// EqualLengthsGoal
    /// </summary>
    public  class EqualLengthsGoal
    {
        private EqualLengthsGoal(){}


        /// <summary>
        /// Creates an EqualLengthsGoal to force a sequence of nodes to maintain equal distances.
        /// </summary>
        /// <param name="startPositions">The starting positions of the sequence of nodes</param>
        /// <param name="weight">The goal's weight/impact on the solver</param>
        [NodeCategory("Create")]
        public static DynaShape.Goals.EqualLengthsGoal ByStartPositions(
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

            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.EqualLengthsGoal>();
            goal.Initialize(triples, weight);

            return goal;
        }


        /// <summary>
        /// Creates an EqualLengthsGoal to force a set of line segments to maintain equal lengths.
        /// </summary>
        /// <param name="startPositions">The starts of the line segments</param>
        /// <param name="endPositions">The ends of the line segments</param>
        /// <param name="weight">The goal's weight/impact on the solver</param>
        /// <returns name="EqualLengthsGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.EqualLengthsGoal ByStartEndPositions(
            List<Point> startPositions,
            List<Point> endPositions,
            [DefaultArgument("1.0")] float weight)
        {
            List<Triple> triples = new List<Triple>();

            int n = startPositions.Count < endPositions.Count ? startPositions.Count : endPositions.Count;

            for (int i = 0; i < n; i++)
            {
                triples.Add(startPositions[i].ToTriple());
                triples.Add(endPositions[i].ToTriple());
            }

            triples.Add(startPositions[startPositions.Count - 1].ToTriple());

            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.EqualLengthsGoal>();
            goal.Initialize(triples, weight);

            return goal;
        }


        /// <summary>
        /// Creates a EqualLengthsGoal to force a set of line segments to maintain equal lengths.
        /// </summary>
        /// <param name="lines">A set of lines to apply the EqualLengthsGoal to.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="EqualLengthsGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.EqualLengthsGoal ByLines(
            List<Line> lines,
            [DefaultArgument("1.0")] float weight)
        {
            List<Triple> startPositions = new List<Triple>();
            foreach (Line line in lines)
            {
                startPositions.Add(line.StartPoint.ToTriple());
                startPositions.Add(line.EndPoint.ToTriple());
            }

            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.EqualLengthsGoal>();
            goal.Initialize(startPositions, weight);

            return goal;
        }
    }
}
