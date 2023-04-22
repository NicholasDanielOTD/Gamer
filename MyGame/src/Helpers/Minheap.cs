using System;
using System.Text;

namespace MyGame.Helpers;

public class MinHeap{
    private int[] ar;
    private int count;
    public StringBuilder sb;
    public MinHeap(){
        ar = new int[150000];
        count = 0;
        sb = new StringBuilder("");
    }
    private int left(int i){
        return 2*i + 1;
    }
    private int right(int i){
        return 2*i + 2;
    }
    private int parent(int i){
        return (i-1)/2;
    }
    public void insert(int n){
        count++;
        int i = count-1;
        ar[i] = n;
        percolateUp(count-1);
    }
    private void percolateUp(int i){
        if(i <= 0){
            return;
        }
        if(ar[i] < ar[parent(i)]){
            int temp = ar[i];
            ar[i] = ar[parent(i)];
            ar[parent(i)] = temp;
            percolateUp(parent(i));
        }
    }
    private void percolateDown(int i){
        int l = left(i);
        int r = right(i);
        int min = i;
        bool flag = false;
        if(l < count && ar[i] > ar[l]){
            min = l;
            flag = true;
        }
        if(r < count && ar[r] < ar[min]){
            min = r;
            flag = true;
        }
        if(flag){
            int temp = ar[i];
            ar[i] = ar[min];
            ar[min] = temp;
            percolateDown(min);
        }
    }
    public void deleteMin(){
        if(count == 0){
            return;
        }
        if(count == 1){
            count--;
            return;
        }
        ar[0] = ar[count-1];
        count--;
        percolateDown(0);
    }
}