﻿using System;
namespace Services
{
	public interface ImageLoaderService
	{
		Byte[] LoadImage(string path);
		void SaveImage(byte[] bytes, string fileName, int compression = 100);
	}
}
