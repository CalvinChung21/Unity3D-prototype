using UnityEngine;
using System.Collections;
using System;

public class Heap<T> where T : IHeapItem<T> {
	
    T[] items;
    int currentItemCount;
    // Min-heap so that the value in the root is always the smallest value
    public Heap(int maxHeapSize) { items = new T[maxHeapSize]; }
	
    public void Add(T item) {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        // sorting the heap tree again
        SortUp(item);
        currentItemCount++;
    }
    
    public T RemoveFirst() {
        T firstItem = items[0];
        
        // replace the first item with the last item in the heap
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        // sort all the child in the heap tree
        SortDown(items[0]);
        return firstItem;
    }
    
    public void UpdateItem(T item) { SortUp(item); }
    
    public int Count { get { return currentItemCount; } }

    public bool Contains(T item) { return Equals(items[item.HeapIndex], item); }
    
    void SortDown(T item) {
        // sorting the heap tree in a downward way
        while (true) {
            
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;
            
            // when the index is not out of range, keep doing the sorting process of child
            if (childIndexLeft < currentItemCount && childIndexRight < currentItemCount) {
                // see whether the child in the left or the child in the right has the lower value
                // lower value <-- higher priority in a min-heap tree
                if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0) {
                    swapIndex = childIndexRight;
                }else {
                    swapIndex = childIndexLeft;
                }
                
                // compare their f cost or h cost
                // if the lower value node is smaller than the current item node value
                // then swap them
                if (item.CompareTo(items[swapIndex]) < 0) {
                    Swap (item,items[swapIndex]);
                } // if the child has bigger value then stop the sorting process for the child
                else { return; }
                
            }
            
            else { return; }
        }
    }
	
    // sort all the parents in the heap tree
    void SortUp(T item) {
        int parentIndex = (item.HeapIndex-1)/2;
		
        while (true) {
            T parentItem = items[parentIndex];
            // if the current item has smaller value <-- higher priority
            // then swap it with the parent
            if (item.CompareTo(parentItem) > 0) {
                Swap (item,parentItem);
            }
            else {
                break;
            }

            parentIndex = (item.HeapIndex-1)/2;
        }
    }
    
	// swap items in the heap
    // and swap their heap index value
    void Swap(T itemA, T itemB) {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}

// getter and setter for the heap index
public interface IHeapItem<T> : IComparable<T> {
    int HeapIndex { get; set; }
}