// var allText = await File.ReadAllTextAsync("Paths1.txt"); //42
var allText = await File.ReadAllTextAsync("Paths2.txt"); //270768
var lookup = ParseOrbits(allText);

var total = TotalNumberOfOrbitsOptimized(lookup);
Console.WriteLine(total);


Console.WriteLine("Hello World!");


/// <summary>
/// Parse the entire text file into a lookup.
/// We are storing the nodes as lookup[child] = parent since children are unique and multiple children can have same parents.
/// </summary>
Dictionary<string, string> ParseOrbits(string paths)
{
	var parsedPaths = paths.Split("\n")
		.Select(i => i.Trim())
		.Where(i => !string.IsNullOrEmpty(i))
		.Select(i =>
		{
			var temp = i.Split(')');
			var parent = temp[0];
			var child = temp[1];

			return (parent, child);
		})
		.ToDictionary(kvp => kvp.child, kvp => kvp.parent); // Storing in reverse order

	return parsedPaths;
}


/// <summary>
/// Attempts to find the total depth of the tree in a bottom up fashion.
/// Refer to the optimized version for better performance.
/// </summary>

int TotalNumberOfOrbits(Dictionary<string, string> nodesLookup)
{
	var sum = 0;
	foreach (var node in lookup)
	{
		var parent = node.Value;
		sum++;

		while (!parent.Equals("COM"))
		{
			parent = nodesLookup[parent];
			sum++;
		}
	}

	return sum;
}


int TotalNumberOfOrbitsOptimized(Dictionary<string, string> nodesLookup)
{
	var finder = new DepthFinder();
	return finder.Find(nodesLookup);
}


/// <summary>
/// Attempts to find the total depth of the tree in a bottom up fashion.
/// It also attempts to actively cache the results to optimize execution time.
/// Refer to the unoptimized version to understand the algorithm better.
/// </summary>
public class DepthFinder
{
	public Dictionary<string, int> DepthCache = new Dictionary<string, int>();
	int OuterHits = 0;
	int InnerHits = 0;

	public int Find(Dictionary<string, string> lookup)
	{
		var treeTotal = 0;
		foreach (var node in lookup)
		{
			if (DepthCache.ContainsKey(node.Key))
			{
				OuterHits++; // For stats
				treeTotal += DepthCache[node.Key];
				continue;
			}

			var parent = node.Value;
			var depthFromParent = FindParentDepth(lookup, parent);
			var nodeTotal = depthFromParent + 1; // +1 because even if parent returns 0 (COM), the current child still has 1 parent
			treeTotal += nodeTotal;

			// We already know that depth of current node is not cached and all of its parents are cached.
			// So, its safe to add the key directly.
			DepthCache.Add(node.Key, nodeTotal);
		}

		Console.WriteLine($"Cache Hits = {InnerHits + OuterHits}");
		return treeTotal;
	}

	public int FindParentDepth(Dictionary<string, string> lookup, string currentNode)
	{
		
		if (currentNode.Equals("COM"))
			return 0;
		
		if (DepthCache.ContainsKey(currentNode))
		{
			InnerHits++; // For stats
			return DepthCache[currentNode];
		}

		var parentNode = lookup[currentNode];
		var depthFromParent = FindParentDepth(lookup, parentNode);
		// +1 because we have at least 1 depth as parent of caller.
		var nodeTotal = depthFromParent + 1; 

		// If the node was already added to cache, it would have been found earlier.
		// Furthermore, FindParentDepth only caches depth of parents. Hence this is a safe operation
		DepthCache.Add(currentNode, nodeTotal);

		return nodeTotal;
	}
}