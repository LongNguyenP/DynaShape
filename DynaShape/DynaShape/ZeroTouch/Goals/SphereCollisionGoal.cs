using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using Dynamo.Graph.Nodes;

namespace DynaShape.ZeroTouch.Goals
{
    /// <summary>
    /// SphereCollisionGoal
    /// </summary>
    public  class SphereCollisionGoal
    {
        private SphereCollisionGoal(){}

        /// <summary>
        /// Creates a SphereCollisionGoal that maintains a minimum distance between the nodes.
        /// </summary>
        /// <param name="centers">The centers of the nodes.</param>
        /// <param name="radii">The radii of the spheres.</param>
        /// <param name="weight">The goal's weight/impact on the solver.</param>
        /// <returns name="SphereCollisionGoal"></returns>
        [NodeCategory("Create")]
        public static DynaShape.Goals.SphereCollisionGoal Create(
            List<Point> centers,
            List<float> radii,
            [DefaultArgument("1.0")] float weight)
        {
            if (centers.Count != radii.Count)
                throw new Exception("Error: centers count is not equal to radii count");

            return new DynaShape.Goals.SphereCollisionGoal(centers.ToTriples(), radii, weight);
        }


        /// <summary>
        /// Modifies the SphereCollisionGoal.
        /// </summary>
        /// <param name="sphereCollisionGoal">The SphereCollisionGoal to modify.</param>
        /// <param name="radii">Optional new radii for the given SphereCollisionGoal.</param>
        /// <param name="weight">An optional new weight for the SphereCollisionGoal.</param>
        /// <returns name="SphereCollisionGoal"></returns>
        [NodeCategory("Actions")]
        public static DynaShape.Goals.SphereCollisionGoal Change(
            DynaShape.Goals.SphereCollisionGoal sphereCollisionGoal,
            [DefaultArgument("null")] List<float> radii,
            [DefaultArgument("-1.0")] float weight)
        {
            if (radii != null)
            {
                if (sphereCollisionGoal.NodeCount != radii.Count)
                    throw new Exception("Error: radii count is not equal to node count");
                sphereCollisionGoal.Radii = radii.ToArray();
            }

            if (weight >= 0.0) sphereCollisionGoal.Weight = weight;
            return sphereCollisionGoal;
        }
    }
}
