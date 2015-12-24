<Query Kind="Program" />

void Main(params string[] diff)
{
	if(diff == null)
		diff = new string[0];
		
	Console.WriteLine();
	Console.WriteLine("Vérification du contenu du commit : ");
	Console.WriteLine();
	
	AddMissingCommonImports();
	
	if(diff.Any())
	{
		var changedFiles = ParseDiff(diff);
		CheckNugetFiles(changedFiles);
	}
	
	//Environment.Exit(5);
}

IReadOnlyList<ChangedFile> ParseDiff(string[] diffs)
{
	var baseDirectory = Environment.CurrentDirectory;
	return Enumerable.Range(0, diffs.Length / 2)
							   .Select (e => new ChangedFile
							   				{
												RelativePath = diffs[2 * e + 1].Replace("/", "\\"),
												FullPath = Path.Combine(baseDirectory, diffs[2 * e + 1].Replace("/", "\\")),
												Operation = ParseOperation(diffs[2 * e])
											})
								.ToList();
}

void CheckNugetFiles(IReadOnlyList<ChangedFile> changedFiles)
{	
	var libFiles = changedFiles.Where(o => o.RelativePath.StartsWith(@"libs\")).ToList();
	if (libFiles.Any())
	{
		Console.WriteLine("Erreur détectée !");
		Console.Write("Les fichiers suivants ont été identifiés comme contenu de paquet NuGet, ");
		Console.WriteLine("cependant il est interdit de les commiter. Merci de les enlever.");
		Console.WriteLine();
		Console.WriteLine("Fichiers :");
		libFiles.ForEach(f => Console.WriteLine(f.RelativePath));
		Environment.Exit(1);
	}
}

void AddMissingCommonImports()
{
	var csprojs = Directory.GetFiles(Environment.CurrentDirectory, "*.csproj", SearchOption.AllDirectories);
	const string msbNs = "http://schemas.microsoft.com/developer/msbuild/2003";
	foreach(var csprojFile in csprojs)
	{
		Console.WriteLine("Checking project for imports : " + csprojFile);
		bool changed = false;
		var doc = XDocument.Load(csprojFile);
		var nsManager = new XmlNamespaceManager(doc.CreateReader().NameTable);
		nsManager.AddNamespace("msb", msbNs);
		var imports = doc.XPathSelectElements("//msb:Import", nsManager);
		if(!imports.Any(i => i.Attribute("Project").Value.EndsWith("Common.BeforeBuild.proj")))
		{
			Console.WriteLine("Adding import to Common.BeforeBuild.proj");
			var path = @"$([MSBuild]::GetDirectoryNameOfFileAbove($(MSBuildThisFileDirectory), .gitignore))\tools\Common.BeforeBuild.proj";
			doc.Root.AddFirst(new XElement(XNamespace.Get(msbNs) + "Import",
										   new XAttribute("Project", path),
										   new XAttribute("Condition", string.Format("Exists('{0}')", path))));
			changed = true;							
		}
		if(!imports.Any(i => i.Attribute("Project").Value.EndsWith("Common.AfterBuild.proj")))
		{
			Console.WriteLine("Adding import to Common.AfterBuild.proj");
			var path = @"$([MSBuild]::GetDirectoryNameOfFileAbove($(MSBuildThisFileDirectory), .gitignore))\tools\Common.AfterBuild.proj";
			doc.Root.Add(new XElement(XNamespace.Get(msbNs) + "Import",
										   new XAttribute("Project", path),
										   new XAttribute("Condition", string.Format("Exists('{0}')", path))));
			changed = true;						
		}
		
		if(changed)
			doc.Save(csprojFile);
	}
	
	Console.WriteLine();
}

public class ChangedFile
{
	public string RelativePath { get; set; }
	public string FullPath { get; set; }
	public Operation Operation { get; set; }
}

public enum Operation
{
	Unknown = 0,
	Added,
	Copied,
	Deleted,
	Modified,
	Renamed,
	TypeChanged,
	Unmerged,
	PairingBroken,
}

public Operation ParseOperation(string o)
{
	switch(o)
	{
		case "A" : return Operation.Added;
		case "C" : return Operation.Copied;
		case "D" : return Operation.Deleted;
		case "M" : return Operation.Modified;
		case "R" : return Operation.Renamed;
		case "T" : return Operation.TypeChanged;
		case "U" : return Operation.Unmerged;
		case "B" : return Operation.PairingBroken;
		default: return Operation.Unknown;
	}
}