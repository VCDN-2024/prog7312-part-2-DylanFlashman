using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityApp.MVVM.Model
{
    /*
     * Geeks for Geeks
     * Insertion for AVL Tree
     * https://www.geeksforgeeks.org/insertion-in-an-avl-tree/
     * [Accessed 04/11/2024]
     */

    public class AVLNode
    {
        public ServiceRequest Request;
        public AVLNode Left;
        public AVLNode Right;
        public int Height;

        public AVLNode(ServiceRequest request)
        {
            Request = request;
            Height = 1;
        }
    }
    public class AVLTree
    {
        public AVLNode Root;

        private int Height(AVLNode node) => node?.Height ?? 0;

        private int GetBalance(AVLNode node) => node == null ? 0 : Height(node.Left) - Height(node.Right);

        private AVLNode RotateRight(AVLNode y)
        {
            var x = y.Left;
            var T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x;
        }

        private AVLNode RotateLeft(AVLNode x)
        {
            var y = x.Right;
            var T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y;
        }

        public void Insert(ServiceRequest request)
        {
            Root = InsertRec(Root, request);
        }

        private AVLNode InsertRec(AVLNode node, ServiceRequest request)
        {
            if (node == null)
                return new AVLNode(request);

            if (request.DateSubmitted < node.Request.DateSubmitted)
                node.Left = InsertRec(node.Left, request);
            else if (request.DateSubmitted > node.Request.DateSubmitted)
                node.Right = InsertRec(node.Right, request);
            else
                return node;

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            int balance = GetBalance(node);

            if (balance > 1 && request.DateSubmitted < node.Left.Request.DateSubmitted)
                return RotateRight(node);

            if (balance < -1 && request.DateSubmitted > node.Right.Request.DateSubmitted)
                return RotateLeft(node);

            if (balance > 1 && request.DateSubmitted > node.Left.Request.DateSubmitted)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            if (balance < -1 && request.DateSubmitted < node.Right.Request.DateSubmitted)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

    }
}
