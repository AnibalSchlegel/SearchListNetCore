
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

        public Search(Node startNode, Node endNode, List<Relation> relations)
        {
            this.StartNode = startNode;
            this.StartNode.SetAsStartNode();
            this.EndNode = endNode;
            this.EndNode.SetAsEndNode();

            for (int i = 0; i < relations.Count; i++) 
            {
                if (relations[i].TargetNode.Name == startNode.Name || relations[i].SourceNode.Name == endNode.Name)
                    continue;

                if (!SearchTree.ContainsKey(relations[i].SourceNode.Name))
                    SearchTree.Add(relations[i].SourceNode.Name, new List<Relation>());

                if (!SearchTree.ContainsKey(relations[i].TargetNode.Name))
                    SearchTree.Add(relations[i].TargetNode.Name, new List<Relation>());

                SearchTree[relations[i].SourceNode.Name].Add(relations[i]);
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
            bool found = false;
            for (int j = 0; j < closedList.Count; j++)
            {
                if (EndNode.Name == closedList[j].TargetNode.Name)
                    found = true;
            }
            if (!found)
                return true;
        
            return false;
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
                        //updated = true;
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
                    //if (closedList[i].TargetNode.TotalWeight > lowestWeight.TargetNode.TotalWeight)
                    //{
                    //    closedList[i] = lowestWeight.Clone();
                    //    CutOppositeHubConnection(closedList[i]);
                    //}

                    //return;
                    if (closedList[i].TargetNode.TotalWeight <= lowestWeight.TargetNode.TotalWeight)
                        return;
                    else
                    {
                        closedList[i] = lowestWeight.Clone();
                        return;
                    }
                }
            }

            closedList.Add(lowestWeight.Clone());
            //CutOppositeHubConnection(closedList[closedList.Count - 1]);
        }

        //private void CutOppositeHubConnection(Relation relation)
        //{
        //    if (SearchTree.ContainsKey(relation.TargetNode.Name))
        //    {
        //        for (int n = 0; n < SearchTree[relation.TargetNode.Name].Count; n++)
        //        {
        //            if (SearchTree[relation.TargetNode.Name][n].TargetNode.Name == relation.SourceNode.Name)
        //            {
        //                SearchTree[relation.TargetNode.Name].RemoveAt(n);
        //                return;
        //            }
        //        }
        //    }
        //}

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

                //if (last.SourceNode.Type == NodeType.StartNode)
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
            int index = -1;

            for (int i = 0; i < closedList.Count; i++)
            {
                if (closedList[i].TargetNode.Name == EndNode.Name && closedList[i].TargetNode.TotalWeight < minWeight)
                {
                    minWeight = closedList[i].TargetNode.TotalWeight;
                    index = i;
                }
            }

            if (index == -1)
                return null;
            else
                return closedList[index];
        }

        private Relation ExtractLowestWeightedNode(List<Relation> openList)
        {
            int minWeight = int.MaxValue;
            int index = -1;

            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].TargetNode.TotalWeight < minWeight)
                {
                    minWeight = openList[i].TargetNode.TotalWeight;
                    index = i;
                }
            }

            if (index > -1)
            {
                Relation found = openList[index].Clone();
                openList.RemoveAt(index);

                return found;
            }
            else return null;
        }

        //private int GetTotalWeight(string nodeName, List<Relation> closedList)
        //{
        //    for (int i = 0; i < closedList.Count; i++)
        //    {
        //        if (closedList[i].TargetNode.Name == nodeName)
        //        {
        //            return closedList[i].TargetNode.TotalWeight;
        //        }
        //    }
        //    return 0;
        //}

        private List<Relation> GetNextOpenNodes(Node lastClosed, List<Relation> closedList)
        {
            if (lastClosed != null)
            {
                Relation temp = null;

                List<Relation> newOpens = new List<Relation>();

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

                return newOpens;
            }
            else return new List<Relation>();
        }

        #endregion
    }
}
