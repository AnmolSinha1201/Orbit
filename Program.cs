// var allText = await File.ReadAllTextAsync("Paths1.txt"); //42
var allText = await File.ReadAllTextAsync("Paths2.txt"); //270768
var lookup = ParseOrbits(allText);
var root = lookup["COM"];


var total = TotalNumberOfOrbits(root);
Console.WriteLine(total);


Console.WriteLine("Hello World!");


/// <summary>
/// Parses the orbit in the format lookup[nodeName] = node
/// The node object contains all the children of the node
/// </summary>
Dictionary<string, Node> ParseOrbits(string paths)
{
	var lookup = new Dictionary<string, Node>();
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

	foreach (var path in parsedPaths)
	{
		Node parentNode, childNode;
		if (lookup.ContainsKey(path.parent))
			parentNode = lookup[path.parent];
		else
		{
			parentNode = new Node { Name = path.parent };
			lookup.Add(path.parent, parentNode);
		}

		if (lookup.ContainsKey(path.child))
			childNode = lookup[path.child];
		else
		{
			childNode = new Node { Name = path.child };
			lookup.Add(path.child, childNode);
		}

		parentNode.Children.Add(childNode);
	}

	return lookup;
}


/// <summary>
/// Top down parsing where we get the depth from each of the children and add them together
/// </summary>
int TotalNumberOfOrbits(Node root, int depth = 0)
{
	var depthFromChildren = root.Children
		.Select(i => TotalNumberOfOrbits(i, depth + 1))
		.Sum();

	return depth + depthFromChildren;
}

public class Node
{
	public string Name;
	public List<Node> Children = new List<Node>();
}