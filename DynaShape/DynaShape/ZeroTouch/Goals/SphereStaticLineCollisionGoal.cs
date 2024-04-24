using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// SphereStaticLineCollisionGoal
    /// </summary>
    public  class SphereStaticLineCollisionGoal
    {
        private SphereStaticLineCollisionGoal(){}

        /// <summary>
        /// Move a set of nodes to positions that resemble a target shape.
        /// The target shape is defined by a sequence of points, in the same order as how the nodes are specified.
        /// </summary>
        /// <param name="centers"></param>
        /// <param name="radii"></param>
        /// <param name="lines"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.SphereStaticLineCollisionGoal SphereStaticLineCollisionGoal_Create(
            List<Point> centers,
            List<float> radii,
            [DefaultArgument("null")] List<Line> lines,
            [DefaultArgument("1.0")] float weight)
        {
            if (centers.Count != radii.Count)
                throw new Exception("Error: centers count is not equal to radii count");

            List<Triple> lineStarts = new List<Triple>();
            List<Triple> lineEnds = new List<Triple>();

            if (lines != null)
                foreach (Line line in lines)
                {
                    lineStarts.Add(line.StartPoint.ToTriple());
                    lineEnds.Add(line.EndPoint.ToTriple());
                }

            return new DynaShape.Goals.SphereStaticLineCollisionGoal(centers.ToTriples(), radii, lineStarts, lineEnds, weight);
        }


        /// <summary>
        /// Adjust the goal's parameters while the solver is running.
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="radii"></param>
        /// <param name="lines"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static DynaShape.Goals.SphereStaticLineCollisionGoal SphereStaticLineCollisionGoal_Change(
            DynaShape.Goals.SphereStaticLineCollisionGoal goal,
            [DefaultArgument("null")] List<float> radii,
            [DefaultArgument("null")] List<Line> lines,
            [DefaultArgument("-1.0")] float weight)
        {
            if (radii != null)
            {
                if (goal.NodeCount != radii.Count)
                    throw new Exception("Error: radii count is not equal to node count");
                goal.Radii = radii.ToArray();
            }

            if (lines != null)
            {
                goal.LineStarts = new List<Triple>(lines.Count);
                goal.LineEnds = new List<Triple>(lines.Count);

                for (int i = 0; i < lines.Count; i++)
                {
                    goal.LineStarts.Add(lines[i].StartPoint.ToTriple());
                    goal.LineStarts.Add(lines[i].EndPoint.ToTriple());
                }
            }

            if (weight >= 0.0) goal.Weight = weight;
            return goal;
        }
    }
}
