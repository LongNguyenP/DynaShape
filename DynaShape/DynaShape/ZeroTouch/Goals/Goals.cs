﻿using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using DynaShape.Goals;
using Mesh = Autodesk.Dynamo.MeshToolkit.Mesh;

namespace DynaShape.ZeroTouch.Goals;


public static class Goals
{
    //==================================================================
    // Change the weight value of a goal (of any type)
    //==================================================================

    /// <summary>
    /// Change the weight value of a goal (of any kind)
    /// </summary>
    /// <param name="goal"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    public static Goal Goal_ChangeWeight(Goal goal, float weight)
    {
        goal.Weight = weight;
        return goal;
    }
}