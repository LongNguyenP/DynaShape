using System.Collections.Generic;
using Autodesk.DesignScript.Runtime;


namespace WhatTheFlock
{
    [IsVisibleInDynamoLibrary(false)]
    public class KdTree
    {
        public int[] ids;
        public float[] xs;
        public float[] ys;
        public float[] zs;
        public int[] splits;
        public int[] nodes;


        public KdTree(float[] xs, float[] ys, float[] zs, int[] ids)
        {
            int n = xs.Length;
            if (n == 0) return;

            this.xs = xs;
            this.ys = ys;
            this.zs = zs;
            this.ids = ids;

            nodes = new int[n];
            for (int i = 0; i < n; i++) nodes[i] = i;

            splits = new int[n];

            if (n == 1) splits[0] = -1;
            else BuildRecursive(nodes, 0, n - 1);
        }


        public List<int> Search(float centerX, float centerY, float centerZ, float radius)
        {
            List<int> result = new List<int>();
            SearchRecursive(0, nodes.Length - 1, centerX, centerY, centerZ, radius, result);
            return result;
        }


        private void SearchRecursive(
            int s, int e,
            float centerX,
            float centerY,
            float centerZ,
            float radius,
            List<int> results)
        {
            int m = (s + e) / 2;
            int node = nodes[m];

            float nodeX = xs[node];
            float nodeY = ys[node];
            float nodeZ = zs[node];
            float dx = nodeX - centerX;
            float dy = nodeY - centerY;
            float dz = nodeZ - centerZ;

            if (dx * dx + dy * dy + dz * dz < radius * radius)
                results.Add(ids[node]);

            int splitter = splits[node];

            if (splitter == -1) return;

            if (splitter == 0 && centerX - radius < nodeX ||
                splitter == 1 && centerY - radius < nodeY ||
                splitter == 2 && centerZ - radius < nodeZ)
            {
                if (s <= m - 1) SearchRecursive(s, m - 1, centerX, centerY, centerZ, radius, results);
            }

            if (splitter == 0 && centerX + radius > nodeX ||
                splitter == 1 && centerY + radius > nodeY ||
                splitter == 2 && centerZ + radius > nodeZ)
            {
                if (m + 1 <= e) SearchRecursive(m + 1, e, centerX, centerY, centerZ, radius, results);
            }
        }


        private void BuildRecursive(int[] nodes, int s, int e)
        {
            // Find the largest spanning dimension _________________________________________________________

            int node = nodes[s];
            float minX = xs[node];
            float minY = ys[node];
            float minZ = zs[node];
            float maxX = xs[node];
            float maxY = ys[node];
            float maxZ = zs[node];

            for (int i = s + 1; i <= e; i++)
            {
                if (xs[nodes[i]] < minX) minX = xs[nodes[i]];
                else if (xs[nodes[i]] > maxX) maxX = xs[nodes[i]];
                if (ys[nodes[i]] < minY) minY = ys[nodes[i]];
                else if (ys[nodes[i]] > maxY) maxY = ys[nodes[i]];
                if (zs[nodes[i]] < minZ) minZ = zs[nodes[i]];
                else if (zs[nodes[i]] > maxZ) maxZ = zs[nodes[i]];
            }

            double spanX = maxX - minX;
            double spanY = maxY - minY;
            double spanZ = maxZ - minZ;

            int dominantDimension = spanX > spanY
                ? (spanY > spanZ ? 0 : (spanX > spanZ ? 0 : 2))
                : (spanX > spanZ ? 1 : (spanY > spanZ ? 1 : 2));

            // partition ________________________________________________________________

            Partition(dominantDimension == 0 ? xs : (dominantDimension == 1 ? ys : zs), nodes, s, e);

            // recursive calls______________________________________________________________

            int m = (s + e) / 2;
            splits[nodes[m]] = dominantDimension;

            if (s < m - 1) BuildRecursive(nodes, s, m - 1);
            else if (s == m - 1) splits[nodes[s]] = -1;

            if (m + 1 < e) BuildRecursive(nodes, m + 1, e);
            else if (m + 1 == e) splits[nodes[e]] = -1;
        }


        private void Partition(float[] v, int[] nodes, int s, int e)
        {
            int temp;
            int m = (s + e) / 2;

            for (int n = 0; n < e - s; n++)
            {
                float pivot = v[nodes[s]];
                int left = s;
                for (int i = s + 1; i <= e; i++)
                    if (v[nodes[i]] < pivot)
                    {
                        left++;
                        temp = nodes[i];
                        nodes[i] = nodes[left];
                        nodes[left] = temp;
                    }

                temp = nodes[s];
                nodes[s] = nodes[left];
                nodes[left] = temp;

                if (m < left) e = left - 1;
                else if (left < m) s = left + 1;
                else return;
            }
        }
    }
}


