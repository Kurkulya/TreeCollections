using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeCollections
{
    public class BsTreeAVL : ITreeBalanced
    {
        protected Node root = null;

        public void Init(int[] ini)
            {
                if (ini == null)
                    return;

                Clear();
                for (int i = 0; i < ini.Length; i++)
                {
                    Add(ini[i]);
                }
            }

        private void SetHeight(Node node)
        {
            int lHeight = (node.left == null) ? 0 : node.left.height;
            int rHeight = (node.right == null) ? 0 : node.right.height;
            node.height = 1 + Math.Max(lHeight, rHeight);
        }

        #region NodeImpl
            protected class Node
            {
                public int val;
                public Node left;
                public Node right;
                public int height;

                public Node(int val)
                {
                    this.val = val;
                }
        }
        #endregion

        #region Rotating
        Node RightRotate(Node node)
        {
            Node left = node.left;
            node.left = left.right;
            left.right = node;
            SetHeight(node);
            SetHeight(left);
            return left;
        }
        Node LeftRotate(Node node)
        {
            Node right = node.right;
            Node temp = right.left;

            node.right = temp;
            right.left = node;

            SetHeight(node);
            SetHeight(right);
            return right;
        }

        private Node Balance(Node node)
        {
            SetHeight(node);
            if (GetBalance(node) > 1)
            {
                if (GetBalance(node.right) < 0)
                    node.right = RightRotate(node.right);
                return LeftRotate(node);
            }
            else if (GetBalance(node) < -1)
            {
                if (GetBalance(node.left) > 0)
                    node.left = LeftRotate(node.left);
                return RightRotate(node);
            }
            return node;
        }

        int GetBalance(Node node)
        {
            if (node == null)
                return 0;

            int lHeight = (node.left == null) ? 0 : node.left.height;
            int rHeight = (node.right == null) ? 0 : node.right.height;

            return rHeight - lHeight;
        }
        #endregion

        #region Adding
        public void Add(int val)
        {
            if (root == null)
            {
                root = new Node(val);
            }
            else
            {
                root = InsertNode(root, val);
            }
        }
        private Node InsertNode(Node node, int val)
        {
            if (node == null)
                return new Node(val);

            if (val < node.val)
                node.left = InsertNode(node.left, val);
            else
                node.right = InsertNode(node.right, val);

            return Balance(node);
        }
        #endregion

        #region Delete
        public void Del(int val)
        {
            if (root == null)
                throw new EmptyTreeEx();
            if (FindNode(root, val) == null)
                throw new ValueNotFoundEx();

            if (Size() == 1)
                root = null;
            else
                DeleteNode(root, val);
        }
        private Node FindNode(Node node, int val)
        {
            if (node == null || val == node.val)
                return node;
            if (val < node.val)
                return FindNode(node.left, val);
            else
                return FindNode(node.right, val);
        }
        private Node GetMin(Node node)
        {
            if (node.left == null)
                return node;

            return GetMin(node.left);
        }
        Node DeleteNode(Node root, int val)
        {
            if (root == null)
                return root;

            if (val < root.val)
            {
                root.left = DeleteNode(root.left, val);
            }
            else if (val > root.val)
            {
                root.right = DeleteNode(root.right, val);
            }
            else
            {
                if ((root.left == null) || (root.right == null))
                {
                    Node temp = null;

                    if (temp == root.left)
                        temp = root.right;
                    else
                        temp = root.left;

                    if (temp == null)
                    {
                        temp = root;
                        root = null;
                    }
                    else
                    {
                        root = temp;
                    }
                }
                else
                {
                    Node temp = GetMin(root.right);
                    root.val = temp.val;
                    root.right = DeleteNode(root.right, temp.val);
                }
            }

            if (root == null)
                return root;

            return Balance(root);
        }        
        #endregion

        #region Height
        public int Height()
        {
            return GetHeight(root);
        }

        private int GetHeight(Node node)
        {
            if (node == null)
                return 0;

            return Math.Max(GetHeight(node.left), GetHeight(node.right)) + 1;
        }
        #endregion

        #region Width
        public int Width()
        {
            if (root == null)
                return 0;

            int[] ret = new int[Height()];
            GetWidth(root, ret, 0);
            return ret.Max();
        }
        private void GetWidth(Node node, int[] levels, int level)
        {
            if (node == null)
                return;

            GetWidth(node.left, levels, level + 1);
            levels[level]++;
            GetWidth(node.right, levels, level + 1);
        }
        #endregion

        #region Leaves
        public int Leaves()
        {
            return GetLeaves(root);
        }
        private int GetLeaves(Node node)
        {
            if (node == null)
                return 0;

            int leaves = 0;
            leaves += GetLeaves(node.left);
            if (node.left == null && node.right == null)
                leaves++;
            leaves += GetLeaves(node.right);
            return leaves;
        }
        #endregion

        #region Nodes
        public int Nodes()
        {
            return GetNodes(root);
        }
        private int GetNodes(Node node)
        {
            if (node == null)
                return 0;

            int nodes = 0;
            nodes += GetNodes(node.left);
            if (node.left != null || node.right != null)
                nodes++;
            nodes += GetNodes(node.right);
            return nodes;
        }
        #endregion

        #region Reverse
        public void Reverse()
        {
            SwapSides(root);
        }
        private void SwapSides(Node node)
        {
            if (node == null)
                return;

            SwapSides(node.left);
            Node temp = node.right;
            node.right = node.left;
            node.left = temp;
            SwapSides(node.left);
        }
        #endregion

        #region Size
        public int Size()
        {
            return GetSize(root);
        }
        private int GetSize(Node node)
        {
            if (node == null)
                return 0;

            int count = 0;
            count += GetSize(node.left);
            count++;
            count += GetSize(node.right);
            return count;
        }
        #endregion

        #region ToArray
        public int[] ToArray()
        {
            if (root == null)
                return new int[] { };

            int[] ret = new int[Size()];
            int i = 0;
            NodeToArray(root, ret, ref i);
            return ret;


        }
        private void NodeToArray(Node node, int[] ini, ref int n)
        {
            if (node == null)
                return;

            NodeToArray(node.left, ini, ref n);
            ini[n++] = node.val;
            NodeToArray(node.right, ini, ref n);

        }
        #endregion

        #region ToString
        public override String ToString()
        {
            return NodeToString(root).TrimEnd(new char[] { ',', ' ' });
        }

        private String NodeToString(Node node)
        {
            if (node == null)
                return "";

            String str = "";
            str += NodeToString(node.left);
            str += node.val + ", ";
            str += NodeToString(node.right);
            return str;
        }

        public void Clear()
        {
            root = null;
        }
        #endregion

        #region Equal

        public bool Equal(ITree tree)
        {
            return CompareNodes(root, (tree as BsTreeAVL).root);
        }

        private bool CompareNodes(Node curTree, Node tree)
        {
            if (curTree == null && tree == null)
                return true;
            if (curTree == null || tree == null)
                return false;

            bool equal = false;
            equal = CompareNodes(curTree.left, tree.left);
            equal = equal & (curTree.val == tree.val);
            equal = CompareNodes(curTree.right, tree.right);
            return equal;
        }
        #endregion

        #region Balance
        public bool IsBalanced()
        {
            return Balanced(root);
        }
        private bool Balanced(Node node)
        {
            int lHeight;
            int rHeight; 

            if (node == null)
                return true;

            lHeight = (node.left == null) ? 0 : node.left.height;
            rHeight = (node.right == null) ? 0 : node.right.height;

            return Math.Abs(lHeight - rHeight) <= 1
                    & Balanced(node.left)
                    & Balanced(node.right);

        }
        #endregion
    }
}
