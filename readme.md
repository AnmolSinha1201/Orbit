# Orbit

Code challenge


### General Notes

* Program is currently configured to use Paths2.txt
* Paths1.txt is a sample and produces result as 42
* Paths2.txt produces 270768
* Code contains two methods, TotalNumberOfOrbits and TotalNumberOfOrbitsOptimized. Refer to the first method for general understanding of algorithm and second for the implementation of cache.
* There is another branch called "Recursive" for the recursive implementation. However, it has no optimization for the code.
* The current approach uses lookup in `lookup[child] = parent` fashion. It has the advantage that lookups are easy and its fairly trivial to find the depth in a bottom up manner. However, it also has a disadvantage that if we want to find the depth of a subtree, we will have to construct a separate lookup for it. This can be mitigated by using classes/stuct which refers to both parents and its children.


### Future Optimization Possibilities
* Code can be further optimized by using structs/classes to point to parents directly rather than using lookup to find the parent and avoid the cost associated to hashing and lookup.
* Since the code involves a small recursive function to build cache and find depth of parent, code can be optimized even further by converting it to an iterative function. This also protects us from stack overflows in case ultra long chains.
