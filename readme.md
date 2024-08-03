# Orbit

Code challenge


### General Notes

* This is a recursive implementation of the problem. Check out the main branch for the iterative and optimized version.
* Program is currently configured to use Paths2.txt
* Paths1.txt is a sample and produces result as 42
* Paths2.txt produces 270768
* The recursive function attempts to solve the problem in a top down fashion where it also gets the depth of its children and adds them together to get a node's total depth. This also means that if there are subsequent calls to find the depth of subnodes, then we will have to recalculate everything. However, optimizing this is difficult since all the nodes are visited just once per execution.
* There is a possibility to implement a cache and processing all the nodes in a list. This way we might hit some cache and use the results. However, it DOES NOT provide any improvent in terms of optimization since all the nodes are visited only once.
