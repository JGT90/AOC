using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode {
    internal class Day12 : DayN {
        Dictionary<string, Node<string>> mNodes = new Dictionary<string, Node<string>>();
        static int PathCount;
        public override string Part1() {
            string[] lInput = System.IO.File.ReadAllLines(@"..\..\..\Inputs\Week12.txt");
            for (int i = 0; i < lInput.Length; i++) {
                string[] lSplit = lInput[i].Split('-');
                if (!mNodes.ContainsKey(lSplit[0])) mNodes.Add(lSplit[0].Trim(), new Node<string>() { Name = lSplit[0], IsLower = lSplit[0] == lSplit[0].ToLower() });
                if (!mNodes.ContainsKey(lSplit[1])) mNodes.Add(lSplit[1].Trim(), new Node<string>() { Name = lSplit[1], IsLower = lSplit[1] == lSplit[1].ToLower() });
            }
            for (int i = 0; i < lInput.Length; i++) {
                string[] lSplit = lInput[i].Split('-');
                mNodes[lSplit[0].Trim()].Neighbors.Add(mNodes[lSplit[1].Trim()]);
                mNodes[lSplit[1].Trim()].Neighbors.Add(mNodes[lSplit[0].Trim()]);
            }
            PathCount = 0;
            FindPathPart1(mNodes["start"]);
            return $"{PathCount}";
        }

        public override string Part2() {
            PathCount = 0;
            //FindPathPart2(mNodes["start"], new Dictionary<string, int>(), "");
            FindPathPart2(mNodes["start"], new Dictionary<string, int>());
            return $"{PathCount}";
        }

        private void FindPathPart1(Node<string> aNode) {
            if (aNode.Name == "end") {
                PathCount++;
                return;
            }
            if (aNode.Name == aNode.Name.ToLower()) aNode.Visited = true;
            for (int i = 0; i < aNode.Neighbors.Count; i++) {
                if (aNode.Neighbors[i].Visited) continue;
                FindPathPart1(aNode.Neighbors[i]);
            }
            aNode.Visited = false;
        }
        //private string FindPathPart2(Node<string> aNode, Dictionary<string, int> aPath, string aString) {
        //    aString += aNode.Name + ",";
        //    if (aNode.Name == "end") {
        //        PathCount++;
        //        Console.WriteLine(aString);
        //        return aString;
        //    }
        //    bool lVisitedTwice = false;
        //    if (!aPath.ContainsKey(aNode.Name)) {
        //        aPath.Add(aNode.Name, 0);
        //    } 
        //    if (aNode.Name != aNode.Name.ToUpper()) aPath[aNode.Name]++;
        //    if (aPath.Values.Any(x => x > 1)) {
        //        lVisitedTwice = true;
        //    }
        //    if ((aNode.Name == aNode.Name.ToLower() && lVisitedTwice) || aNode.Name == "start") {
        //        aNode.Visited = true;
        //    }
        //    for (int i = 0; i < aNode.Neighbors.Count; i++) {
        //        if (aNode.Neighbors[i].Visited) continue;
        //        if (aPath.ContainsKey(aNode.Neighbors[i].Name) && aPath[aNode.Neighbors[i].Name] > 0 && lVisitedTwice) {
        //            continue;
        //        }
        //        FindPathPart2(aNode.Neighbors[i], aPath, aString);
        //    }
        //    aPath[aNode.Name]--;
        //    aNode.Visited = false;
        //    //Console.WriteLine(aPath.Keys.ToString());
        //    return "";
        //}
        private void FindPathPart2(Node<string> aNode, Dictionary<string, int> aPath) {
            if (aNode.Name == "end") {
                PathCount++;
                return;
            }
            bool lVisitedTwice = false;
            if (!aPath.ContainsKey(aNode.Name)) {
                aPath.Add(aNode.Name, 0);
            }
            if (aNode.IsLower) aPath[aNode.Name]++;
            if (aPath.Values.Any(x => x > 1)) {
                lVisitedTwice = true;
            }
            if ((aNode.IsLower && lVisitedTwice) || aNode.Name == "start") {
                aNode.Visited = true;
            }
            for (int i = 0; i < aNode.Neighbors.Count; i++) {
                if (aNode.Neighbors[i].Visited) continue;
                if (aPath.ContainsKey(aNode.Neighbors[i].Name) && aPath[aNode.Neighbors[i].Name] > 0 && lVisitedTwice) {
                    continue;
                }
                FindPathPart2(aNode.Neighbors[i], aPath);
            }
            aPath[aNode.Name]--;
            aNode.Visited = false;
            return;
        }
    }

    public class Node<T> {
        public Node() {
            Neighbors = new List<Node<T>>();
        }
        public T Name { get; set; }
        public List<Node<T>> Neighbors { get; set; }
        public bool Visited { get; set; } = false;
        public bool IsLower { get; set; }
    }


















    //public class Node<T> {
    //    private T data;
    //    private NodeList<T> neighbors = null;

    //    public Node() { }
    //    public Node(T data) : this(data, null) { }
    //    public Node(T data, NodeList<T> neighbors) {
    //        this.data = data;
    //        this.neighbors = neighbors;
    //    }

    //    public T Value { get; set; }

    //    protected NodeList<T> Neighbors { get; set; }

    //}
    //public class NodeList<T> : Collection<Node<T>> {
    //    public NodeList() : base() { }

    //    public NodeList(int initialSize) {
    //        for (int i = 0; i < initialSize; i++) {
    //            base.Items.Add(default(Node<T>));
    //        }
    //    }

    //    public Node<T> FindByValue(T value) {
    //        foreach (Node<T> node in Items) {
    //            if (node.Value.Equals(value)) {
    //                return node;
    //            }
    //        }

    //        return null;
    //    }
    //}
    //public class GraphNode<T> : Node<T> {
    //    private List<int> costs;

    //    public GraphNode() : base() { }
    //    public GraphNode(T value) : base(value) { }
    //    public GraphNode(T value, NodeList<T> neighbors) : base(value, neighbors) { }

    //    new public NodeList<T> Neighbors {
    //        get {
    //            if (base.Neighbors == null)
    //                base.Neighbors = new NodeList<T>();

    //            return base.Neighbors;
    //        }
    //    }

    //    public List<int> Costs {
    //        get {
    //            if (costs == null)
    //                costs = new List<int>();

    //            return costs;
    //        }
    //    }

    //}

    //public class Graph<T> : IEnumerable<Node<T>> {
    //    private NodeList<T> nodeSet;

    //    public NodeList<T> Nodes { get => nodeSet; }

    //    public Graph() : this(null) { }
    //    public Graph(NodeList<T> nodeSet) {
    //        if (nodeSet == null) {
    //            this.nodeSet = new NodeList<T>();
    //        } else {
    //            this.nodeSet = nodeSet;
    //        }
    //    }

    //    public void AddNode(GraphNode<T> node) {
    //        if (!nodeSet.Contains(node)) nodeSet.Add(node);
    //    }

    //    public void AddNode(T value) {
    //        nodeSet.Add(new GraphNode<T>(value));
    //    }

    //    public void AddDirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost) {
    //        from.Neighbors.Add(to);
    //        from.Costs.Add(cost);
    //    }

    //    public void AddUndirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost) {
    //        AddDirectedEdge(from, to, cost); //This was duplicated so just call the existing value

    //        to.Neighbors.Add(to);
    //        to.Costs.Add(cost);
    //    }

    //    public bool contains(T value) {
    //        return nodeSet.FindByValue(value) != null;
    //    }

    //    public bool Remove(T value) {
    //        // Remove node from nodeset
    //        GraphNode<T> nodeToRemove = (GraphNode<T>)nodeSet.FindByValue(value);
    //        if (nodeToRemove == null) {
    //            // wasnt found
    //            return false;
    //        }

    //        // was found
    //        nodeSet.Remove(nodeToRemove);

    //        // enumerate through each node in nodeSet, removing edges to this node
    //        foreach (GraphNode<T> gnode in nodeSet) {
    //            int index = gnode.Neighbors.IndexOf(nodeToRemove);
    //            if (index != -1) {
    //                gnode.Neighbors.RemoveAt(index);
    //                gnode.Costs.RemoveAt(index);
    //            }
    //        }

    //        return true;
    //    }

    //    public int Count {
    //        get { return nodeSet.Count; }
    //    }

    //    public IEnumerator<Node<T>> GetEnumerator() {
    //        return nodeSet.GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator() {
    //        return GetEnumerator();
    //    }
    //}
}
