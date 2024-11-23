namespace OtusFiles
{
	internal class Program
	{
		private static readonly string _path1 = @"c:\Otus\TestDir1";
		private static readonly string _path2 = @"c:\Otus\TestDir2";
		private static List<string> _files = new List<string>();
		static async Task Main(string[] args)
		{
			CreateDirectory(_path1);
			CreateDirectory(_path2);
			foreach (var file in _files)
			{
				await AppendToFile(file, DateTime.Now.ToString());
			}
			foreach (var file in _files)
			{
				await ReadFile(file);
			}
			Console.ReadLine();
		}

		private static void CreateDirectory(string path)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			if (directoryInfo.Exists) { Console.WriteLine(directoryInfo.FullName + "already exists"); }
			else directoryInfo.Create();
			
			for (int i = 1; i <= 10; i++)
			{
				CreateFile(path + Path.DirectorySeparatorChar + "File" + i + ".txt");
			}
		}

		private static void CreateFile(string path)
		{
			if (File.Exists(path)) Console.WriteLine(path + "already exists");
			else File.Create(path).Dispose();
			try
			{
				Write(path);
			}
			catch
			{
				Console.WriteLine("Файл не был записан: " + path);
			}
		}
		private static void Write(string file)
		{
			using (StreamWriter sw = new StreamWriter(file))
			{
				sw.WriteLine(Path.GetFileName(file));
				_files.Add(file);
			}
		}
		private static async Task AppendToFile(string path, string content)
		{
			using (StreamWriter writer = File.AppendText(path))
			{
				await writer.WriteAsync(content);
			}
		}
		private static async Task ReadFile(string path)
		{
			using(StreamReader reader = new StreamReader(path))
			{
				var result = await reader.ReadToEndAsync();
				Console.WriteLine("File: " + path);
				Console.WriteLine(result);
			}
		}
	}
}
