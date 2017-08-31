using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeCollections
{
    public class BsTreeV : ITree
    {

        class Node
        {
            public int val;
            public Node left;
            public Node right;
            public Node(int val)
            {
                this.val = val;
            }
        }

        Node root = null;

        #region Add
        public void Add(int val)
        {
            if (root == null)
                root = new Node(val);
            else
                AddNode(root, val);
        }
        private void AddNode(Node node, int val)
        {
            if (val < node.val)
            {
                if (node.left == null)
                    node.left = new Node(val);
                else
                    AddNode(node.left, val);
            }
            else if (val > node.val)
            {
                if (node.right == null)
                    node.right = new Node(val);
                else
                    AddNode(node.right, val);
            }
        }
        #endregion

        public void Clear()
        {
            root = null;
        }

        #region Del
        public void Del(int val)
        {
            if (root == null)
                throw new EmptyTreeEx();
            if (FindNode(root, val) == null)
                throw new ValueNotFoundEx();
            if (Size() == 1)
                root = null;

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
        private Node DeleteNode(Node node, int val)
        {
            if (node == null)
                return node;

            if (val < node.val)
            {
                node.left = DeleteNode(node.left, val);
            }
            else if (val > node.val)
            {
                node.right = DeleteNode(node.right, val);
            }
            else if (node.left != null && node.right != null)
            {
                node.val = Min(node.right).val;
                node.right = DeleteNode(node.right, node.val);
            }
            else
            {
                if (node.left != null)
                    node = node.left;
                else
                    node = node.right;
            }
            return node;
        }
        private Node Min(Node node)
        {
            if (node.left == null)
                return node;

            return Min(node.left);
        }
        #endregion

        #region Equal

        public bool Equal(ITree tree)
        {
            return CompareNodes(root, (tree as BsTreeV).root);
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

        public int Leaves()
        {
            LeavesVisitor lv = new LeavesVisitor();
            Visit(root, lv);
            return lv.leaves;
        }

        public int Nodes()
        {
            NodesVisitor nv = new NodesVisitor();
            Visit(root, nv);
            return nv.nodes;
        }

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

        public int Size()
        {
            SizeVisitor sv = new SizeVisitor();
            Visit(root, sv);
            return sv.size;
        }

        public int[] ToArray()
        {
            ArrayVisitor av = new ArrayVisitor(Size());
            Visit(root, av);
            return av.arr;
        }

        public int Width()
        {
            if (root == null)
                return 0;

            WidthVisitor v = new WidthVisitor(Height(), root);
            Visit(root, v);
            return v.ret.Max();
        }
               

        public override string ToString()
        {
            StringVisitor sv = new StringVisitor();
            Visit(root, sv);
            return sv.str.TrimEnd(new char[] { ',', ' ' });
        }

        #region Visitors
        class LeavesVisitor : IVisitor
        {
            public int leaves = 0;
            public void Action(Node node)
            {
                if (node.left == null && node.right == null)
                    leaves++;
            }
        }

        class NodesVisitor : IVisitor
        {
            public int nodes = 0;
            public void Action(Node node)
            {
                if (node.left != null || node.right != null)
                    nodes++;
            }
        }
        
        class SizeVisitor : IVisitor
        {
            public int size = 0;
            public void Action(Node node)
            {
                size++;
            }
        }

        class WidthVisitor : IVisitor
        {
            public int[] ret;
            private Node root;
            public WidthVisitor(int Height, Node root)
            {
                ret = new int[Height];
                this.root = root;
            }
            public void Action(Node node)
            {
                int currentDepth = HeightV(root, node, 0);
                ++ret[currentDepth];
            }
        }

        class ArrayVisitor : IVisitor
        {
            public int[] arr = null;
            int i = 0;
            public ArrayVisitor(int size)
            {
                arr = new int[size];
            }
            public void Action(Node node)
            {
                arr[i++] = node.val;
            }
        }
       
        class StringVisitor : IVisitor
        {
            public string str = "";
            public void Action(Node node)
            {
                str += node.val + ", ";
            }
        }
#endregion

        private interface IVisitor
        {
            void Action(Node node);
        }

        static int HeightV(Node node, Node dest, int level)
        {
            if (node == null)
                return 0;

            if (node == dest)
                return level;

            int downlevel = HeightV(node.left, dest, level + 1);
            if (downlevel != 0)
                return downlevel;
            downlevel = HeightV(node.right, dest, level + 1);
            return downlevel;
        }

        private void Visit(Node node, IVisitor v)
        {
            if (node == null)
                return;

            Visit(node.left, v);
            v.Action(node);
            Visit(node.right, v);
        }
    }
}
