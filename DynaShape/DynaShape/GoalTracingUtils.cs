using Autodesk.DesignScript.Runtime;
using DynamoServices;
using DynaShape.Goals;

namespace DynaShape;

[IsVisibleInDynamoLibrary(false)]
internal class GoalTracingUtils
{
    private const string TraceID = "{0459D869-0C72-447F-96D8-08A7FB92214B}-REVIT";
    private static int id = 0;
    private static Dictionary<Guid, Goal> traceableObjectDictionary = new Dictionary<Guid, Goal>();

    public static T GetObjectFromTrace<T>() where T : Goal, new()
    {
        T item;
        var traceId = GoalTracingUtils.GetObjectIdFromTrace();

        Guid id;
        if (traceId.Equals(Guid.Empty))
        {
            // If there's no id stored in trace for this object,
            // then grab the next unused trace id.
            id = Guid.NewGuid();

            // Create an item
            item = new T();

            // Remember to store the updated object in the trace object manager,
            // so it's available to use the next time around.
            GoalTracingUtils.RegisterTraceableObjectForId(id, item);
        }
        else
        {
            // If there's and id stored in trace, then retrieve the object stored
            // with that id from the trace object manager.
            item = (T)GoalTracingUtils.GetTracedObjectById(traceId)
                ?? new T();
        }

        return item;
    }

    private static Guid GetObjectIdFromTrace()
    {
        string id = TraceUtils.GetTraceData(TraceID);
        return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
    }

    private static Goal GetTracedObjectById(Guid id)
    {
        Goal ret;
        traceableObjectDictionary.TryGetValue(id, out ret);
        return ret;
    }

    private static void RegisterTraceableObjectForId(Guid id, Goal objectToTrace)
    {
        if (traceableObjectDictionary.ContainsKey(id))
        {
            traceableObjectDictionary[id] = objectToTrace;
        }
        else
        {
            traceableObjectDictionary.Add(id, objectToTrace);

            TraceUtils.SetTraceData(TraceID, id.ToString());
        }
    }

    private static void Clear()
    {
        traceableObjectDictionary.Clear();
        id = 0;
    }
}