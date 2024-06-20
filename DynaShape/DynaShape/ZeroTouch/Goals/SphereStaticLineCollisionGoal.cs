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

            var goal = TracingUtils.GetObjectFromTrace<DynaShape.Goals.SphereStaticLineCollisionGoal>();
            goal.Initialize(centers.ToTriples(), radii, lineStarts, lineEnds, weight);
            return goal;
        }
    }
}
