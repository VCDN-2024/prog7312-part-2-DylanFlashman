using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityApp.MVVM.Model
{
    /*
     * aaronjwood
     * Binary search tree implementation in c#
     * GitHub Gist
     * https://gist.github.com/aaronjwood/7e0fc962c5cd898b3181
     * [Accessed 29/10/2024]
     */
    public class BSTNode
    {
        public ServiceRequest Request;
        public BSTNode Left;
        public BSTNode Right;

        public BSTNode(ServiceRequest request)
        {
            Request = request;
        }
    }
    public class BinarySearchTree
    {
        public BSTNode Root;

        public void Insert(ServiceRequest request)
        {
            Root = InsertRec(Root, request);
        }

        private BSTNode InsertRec(BSTNode root, ServiceRequest request)
        {
            if (root == null)
            {
                root = new BSTNode(request);
                return root;
            }
            if (request.Id < root.Request.Id)
                root.Left = InsertRec(root.Left, request);
            else if (request.Id > root.Request.Id)
                root.Right = InsertRec(root.Right, request);

            return root;
        }

        public ServiceRequest Search(int id)
        {
            return SearchRec(Root, id)?.Request;
        }

        private BSTNode SearchRec(BSTNode root, int id)
        {
            if (root == null || root.Request.Id == id)
                return root;

            if (id < root.Request.Id)
                return SearchRec(root.Left, id);

            return SearchRec(root.Right, id);
        }

        public void DisplayInOrder(BSTNode node)
        {
            if (node == null) return;
            DisplayInOrder(node.Left);
            Console.WriteLine($"ID: {node.Request.Id}, Status: {node.Request.Status}");
            DisplayInOrder(node.Right);
        }
    }
}
