// var allText = await File.ReadAllTextAsync("Paths1.txt"); //42
var allText = await File.ReadAllTextAsync("Paths2.txt"); //270768
var nodes = ParseOrbits(allText);

var root = nodes["COM"];
var total = TotalNumberOfOrbits(root);
Console.WriteLine(total);


Console.WriteLine("Hello World!");


// parse the entire text file into a lookup.
Dictionary<string, Node> ParseOrbits(string paths)
{
	var nodesLookup = new Dictionary<string, Node>();

	var parsedPaths = paths.Split("\n")
		.Select(i => i.Trim())
		.Where(i => !string.IsNullOrEmpty(i))
		.Select(i =>
		{
			var temp = i.Split(')');
			var parent = temp[0];
			var child = temp[1];
			return (parent, child);
		});

	foreach (var (parent, child) in parsedPaths)
	{
		if (!nodesLookup.ContainsKey(child))
			nodesLookup.Add(child, new Node { Name = child });
		var childNode = nodesLookup[child];

		if (!nodesLookup.ContainsKey(parent))
			nodesLookup.Add(parent, new Node {Name = parent });
		nodesLookup[parent].Children.Add(childNode);	
	}

	return nodesLookup;
}

int TotalNumberOfOrbits(Node root, int depth = 0)
{
	if (root.Children.Count == 0)
		return depth;

	var sum = 0;
	foreach (var child in root.Children)
		sum = sum + TotalNumberOfOrbits(child, depth + 1);

	return depth + sum;
}

public class Node
{
	public string Name;

	public List<Node> Children = new();
}