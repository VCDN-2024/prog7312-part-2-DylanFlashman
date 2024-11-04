using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityApp.MVVM.Model
{
    /*
     * Geeks for Geeks
     * Introduction to Min-Heap data structures
     * https://www.geeksforgeeks.org/introduction-to-min-heap-data-structure/
     * [Accessed 02/11/2024]
     */
    public class MinHeap
    {
        private List<ServiceRequest> heap;

        public MinHeap()
        {
            heap = new List<ServiceRequest>();
        }
        public MinHeap(MinHeap other)
        {
            heap = new List<ServiceRequest>(other.heap);
        }
        public bool IsEmpty()
        {
            return heap.Count == 0;
        }

        public void Insert(ServiceRequest request)
        {
            heap.Add(request);
            int currentIndex = heap.Count - 1;

            while (currentIndex > 0)
            {
                int parentIndex = (currentIndex - 1) / 2;
                if (heap[currentIndex].Priority >= heap[parentIndex].Priority)
                    break;

                (heap[currentIndex], heap[parentIndex]) = (heap[parentIndex], heap[currentIndex]);
                currentIndex = parentIndex;
            }
        }

        public ServiceRequest ExtractMin()
        {
            if (heap.Count == 0) return null;

            var minRequest = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            int currentIndex = 0;
            while (currentIndex < heap.Count)
            {
                int leftChildIndex = 2 * currentIndex + 1;
                int rightChildIndex = 2 * currentIndex + 2;

                if (leftChildIndex >= heap.Count) break;

                int smallerChildIndex = leftChildIndex;
                if (rightChildIndex < heap.Count && heap[rightChildIndex].Priority < heap[leftChildIndex].Priority)
                    smallerChildIndex = rightChildIndex;

                if (heap[currentIndex].Priority <= heap[smallerChildIndex].Priority)
                    break;

                (heap[currentIndex], heap[smallerChildIndex]) = (heap[smallerChildIndex], heap[currentIndex]);
                currentIndex = smallerChildIndex;
            }

            return minRequest;
        }

        public List<ServiceRequest> PeekAll()
        {
            return new List<ServiceRequest>(heap);
        }
    }
}
