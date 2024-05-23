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
        /// <param name="startPositions">The start positions of the nodes.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="EqualLengthsGoal"></returns>
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

            return new DynaShape.Goals.EqualLengthsGoal(triples, weight);
        }


        /// <summary>
        /// Creates an EqualLengthsGoal to force a set of line segments to maintain equal lengths.
        /// </summary>
        /// <param name="startPositions">The start positions of the nodes.</param>
        /// <param name="endPositions">The end positions of the nodes.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
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

            return new DynaShape.Goals.EqualLengthsGoal(triples, weight);
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
            return new DynaShape.Goals.EqualLengthsGoal(startPositions, weight);
        }


        /// <summary>
        /// Modifies the EqualLengthsGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="equalLengthsGoal">The equalLengthsGoal to modify.</param>
        /// <param name="weight">An optional new weight for the EqualLengthsGoal.</param>
        /// <returns name="EqualLengthsGoal"></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.EqualLengthsGoal Change(
            DynaShape.Goals.EqualLengthsGoal equalLengthsGoal,
            [DefaultArgument("-1.0")] float weight)
        {
            if (weight >= 1.0) equalLengthsGoal.Weight = weight;
            return equalLengthsGoal;
        }
    }
}
