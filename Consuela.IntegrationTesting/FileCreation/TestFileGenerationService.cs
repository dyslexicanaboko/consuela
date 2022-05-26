using Consuela.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Consuela.IntegrationTesting.FileCreation
{
	public class TestFileGenerationService
	{
		public enum Unit
		{
			Bytes = 0,
			KiloBytes = 10,
			MegaBytes = 20,
			GigaBytes = 30
		}

		public int GetFileSizeInBytes(Unit unit, int units)
		{
			var power = (double)unit;

			var fileSize = Convert.ToInt32(Math.Pow(2, power) * units);

			return fileSize;
		}

		public List<FileInfoEntity> CreateDummyFiles(int numberOfFiles, int fileSizesInBytes, string saveTo, string saveAs, string extension)
		{
			var lst = new List<FileInfoEntity>(numberOfFiles);

			for (int i = 0; i < numberOfFiles; i++)
			{
				var file = saveAs + i + extension;

				var path = Path.Combine(saveTo, file);

				lst.Add(new FileInfoEntity(path, DateTime.Now));

				var txt = GetPayLoad(fileSizesInBytes);

				File.WriteAllText(path, txt);
			}

			return lst;
		}

		public string GetPayLoad(int bytes)
		{
			var r = new Random();

			var sb = new StringBuilder();

			var lineWidth = 0;

			for (int i = 0; i < bytes; i++)
			{
				lineWidth++;

				var n = r.Next(255); //ASCII limits

				var b = new byte[] { Convert.ToByte(n) };

				string s = Encoding.ASCII.GetString(b);

				sb.Append((char)n);

				if (lineWidth == 2000)
				{
					sb.AppendLine();
				}
			}

			var txt = sb.ToString();

			return txt;
		}

		public List<string> CreateDummyFolders(int folders, string baseDirectory)
		{
			var lst = new List<string>();

			for (int i = 0; i < folders; i++)
			{
				var path = Path.Combine(baseDirectory, $"Delete{i}");

				lst.Add(path);

				Directory.CreateDirectory(path);
			}

			return lst;
		}
	}
}
