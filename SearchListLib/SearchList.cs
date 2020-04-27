using System.Collections.Generic;

namespace SearchList
{
    public class Search
    {
        #region Data Structures

        Node StartNode;
        Node EndNode;
        Dictionary<string, List<Relation>> SearchTree = new Dictionary<string, List<Relation>>();

        #endregion

        #region Constructor

        public Search(Node startNode, Node endNode, List<Relation> relations, bool twoWays = true)
        {
            this.StartNode = startNode;
            this.EndNode = endNode;

            for (int i = 0; i < relations.Count; i++)
            {
                BuildSearchTree(relations[i]);

                if (twoWays)
                    BuildSearchTree(relations[i].GetInverse());
            }
        }

        private void BuildSearchTree(Relation rel)
        {
            if (rel.TargetNode.Name != this.StartNode.Name && rel.SourceNode.Name != this.EndNode.Name)
            {
                if (!SearchTree.ContainsKey(rel.SourceNode.Name))
                    SearchTree.Add(rel.SourceNode.Name, new List<Relation>());

                if (!SearchTree.ContainsKey(rel.TargetNode.Name))
                    SearchTree.Add(rel.TargetNode.Name, new List<Relation>());

                SearchTree[rel.SourceNode.Name].Add(rel);
            }
        }

        #endregion

        #region Algorithm

        public List<Relation> Run()
        {
            List<Relation> openList = new List<Relation>();
            List<Relation> closedList = new List<Relation>();
            Relation lowestWeight;
            Relation temp;
            Node lastClosed = null;

            for (int j = 0; j < SearchTree[StartNode.Name].Count; j++)
            {
                temp = SearchTree[StartNode.Name][j].Clone();
                temp.TargetNode.Weight = temp.Cost;
                temp.TargetNode.TotalWeight = temp.TargetNode.Weight;
                openList.Add(temp);
            }

            while (NotFinisished(closedList))
            {
                UpdateOpenList(openList, GetNextOpenNodes(lastClosed, closedList));
                lowestWeight = ExtractLowestWeightedNode(openList);
                UpdateClosedList(closedList, lowestWeight);
                lastClosed = lowestWeight.TargetNode;
            }

            return BuildPath(closedList);
        }

        private bool NotFinisished(List<Relation> closedList)
        {
            for (int j = 0; j < closedList.Count; j++)
            {
                if (EndNode.Name == closedList[j].TargetNode.Name)
                    return false;
            }
            return true;
        }

        private void UpdateOpenList(List<Relation> openList, List<Relation> newNodes)
        {
            bool updated;

            for (int i = 0; i < newNodes.Count; i++)
            {
                updated = false;
                for (int j = openList.Count - 1; j >= 0; j--)
                {
                    if (newNodes[i].TargetNode.Name == openList[j].TargetNode.Name)
                    {
                        if (newNodes[i].TargetNode.TotalWeight < openList[j].TargetNode.TotalWeight)
                        {
                            openList[j] = newNodes[i];
                            updated = true;
                        }
                        break;
                    }
                }
                if (!updated)
                    openList.Add(newNodes[i]);
            }
        }

        private void UpdateClosedList(List<Relation> closedList, Relation lowestWeight)
        {
            for (int i = 0; i < closedList.Count; i++)
            {
                if (closedList[i].TargetNode.Name == lowestWeight.TargetNode.Name)
                {
                    if (closedList[i].TargetNode.TotalWeight > lowestWeight.TargetNode.TotalWeight)
                        closedList[i] = lowestWeight.Clone();
                    return; 
                }
            }

            closedList.Add(lowestWeight.Clone());
        }

        private List<Relation> BuildPath(List<Relation> closedList)
        {
            if (closedList.Count == 0) return new List<Relation>();

            List<Relation> finalPath = new List<Relation>();

            Relation last = FindLastNodeInPath(closedList);

            if (last == null) 
                return new List<Relation>();
            else
            {
                finalPath.Add(last);

                if (last.SourceNode.Name == StartNode.Name)
                    return finalPath;

                closedList.Remove(last);

                for (int i = closedList.Count - 1; i >= 0; i--)
                {
                    if (finalPath[finalPath.Count - 1].SourceNode.Name == closedList[i].TargetNode.Name)
                        finalPath.Add(closedList[i]);
                }

                finalPath.Reverse();

                return finalPath;
            }
        }

        private Relation FindLastNodeInPath(List<Relation> closedList)
        {
            int minWeight = int.MaxValue;
            Relation relation = null;

            for (int i = 0; i < closedList.Count; i++)
            {
                if (closedList[i].TargetNode.Name == EndNode.Name && closedList[i].TargetNode.TotalWeight < minWeight)
                {
                    minWeight = closedList[i].TargetNode.TotalWeight;
                    relation= closedList[i];
                }
            }

            return relation;
        }

        private Relation ExtractLowestWeightedNode(List<Relation> openList)
        {
            int minWeight = int.MaxValue;
            Relation relation = null;

            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].TargetNode.TotalWeight < minWeight)
                {
                    minWeight = openList[i].TargetNode.TotalWeight;
                    relation = openList[i];
                }
            }

            if (relation != null)
            {
                Relation cloned = relation.Clone();
                openList.Remove(relation);

                return cloned;
            }
            else return null;
        }

        private List<Relation> GetNextOpenNodes(Node lastClosed, List<Relation> closedList)
        {
            List<Relation> newOpens = new List<Relation>();

            if (lastClosed != null)
            {
                Relation temp;
                
                for (int i = 0; i < SearchTree[lastClosed.Name].Count; i++)
                {
                    temp = SearchTree[lastClosed.Name][i].Clone();

                    bool discard = false;

                    for (int c = 0; c < closedList.Count; c++)
                    {
                        if (closedList[c].InverseEquals(temp))
                        {
                            discard = true;
                            break;
                        }
                    }

                    if (discard)
                        continue;

                    temp.TargetNode.Weight = temp.Cost;
                    temp.TargetNode.TotalWeight = temp.TargetNode.Weight + lastClosed.TotalWeight;
                    newOpens.Add(temp);
                }
            }
            
            return newOpens;
        }

        #endregion
    }
}
