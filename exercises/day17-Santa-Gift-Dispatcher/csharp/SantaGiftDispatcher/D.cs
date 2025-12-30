using System;
using System.Collections;
using System.Collections.Generic;

// Decompiled / patched-up version used for the exercise.
// I vaguely recall this being the main coordinator class, but the name never got cleaned up.
public class D
{
    // some dictionary of counts? probably keyed by string, but leaving it loose for now.
    private IDictionary s;
    // collection of "entries" that get processed later (requests? rows?).
    private IList r = new ArrayList();

    // this seems to be given at startup with initial data (stock? mapping?).
    public D(IDictionary m)
    {
        s = new Hashtable();
        if (m != null)
        {
            foreach (DictionaryEntry entry in m)
            {
                s[entry.Key] = entry.Value; // shallow copy is probably enough here
            }
        }
    }

    // stores one item identified by x with a list y (favorite things? options?).
    public void A(string x, IList y)
    {
        if (x == null || y == null) return;
        var cp = new ArrayList();
        foreach (var item in y)
        {
            cp.Add(item); // keep our own list, just in case y changes later
        }
        r.Add(new E(x, cp));
    }

    // walks over r and somehow uses s to create result rows. z = some kind of upper limit.
    public IList B(int z)
    {
        var outList = new ArrayList();
        if (z <= 0) return outList;

        for (int i = 0; i < r.Count; i++)
        {
            if (!(r[i] is E e)) continue; // shouldn't happen but defensive check stayed in
            int c = z;
            while (c > 0)
            {
                object picked = F(e);
                if (picked == null) break; // nothing left that makes sense here
                outList.Add(new G(e.N, picked.ToString()));
                c--;
            }
        }
        return outList;
    }

    // tries to pick exactly one value from s that fits with the given E.
    private object F(E e)
    {
        if (e == null || e.W == null) return null;

        // first attempt: run through e.W in order and see if s contains any of those
        for (int i = 0; i < e.W.Count; i++)
        {
            var wish = e.W[i];
            if (wish == null) continue;
            var cntObj = s[wish];
            if (cntObj is int cnt && cnt > 0)
            {
                s[wish] = cnt - 1; // use one unit
                return wish;
            }
        }

        // backup plan: give up on matching and just take the first entry with positive count
        object altKey = null;
        int? altCnt = null;
        foreach (DictionaryEntry entry in s)
        {
            if (entry.Value is int ci && ci > 0)
            {
                altKey = entry.Key;
                altCnt = ci;
                break;
            }
        }

        if (altKey == null || altCnt == null) return null;
        s[altKey] = altCnt.Value - 1;
        return altKey;
    }

    // tiny container: one string label plus a list of "w" values (wish list?).
    private class E
    {
        public string N;
        public IList W;

        public E(string n, IList w)
        {
            N = n;
            W = w;
        }
    }

    // looks like an output pair (name + picked value) wrapped in a class.
    public class G
    {
        public readonly string A;
        public readonly string B;

        public G(string a, string b)
        {
            A = a;
            B = b;
        }
    }
}
