using System;
using System.Collections.Generic;

namespace YouAndCthulhu
{
    public class FunctionTable
    {
        // List of Function objects
        // A FunctionTable is always sorted. It has elements, each corresponding
        // to a List<Function> containing Function of IdLetter A, B, C or D,
        // respectively contained as elements 0, 1, 2, 3 of the table. These
        // lists are sorted in increasing order of the IdNatural.
        protected List<Function>[] ftable;

        // Constructor
        public FunctionTable()
        {
            ftable = new List<Function>[4];
        }

        // Used in order to keep the table sorted at all time
        // Search idnum in l. If found, return its index, else, returns the index
        // it should be inserted at.
        private int BinarySearch(List<Function> l, ulong idnum)
        {
            int i = 0;
            int end = l.Count - 1;
            bool find = false;
            int mid = 0;

            while (!find && i <= end)
            {
                mid = (i + end) / 2;
                if (l[mid].Idnum == idnum)
                {
                    find = true;
                }
                else
                {
                    if (idnum > l[mid].Idnum) i = mid + 1;
                    else end = mid - 1;
                }
            }

            return mid;
        }

        // Adds a function at the right place in the table
        public void Add(Function f)
        {
            throw new NotImplementedException("Do it");
        }

        // Search method, returns the right function.
        public Function Search(ulong idnum, char idchar)
        {
            throw new NotImplementedException("Do it");
        }

        // Same but with a register which could be negative
        public Function SearchRegister(int register, char idchar)
        {
            throw new NotImplementedException("Do it");
        }

        // Executes the ftable (by executing 0A)
        public void Execute()
        {
            throw new NotImplementedException("Do it");
        }
    }
}