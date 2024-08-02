// var allText = await File.ReadAllTextAsync("Paths1.txt"); //42
var allText = await File.ReadAllTextAsync("Paths2.txt"); //270768
var lookup = ParseOrbits(allText);

var total = TotalNumberOfOrbitsOptimized(lookup);
Console.WriteLine(total);


Console.WriteLine("Hello World!");


// parse the entire text file into a lookup.
// We are storing the nodes as lookup[child] = parent since children are unique and multiple
// children can have same parents.
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


// Bottom up calculation. Pick a node and keep going up the tree till you find COM
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
	var depthCache = new Dictionary<string, int>();

	var foo = (Dictionary<string, string> lookup) =>
	{
		var totalSum = 0;
		foreach (var node in lookup)
		{
			var parent = node.Value;
			var sum = 0;
			sum++;

			while (!parent.Equals("COM"))
			{
				if (depthCache.ContainsKey(parent))
				{
					sum += depthCache[parent];
					break;
				}
				parent = nodesLookup[parent];
				sum++;
			}

			totalSum += sum;
			depthCache.Add(node.Key, sum);
		}

		return totalSum;
	};

	return foo(nodesLookup);
}