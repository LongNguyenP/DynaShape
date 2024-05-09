using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// SphereStaticLineCollisionGoal
    /// </summary>
    public  class SphereStaticLineCollisionGoal
    {
        private SphereStaticLineCollisionGoal(){}

        /// <summary>
        /// Create a SphereStaticLineCollisionGoal to move a set of nodes to positions that resemble a target shape.
        /// The target shape is defined by a sequence of points, provided in the desired order.
        /// </summary>
        /// <param name="centers">The centers of the spheres.</param>
        /// <param name="radii">The radii of the spheres.</param>
        /// <param name="lines">The lines to extract nodes from to move to positions that resemble a target shape.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="SphereStaticLineCollisionGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.SphereStaticLineCollisionGoal Create(
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
        /// Modifies the SphereStaticLineCollisionGoal's parameters while the solver is running.
        /// </summary>
        /// <param name="sphereStaticLineCollisionGoal">The SphereStaticLineCollisionGoal to modify.</param>
        /// <param name="radii">Optional new radii for the SphereStaticLineCollisionGoal.</param>
        /// <param name="lines">Optional new lines for the SphereStaticLineCollisionGoal.</param>
        /// <param name="weight">An optional new weight for the SphereStaticLineCollisionGoal.</param>
        /// <returns name="SphereStaticLineCollisionGoal"></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.SphereStaticLineCollisionGoal Change(
            DynaShape.Goals.SphereStaticLineCollisionGoal sphereStaticLineCollisionGoal,
            [DefaultArgument("null")] List<float> radii,
            [DefaultArgument("null")] List<Line> lines,
            [DefaultArgument("-1.0")] float weight)
        {
            if (radii != null)
            {
                if (sphereStaticLineCollisionGoal.NodeCount != radii.Count)
                    throw new Exception("Error: radii count is not equal to node count");
                sphereStaticLineCollisionGoal.Radii = radii.ToArray();
            }

            if (lines != null)
            {
                sphereStaticLineCollisionGoal.LineStarts = new List<Triple>(lines.Count);
                sphereStaticLineCollisionGoal.LineEnds = new List<Triple>(lines.Count);

                for (int i = 0; i < lines.Count; i++)
                {
                    sphereStaticLineCollisionGoal.LineStarts.Add(lines[i].StartPoint.ToTriple());
                    sphereStaticLineCollisionGoal.LineStarts.Add(lines[i].EndPoint.ToTriple());
                }
            }

            if (weight >= 0.0) sphereStaticLineCollisionGoal.Weight = weight;
            return sphereStaticLineCollisionGoal;
        }
    }
}
